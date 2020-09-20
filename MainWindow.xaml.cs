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
        public MainWindow()
        {
            InitializeComponent();

            //Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            GetTemperature(out Temps);
            Labels = new double[51];
            SeriesCollection = new SeriesCollection
            {
                //new LineSeries
                //{
                //   // Title = "2月6日",
                //    Values = new ChartValues<double>  {},
                //    Fill = Brushes.Transparent,

                //},
                new LineSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {

                    },
                    Fill = Brushes.Transparent,
                }

            };
            ConsoleManager.Show();//打开控制台窗口
            for (int i = 0; i <= 50; i++)
            {
                double svp = SaturationVaporPressure(Temps[i]); //饱和水蒸气压力PˋˋhPa
                double mc = MoistureContent(svp, 0.12f, 1013.0f);
                Labels[i] = mc;
                //SeriesCollection[0].Values.Add(Temps[i]);
                var op = new ObservablePoint(Labels[i], Temps[i]);
                SeriesCollection[0].Values.Add(op);
                Console.Write(Temps[i] + "   " + svp + "   " + mc + "\n");

            }
            //YFormatter = value => value.ToString("C");

            //modifying the series collection will animate and update the chart
            //seriesCollection.Add(new LineSeries
            //{
            //    Values = new ChartValues<double> { 5, 3, 2, 4 },
            //    LineSmoothness = 0 //straight lines, 1 really smooth lines
            //});

            ////modifying any series values will also animate and update the chart
            //seriesCollection[2].Values.Add(5d);

            DataContext = this;

        }

        public SeriesCollection SeriesCollection { get; set; }
        public double[] Labels;
        public double[] Temps;

        //public Func<double, string> YFormatter { get; set; }

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

