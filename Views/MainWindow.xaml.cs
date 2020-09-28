using HSTDemo.ViewModels;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HSTDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        //HSChartViewModel _chartViewModel;


        public MainWindow()
        {
            InitializeComponent();


            //ConsoleManager.Show();//打开控制台窗口
            //UpdateXDSD(1013.25);
            //InitXDSD(233);


            //_chartViewModel = new HSChartViewModel();
            //DataContext = _chartViewModel;

        }
        //public void InitXDSD(double pressure)
        //{
        //    Visuals = new VisualElementsCollection { };
        //    SeriesCollection = new SeriesCollection { };
        //    Labels = new double[10][];
        //    arh = new double[10];

        //    for (int i = 0; i < 10; i++)
        //    {
        //        arh[i] = 0.1 + 0.05 * i;
        //        Labels[i] = new double[51];
        //        SeriesCollection.Add(
        //            new LineSeries
        //            {
        //                Values = new ChartValues<ObservablePoint> { },
        //                LineSmoothness = 0,
        //                Fill = Brushes.Transparent,
        //                PointGeometrySize = 0,
        //                //DataLabels = true,
        //            }
        //            );

        //        Visuals.Add(
        //           new VisualElement
        //           {
        //               HorizontalAlignment = HorizontalAlignment.Left,
        //               VerticalAlignment = VerticalAlignment.Center,
        //               UIElement = new XDSDVEControl(arh[i])
        //           }
        //            );
        //    }

        //    GetTemperature(out Temps);
        //    SVPs = new double[51];
        //    for (int i = 0; i < 51; i++)
        //    {
        //        SVPs[i] = SaturationVaporPressure(Temps[i]);
        //    }
        //    for (int j = 0; j < 10; j++)
        //    {
        //        bool isShowVE = false;

        //        for (int i = 0; i < 51; i++)
        //        {
        //            //数据计算
        //            double mc = MoistureContent(SVPs[i], arh[j], pressure);
        //            Labels[j][i] = mc;

        //            var op = new ObservablePoint(Labels[j][i], Temps[i]);
        //            //显示线
        //            SeriesCollection[j].Values.Add(op);
        //            //显示标志
        //            if ((Temps[i] >= 40.0) && !isShowVE)
        //            {
        //                Visuals[j].X = Labels[j][i];
        //                Visuals[j].Y = 39;
        //                isShowVE = true;

        //            }
        //            else if (Labels[j][i] >= 50 && !isShowVE)
        //            {
        //                Visuals[j].X = 49;
        //                Visuals[j].Y = Temps[i];
        //                isShowVE = true;

        //            }

        //            //调试信息
        //            //Console.Write(Temps[i] + "   " + svp + "   " + mc + "\n");

        //        }
        //    }
        //}
        //public void UpdateXDSD(double pressure)
        //{
        //    for (int j = 0; j < 10; j++)
        //    {
        //        bool isShowVE = false;

        //        for (int i = 0; i < 51; i++)
        //        {
        //            //数据计算
        //            double svp = SaturationVaporPressure(Temps[i]); //饱和水蒸气压力PˋˋhPa
        //            double mc = MoistureContent(svp, arh[j], pressure);
        //            Labels[j][i] = mc;

        //            //显示线 
        //            ObservablePoint op = (ObservablePoint)SeriesCollection[j].Values[i];
        //            op.X = Labels[j][i];
        //            op.Y = Temps[i];
        //            if ((Temps[i] >= 40.0) && !isShowVE)
        //            {
        //                Visuals[j].X = Labels[j][i];
        //                Visuals[j].Y = 39;
        //                isShowVE = true;

        //            }
        //            else if (Labels[j][i] >= 50 && !isShowVE)
        //            {
        //                Visuals[j].X = 49;
        //                Visuals[j].Y = Temps[i];
        //                isShowVE = true;

        //            }

        //        }
        //    }

        //}

        //private void PressureKeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {
        //        UpdateXDSD(Pressure);

        //        Console.Write("压强：" + Pressure);
        //    }
        //}

        //private void PressureMouseLeave(object sender, MouseEventArgs e)
        //{
        //    //Console.Write("压强：" + Pressure);
        //}

      
    }
}

