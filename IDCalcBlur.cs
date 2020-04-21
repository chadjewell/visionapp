using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionApp
{
    class IDCalcBlur
    {
        double Pixels;
        double SensorL;
        string name;

        public IDCalcBlur(double Pixels, double SensorL, string name)
        {
            this.name = name;
            this.SensorL = SensorL;
            this.Pixels = Pixels; 
        }

        public double MaxE(double FOV, double LineS)
        {
            return FOV / Pixels / LineS * 1000000;
        }

        public double MaxFOV (double MIL, double PPM)
        {
            return MIL / (1000 * PPM) * Pixels;
        }

    }
}
