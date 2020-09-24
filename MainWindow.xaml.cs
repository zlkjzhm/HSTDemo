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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private SeriesCollection seriescCollection;
        public SeriesCollection SeriesCollection 
        {
            get
            {
                return seriescCollection;
            }
            set 
            {
                seriescCollection = value;
                OnPropertyChanged("SeriesCollection");
            }
        
        }
        public VisualElementsCollection visuals;

        public VisualElementsCollection Visuals
        {
            get { return visuals; }
            set
            {
                visuals = value;
                OnPropertyChanged("Visuals");
            }
        }
        //private double pressure;
        public double Pressure { get; set; }


        //public VisualElement XDSDVisualElement { get; set; }
        public double[][] Labels;
        public double[] Temps;
        public double[] SVPs;
        private double[] arh;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public MainWindow()
        {
            InitializeComponent();


            //ConsoleManager.Show();//打开控制台窗口
            //UpdateXDSD(1013.25);
            InitXDSD(233);



            DataContext = this;

        }
        public void InitXDSD(double pressure)
        {
            Visuals = new VisualElementsCollection { };
            SeriesCollection = new SeriesCollection { };
            Labels = new double[10][];
            arh = new double[10];

            for (int i = 0; i < 10; i++)
            {
                arh[i] = 0.1 + 0.05 * i;
                Labels[i] = new double[51];
                SeriesCollection.Add(
                    new LineSeries
                    {
                        Values = new ChartValues<ObservablePoint> { },
                        LineSmoothness = 0,
                        Fill = Brushes.Transparent,
                        PointGeometrySize = 0,
                        //DataLabels = true,
                    }
                    );

                Visuals.Add(
                   new VisualElement
                   {
                       HorizontalAlignment = HorizontalAlignment.Left,
                       VerticalAlignment = VerticalAlignment.Center,
                       UIElement = new XDSDVEControl(arh[i])
                   }
                    );
            }

            GetTemperature(out Temps);
            SVPs = new double[51];
            for (int i = 0; i < 51; i++)
            {
                SVPs[i] = SaturationVaporPressure(Temps[i]);
            }
            for (int j = 0; j < 10; j++)
            {
                bool isShowVE = false;

                for (int i = 0; i < 51; i++)
                {
                    //数据计算
                    double mc = MoistureContent(SVPs[i], arh[j], pressure);
                    Labels[j][i] = mc;

                    var op = new ObservablePoint(Labels[j][i], Temps[i]);
                    //显示线
                    SeriesCollection[j].Values.Add(op);
                    //显示标志
                    if ((Temps[i] >= 40.0) && !isShowVE)
                    {
                        Visuals[j].X = Labels[j][i];
                        Visuals[j].Y = 39;
                        isShowVE = true;

                    }
                    else if (Labels[j][i] >= 50 && !isShowVE)
                    {
                        Visuals[j].X = 49;
                        Visuals[j].Y = Temps[i];
                        isShowVE = true;

                    }

                    //调试信息
                    //Console.Write(Temps[i] + "   " + svp + "   " + mc + "\n");

                }
            }
        }
        public void UpdateXDSD(double pressure)
        {
            for (int j = 0; j < 10; j++)
            {
                bool isShowVE = false;

                for (int i = 0; i < 51; i++)
                {
                    //数据计算
                    double svp = SaturationVaporPressure(Temps[i]); //饱和水蒸气压力PˋˋhPa
                    double mc = MoistureContent(svp, arh[j], pressure);
                    Labels[j][i] = mc;

                    //显示线 
                    ObservablePoint op = (ObservablePoint)SeriesCollection[j].Values[i];
                    op.X = Labels[j][i];
                    op.Y = Temps[i];
                    if ((Temps[i] >= 40.0) && !isShowVE)
                    {
                        Visuals[j].X = Labels[j][i];
                        Visuals[j].Y = 39;
                        isShowVE = true;

                    }
                    else if (Labels[j][i] >= 50 && !isShowVE)
                    {
                        Visuals[j].X = 49;
                        Visuals[j].Y = Temps[i];
                        isShowVE = true;

                    }

                }
            }

        }

        private void PressureKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UpdateXDSD(Pressure);

                Console.Write("压强：" + Pressure);
            }
        }

        private void PressureMouseLeave(object sender, MouseEventArgs e)
        {
            //Console.Write("压强：" + Pressure);
        }

        //计算函数
        public void GetTemperature(out double[] labels)
        {
            double startTemp = -10f;
            double[] dataTemp = new double[51];

            for (int i = 0; i < 51; i++)
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




        //private void PressureTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        //{
        //   if(e.)
        //    Console.Write("压强：" + Pressure);
        //}
    }
}

