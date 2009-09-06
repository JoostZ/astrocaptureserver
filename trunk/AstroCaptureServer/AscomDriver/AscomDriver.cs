using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ASCOM.Interface;
using ASCOM.Helper;
using ASCOM.DriverAccess;
using System.Threading;
using ScopeServer;

namespace AscomGuiding
{
    public class AscomDriver : ITelescopeDriver
    {
        public delegate void StartStopHandler(bool forward);
        public event StartStopHandler OnStartRa = null;
        public event StartStopHandler OnStopRa = null;
        public event StartStopHandler OnStartDe = null;
        public event StartStopHandler OnStopDe = null;

        private Telescope _telescope;
        public AscomDriver(String ProgId)
        {
            _telescope = new Telescope(ProgId);
            _telescope.Connected = true;
            _telescope.Unpark();
        }
        public void Setup()
        {
            _telescope.SetupDialog();
        }

        public void Disconnect()
        {
            _telescope.Connected = false;
            _telescope.Dispose();
            _telescope = null;
        }

        public double GuideRateAscension
        {
            get
            {

                if (_telescope.CanPulseGuide)
                {
                    // GuideRatRightAscension is in arcseconds per second
                    // We need to convert it to miliseconds per arcsecond
                    return _telescope.GuideRateRightAscension;
                }
                else
                {
                    return 0.0;
                }
            }
        }
        public double GuideRateDeclination
        {
            get
            {

                if (_telescope.CanPulseGuide)
                {
                    // GuideRatRightAscension is in arcseconds per second
                    // We need to convert it to miliseconds per arcsecond
                    return _telescope.GuideRateDeclination;
                }
                else
                {
                    return 0.0;
                }
            }
        }

        #region ITelescopeDriver Members

        public void Pulse(int raDuration, int decDuration)
        {
            GuideDirections direction;
            if (raDuration != 0)
            {
                if (raDuration < 0)
                {
                    raDuration = -raDuration;
                    direction = GuideDirections.guideWest;
                }
                else
                {
                    direction = GuideDirections.guideEast;
                }
                _telescope.PulseGuide(direction, raDuration);
            }
            if (decDuration != 0)
            {
                if (decDuration < 0)
                {
                    decDuration = -decDuration;
                    direction = GuideDirections.guideSouth;
                }
                else
                {
                    direction = GuideDirections.guideNorth;
                }
                _telescope.PulseGuide(direction, decDuration);
            }
        }

        #endregion

        public String Select()
        {
            string ProgId = Telescope.Choose("");
            _telescope = new Telescope(ProgId);
            _telescope.Connected = true;
            _telescope.Unpark();

            return
            _telescope.Name;
        }

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
    }
}
