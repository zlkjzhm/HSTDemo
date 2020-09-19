using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HSTDemo
{
    public class ComFun
    {
        /// <summary>
        /// 湿空气比焓Ι
        /// </summary>
        /// <returns>湿空气比焓(kJ/kg(干空气))</returns>
        public static double SpecificEnthalpy(double Theta, double MC)
        {
            double SE = 1.005f * Theta + MC * (2500.8f + 1.846f * Theta);
            return SE;
        }

    }
}
