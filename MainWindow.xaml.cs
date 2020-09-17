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

            LineSeries ls1 = new LineSeries();
            SeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "2月6日",
                        Values = new ChartValues<double>  { -4, -4, -5, -56, -7, -8, -9, -8, -6, -4, -5, -5, -2, -1, 0, 1, 3, 6, 6, 4, 52, 1, -1, -2},
                        //Fill = Brushes.Transparent,

                    },
                    new LineSeries
                    {
                        Title = "9月5日",
                        Values = new ChartValues<double> { 23, 22, 22, 21, -21, 21, 21, 22, 23, 25, 26, 27, 29, -30, 31, 31, 31, 30, 28, 26, 25, 24, 24, 23},
                        //Fill = Brushes.Transparent,

                    },

                    new LineSeries
                    {
                        Title = "5月18日",
                        Values = new ChartValues<double>  { 15, -4, -5, -6, -7, -8, 34, -8, -6, -4, 124, -5, 33, -1, 0, 1, 3, 6, 6, 4, 2, 1, -1, -2},
                        //Fill = Brushes.Transparent,

                    },
                    new LineSeries
                    {
                        Title = "7月17日",
                        Values = new ChartValues<double> { 23, 89, 22, 21, 45, 21, 21, 22, 23, -25, 26, 27, 78, 3, 4, 41, 33, 30, 28, 26, 25, 24, 24, 23},
                        //Fill = Brushes.Transparent,

                    },
                };

            Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
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
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
    }
}

