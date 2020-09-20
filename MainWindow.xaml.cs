using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Windows;
using System.Windows.Media;

namespace HSTDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public double[] Labels;
        public double[] Temps;
        public MainWindow()
        {
            InitializeComponent();

            Labels = new double[51];
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<ObservablePoint>{},
                    Fill = Brushes.Transparent,
                }

            };
            GetTemperature(out Temps);

            ConsoleManager.Show();//打开控制台窗口
            for (int i = 0; i <= 50; i++)
            {
                //数据计算
                double svp = SaturationVaporPressure(Temps[i]); //饱和水蒸气压力PˋˋhPa
                double mc = MoistureContent(svp, 0.12f, 1013.0f);
                Labels[i] = mc;

                var op = new ObservablePoint(Labels[i], Temps[i]);
                SeriesCollection[0].Values.Add(op);

                //调试信息
                Console.Write(Temps[i] + "   " + svp + "   " + mc + "\n");

            }

            DataContext = this;

        }

        public void GetTemperature(out double[] labels)
        {
            double startTemp = -10f;
            double[] dataTemp = new double[51];

            for (int i = 0; i <= 50; i++)
            {
                dataTemp[i] = startTemp;
                startTemp += 1;
            }
            labels = dataTemp;
 
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

