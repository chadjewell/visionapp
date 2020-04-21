using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionApp
{
    class Insight
    {
        double sensorLength;
        string name;

        public Insight()
        {

        }

        public Insight(double sensorLength, string name)
        {
            this.sensorLength = sensorLength;
            this.name = name;
        }

        public double Beta(double FOV, double WD)
        {
            double oneObeta = (FOV / sensorLength) + 1;
            double focal = WD / oneObeta;
            return focal;
        }

        public double WorkD(double FOV, double focal)
        {
            double oneObeta = (FOV / sensorLength) + 1;
            return focal * oneObeta;
        }

        public double FOVcalc(double WD, double focal)
        {
            return sensorLength * ((WD / focal) - 1);
        }

        public double[] VA(double FOV, double minFeat)
        {
            double[] accuracy = { (1 / minFeat) * FOV, (3 / minFeat) * FOV, (5 / minFeat) * FOV, (10 / minFeat) * FOV, (20 / minFeat) * FOV, (25 / minFeat) * FOV };
            return accuracy; 
        }
    }
}
