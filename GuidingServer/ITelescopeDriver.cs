using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AstroCaptureServer
{
    /**
     * @brief
     * Interface to telescope driver
     */
    public interface ITelescopeDriver
    {
        /**
         * @brief
         * Perform guidance during specified duration
         * 
         * @param raDuration
         * Duration of movement in RA direction (milliseconds)
         * 
         * @param decDuration
         * Duration of movement in DEC direction (milliseconds)
         */
        void Pulse(int raDuration, int decDuration);
    }
}
