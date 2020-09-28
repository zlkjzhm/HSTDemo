using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSTDemo.Models
{
    class HSAtmosphere
    {
        public HSAtmosphere()
        {
            _airPressure = 1013.25;
        }
        private double _airPressure;

        public double AirPressure
        {
            get { return _airPressure; }
            set { _airPressure = value; }
        }

        private double _airRelativeHumidity;

        public double AirRelativeHumidity
        {
            get { return _airRelativeHumidity; }
            set { _airRelativeHumidity = value; }
        }


        /// <summary>
        /// 计算饱和水蒸气压力Pˋˋ
        /// </summary>
        /// <param name="t">空气温度</param>
        /// <returns></returns>
        public double SaturationVaporPressure(double t)
        {
            double E = 2.0057173f - 3.142305f * (1000.0f / (273.15f + t) - 1000.0f / 373.15f) + 8.2f * Math.Log10(373.15f / (273.15f + t)) - 0.0024804f * (100.0f - t);
            double P = Math.Pow(10, E);
            return P * 10; //转换成hPa
        }

        /// <summary>
        /// 湿空气的含湿量χ
        /// </summary>
        /// <returns></returns>
        public double MoistureContent(double SVP, double ARH, double AirPressure)
        {
            double MC = 622f * ARH * SVP / (AirPressure - ARH * SVP);//g/kg
            return MC;
        }

    }
}
