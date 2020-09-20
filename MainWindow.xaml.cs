using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Windows;

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

            SeriesCollection = new SeriesCollection
                {
                    //new LineSeries
                    //{
                    //    Title = "2月6日",
                    //    Values = new ChartValues<double>  { -4, -4, -5, -56, -7, -8, -9, -8, -6, -4, -5, -5, -2, -1, 0, 1, 3, 6, 6, 4, 52, 1, -1, -2},
                    //    //Fill = Brushes.Transparent,

                    //},
                 
                };

            //Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            GetTemperature(out Labels);
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
        public float[] Labels;
        //public Func<double, string> YFormatter { get; set; }

        public void GetTemperature(out float[] labels)
        {
            float startTemp = -10f;
            float[] dataTemp = new float[51];
            ConsoleManager.Show();//打开控制台窗口

            for (int i = 0; i <= 50; i++)
            {
                dataTemp[i] = startTemp;
                startTemp += 1;
            }
            labels = dataTemp;
            for (int i = 0; i <= 50; i++)
            {
                Console.Write(labels[i]+"\n");

            }
        }

    }
}

