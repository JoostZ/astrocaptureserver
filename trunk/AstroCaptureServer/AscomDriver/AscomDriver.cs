using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ASCOM.Interface;
using ASCOM.Helper;
using ASCOM.DriverAccess;
using System.Threading;
using AstroCaptureServer;

namespace AstroCaptureServer
{
    namespace Driver
    {
        /**
         * @brief
         * ASCOM driver used in ScopeServer
         */
        public class AscomDriver : ITelescopeDriver, IDisposable
        {
            public delegate void StartStopHandler(bool forward);
            public event StartStopHandler OnStartRa = null;
            public event StartStopHandler OnStopRa = null;
            public event StartStopHandler OnStartDe = null;
            public event StartStopHandler OnStopDe = null;

            private Telescope iTelescope;

            /**
             * @brief
             * Initiate a Telescope object specified by ID
             * 
             * @param ProgId
             * ID of the ASCOM driver
             */
            public AscomDriver(String ProgId)
            {
                iTelescope = new Telescope(ProgId);
                iTelescope.Connected = true;
                if (iTelescope.CanUnpark)
                {
                    iTelescope.Unpark();
                }
            }

            /**
             * @brief
             * Destructor
             */
            ~AscomDriver()
            {
                Dispose(false);
            }

            #region Disposing

            private bool iDisposed = false;

            /**
             * @brief
             * Release resources of the driver
             */
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(bool disposing)
            {
                if (!this.iDisposed)
                {
                    if (iTelescope.CanPark)
                    {
                        iTelescope.Park();
                    }
                    iTelescope.Connected = false;

                    // If disposing equals true, dispose all managed
                    // and unmanaged resources.
                    if (disposing)
                    {
                        iTelescope.Dispose();
                    }

                    // Note disposing has been done.
                    iDisposed = true;
                }
            }
            #endregion

            #region TelescopeProperties

            /**
         * @brief
         * Setup of the associated Telescope
         */
            public void Setup()
            {
                iTelescope.SetupDialog();
            }

            /**
             * @brief
             * Name of the Telescope
             */
            public String Name
            {
                get
                {
                    return iTelescope.Name;
                }
            }


            public double GuideRateAscension
            {
                get
                {

                    if (iTelescope.CanPulseGuide)
                    {
                        return iTelescope.GuideRateRightAscension;
                    }
                    else
                    {
                        return 0.0;
                    }
                }
            }

            /**
             * @brief
             * The guiding rate (in degrees per second) in Dec
             */
            public double GuideRateDeclination
            {
                get
                {

                    if (iTelescope.CanPulseGuide)
                    {
                        return iTelescope.GuideRateDeclination;
                    }
                    else
                    {
                        return 0.0;
                    }
                }
            }
            #endregion

            #region ITelescopeDriver Members

            /**
         * @brief
         * Pulse the telescope
         * 
         * @param raDuration
         * Time (in miliseconds) to move in RA direction. Sign specifies the direction.
         * 
         * @param
         * Time (in miliseconds) to move in Dec direction. Sign specifies the direction.
         * 
         * @remark
         * Currently only pulsguiding is supported, not separate MoveAxis
         */
            public void Pulse(int raDuration, int decDuration)
            {
                GuideDirections direction;
                if (raDuration != 0)
                {
                    if (raDuration < 0)
                    {
                        raDuration = -raDuration;
                        direction = GuideDirections.guideEast;
                    }
                    else
                    {
                        direction = GuideDirections.guideWest;
                    }
                    iTelescope.PulseGuide(direction, raDuration);
                }
                if (decDuration != 0)
                {
                    if (decDuration < 0)
                    {
                        decDuration = -decDuration;
                        direction = GuideDirections.guideNorth;
                    }
                    else
                    {
                        direction = GuideDirections.guideSouth;
                    }
                    iTelescope.PulseGuide(direction, decDuration);
                }
            }

            #endregion

            #region Future functions
            private void GuideParallel(int ra_milli, int dec_milli)
            {

                // The essence of Parallel guiding is to allow both RA and DE motors
                // to be engaged at the same time.

                // NOTE: Some controllers do not allow such parallel guiding
                // and then you must use serial guiding instead

                // ignore if zero only
                if (ra_milli == 0 && dec_milli == 0) return;



                // Start DEC guiding
                if (dec_milli != 0)
                {
                    bool forward = dec_milli > 0 ? true : false;
                    if (OnStartDe != null)
                    {
                        OnStartDe(forward);
                    }
                }

                // start RA guiding
                if (ra_milli != 0)
                {
                    bool forward = ra_milli > 0 ? true : false;
                    if (OnStartRa != null)
                    {
                        OnStartRa(forward);
                    }
                }

                // durations withou sign
                int ra_dur = Math.Abs(ra_milli);
                int dec_dur = Math.Abs(dec_milli);

                // total duration equals longest duration
                // phase1 equals shortest duration
                // phase2 equals difference
                int total = 0;
                int phase1 = 0;
                bool ra_shortest = false;
                if (ra_dur > dec_dur)
                {
                    total = ra_dur;
                    phase1 = dec_dur;
                    ra_shortest = false;
                }
                else
                {
                    total = dec_dur;
                    phase1 = ra_dur;
                    ra_shortest = true;
                }
                int phase2 = total - phase1;

                // let phase1 finish
                Thread.Sleep(phase1);
                if (ra_shortest)
                {
                    bool forward = ra_milli > 0 ? true : false;
                    if (OnStopRa != null)
                    {
                        OnStopRa(forward);
                    }
                }
                else
                {
                    bool forward = dec_milli > 0 ? true : false;
                    if (OnStopDe != null)
                    {
                        OnStopDe(forward);
                    }
                }

                // let phase2 finish
                Thread.Sleep(phase2);
                if (ra_shortest)
                {
                    bool forward = dec_milli > 0 ? true : false;
                    if (OnStopDe != null)
                    {
                        OnStopDe(forward);
                    }
                }
                else
                {
                    bool forward = ra_milli > 0 ? true : false;
                    if (OnStopRa != null)
                    {
                        OnStopRa(forward);
                    }
                }

            }
            #endregion
        }
    }
}