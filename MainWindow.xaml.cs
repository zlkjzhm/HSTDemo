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
        public VisualElement XDSDVisualElement { get; set; }

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

        public double[][] Labels;
        public double[] Temps;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public double Pressure { get; set; }

        public MainWindow()
        {
            InitializeComponent();


            //ConsoleManager.Show();//打开控制台窗口
            //UpdateXDSD(1013.25);
                UpdateXDSD(1033);



            DataContext = this;

        }
        public void UpdateXDSD(double pressure)
        {
            double arh = 0.1f;
            Visuals = new VisualElementsCollection { };
            SeriesCollection = new SeriesCollection { };
            Labels = new double[10][];

            for (int i = 0; i < 10; i++)
            {
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
            }

            GetTemperature(out Temps);

            for (int j = 0; j < 10; j++)
            {
                bool isShowVE = false;
                arh += 0.02f;
                SeriesCollection[j].Values.Clear();
                for (int i = 0; i < 51; i++)
                {
                    //数据计算
                    double svp = SaturationVaporPressure(Temps[i]); //饱和水蒸气压力PˋˋhPa
                    double mc = MoistureContent(svp, arh, pressure);
                    Labels[j][i] = mc;

                    var op = new ObservablePoint(Labels[j][i], Temps[i]);
                    //
                    SeriesCollection[j].Values.Add(op);
                    
                    if(((Temps[i] >= 40.0) || (Labels[j][i] >= 50)) && !isShowVE)
                    {
                        double x = Labels[j][i];
                        double y = Temps[i];
                        if (Temps[i] >= 40.0)
                            x = 39.0;
                        else if (Labels[j][i] >= 50)
                            y = 49.0;
 
                        var ve = new VisualElement
                        {
                            X = x,
                            Y = y,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Bottom,
                            UIElement = new XDSDVEControl(arh)
                        };
                        Visuals.Add(ve);
                        isShowVE = true;
                    }
                    
                    //调试信息
                    //Console.Write(Temps[i] + "   " + svp + "   " + mc + "\n");

                }
            }

        }

        private void PressureKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UpdateXDSD(133);

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

