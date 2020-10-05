using HSTDemo.Models;

namespace HSTDemo
{
    class LinesDatabase
    {
        public const int TEMP_MAX = 60;    //显示的最大温度
        public const int TEMP_MIN = -20;   //显示的最小温度
        public const int TEMP_INCREMENT = 1; //显示的温度的增量
        public const int TEMP_LENGTH = TEMP_MAX - TEMP_MIN + 1;    //温度刻度的数量

        public const int MC_MAX = 50;    //显示的最大含湿量
        public const int MC_MIN = 0;   //显示的最小含湿量
        //private const int MC_INCREMENT = 1; //显示的温度的增量


        public const int ARH_LINE_COUNT = 20; //相对湿度线的数量
        public const double ARH_TO_SHOW_MIN = 0.05; //显示的最小相对湿度
        public const double ARH_TO_SHOW_INCREMENT = 0.05; //相对湿度的增量

        public static void GetAtmosphereTables(out double[] temps, out double[] svps, out double[] arhs)
        {
            //获取温度列表
            double startTemp = (double)TEMP_MIN; ; //TODO: xaml中改成绑定常量的形式
            temps = new double[TEMP_LENGTH];
            //获取温度对应的饱和水蒸气分压力列表
            svps = new double[TEMP_LENGTH];

            //要显示的相对湿度列表
            arhs = new double[ARH_LINE_COUNT];
            //mcs = new double[ARH_LINE_COUNT][];
            for (int i = 0; i < ARH_LINE_COUNT; i++)
            {
                arhs[i] = ARH_TO_SHOW_MIN + i * ARH_TO_SHOW_INCREMENT;
                //mcs[i] = new double[TEMP_LENGTH];
            }

            for (int i = 0; i < TEMP_LENGTH; i++)
            {
                temps[i] = startTemp;
                startTemp += TEMP_INCREMENT;

                svps[i] = HSAtmosphere.SaturationVaporPressure(temps[i]);

            }
        }
    }
}
