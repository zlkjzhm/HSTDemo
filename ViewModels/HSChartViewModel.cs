using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using FzyMVVM;
using HSTDemo.Models;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace HSTDemo.ViewModels
{
    class HSChartViewModel : ObservableObject 
    {
        public HSChartViewModel()
        {
            _chart = new HSChart();
            _atmosphere = new HSAtmosphere();
            InitChart();
        }

        public const int TEMP_MAX = 60;    //显示的最大温度
        public const int TEMP_MIN = -20;   //显示的最小温度
        private const int TEMP_INCREMENT = 1; //显示的温度的增量
        private const int TEMP_LENGTH = TEMP_MAX - TEMP_MIN + 1;    //温度刻度的数量

        public const int MC_MAX = 50;    //显示的最大含湿量
        public const int MC_MIN = 0;   //显示的最小含湿量
        //private const int MC_INCREMENT = 1; //显示的温度的增量


        private const int ARH_LINE_COUNT = 20; //相对湿度线的数量
        private const double ARH_TO_SHOW_MIN= 0.05; //显示的最小相对湿度
        private const double ARH_TO_SHOW_INCREMENT = 0.05; //相对湿度的增量


        private HSChart _chart;
        private HSAtmosphere _atmosphere;

        public double Pressure
        {
            get { return _atmosphere.AirPressure; }
            set
            {
                _atmosphere.AirPressure = value;
                RaisePropertyChanged("Pressure");
            }
        }

        private double[] _tempsList;
        private double[] _svpList;
        private double[] _arhToShowList;

        public SeriesCollection SeriesCollection
        {
            get { return _chart.SeriesCollection; }
            set 
            { 
                _chart.SeriesCollection = value;
                RaisePropertyChanged("SeriesCollection");
            }
        }

        public VisualElementsCollection CartesianVisuals
        {
            get { return _chart.CartesianVisuals; }
            set 
            {
                _chart.CartesianVisuals = value; 
                RaisePropertyChanged("CartesianVisuals");
            }
        }


        private void InitChart()
        {
            GetAtmosphereTables(out _tempsList, out _svpList, out _arhToShowList);
            SeriesCollection = new SeriesCollection();
            CartesianVisuals = new VisualElementsCollection();
            for (int j = 0; j < ARH_LINE_COUNT; j++)
            {
                LineSeries ls = new LineSeries
                {
                    Values = new ChartValues<ObservablePoint> { },
                    LineSmoothness = 0,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 0,
                    //DataLabels = true,
                };

                VisualElement ve = new VisualElement
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    UIElement = new HSVEControl(_arhToShowList[j])
                };
                double lastX = 0;
                double lastY = 0;
                bool hasShowEV = false;
                for (int i = 0; i < TEMP_LENGTH; i++)
                {
                    //数据计算
                    double mc = _atmosphere.MoistureContent(_svpList[i], _arhToShowList[j], Pressure);
                    var op = new ObservablePoint(mc, _tempsList[i]);
                    //显示线
                    ls.Values.Add(op);
                    if(!hasShowEV)
                    {
                        if (_tempsList[i] >= TEMP_MAX)
                        {
                            hasShowEV = true;
                            lastX = mc;
                            lastY = _tempsList[i];
                        }
                        else if (mc >= MC_MAX)
                        {
                            hasShowEV = true;
                            lastX = MC_MAX;
                            lastY = _tempsList[i];
                        }
                    }
                    

                    //不会显示的就不要再遍历了
                    //if (mc >= MC_MAX)
                    //{
                    //    break;
                    //}

                }
                SeriesCollection.Add(ls);
                //显示标志
                ve.X = lastX;
                ve.Y = lastY;
                CartesianVisuals.Add(ve);
            }

        }
        private void UpdateChart()
        {
            for (int j = 0; j < ARH_LINE_COUNT; j++)
            {
                double lastX = 0; 
                double lastY = 0;
                bool hasShowEV = false;
                for (int i = 0; i < TEMP_LENGTH; i++)
                {
                    //数据计算
                    double mc = _atmosphere.MoistureContent(_svpList[i], _arhToShowList[j], Pressure);
                   // var op = new ObservablePoint(mc, _tempsList[i]);
                    //显示线
                    ObservablePoint op = (ObservablePoint)SeriesCollection[j].Values[i];
                    op.X = mc;
                    op.Y = _tempsList[i];
                    if(!hasShowEV)
                    {
                        if (_tempsList[i] >= TEMP_MAX)
                        {
                            hasShowEV = true;
                            lastX = mc;
                            lastY = _tempsList[i];
                        }
                        else if (mc >= MC_MAX)
                        {
                            hasShowEV = true;
                            lastX = MC_MAX;
                            lastY = _tempsList[i];
                        }
                    }

                    //不会显示的就不要再遍历了
                    //if (mc >= MC_MAX)
                    //{
                    //    break;
                    //}

                }
                //SeriesCollection.Add(ls);
                //显示标志
                CartesianVisuals[j].X = lastX;
                CartesianVisuals[j].Y = lastY;
                //CartesianVisuals.Add(ve);
            }
        }

        //计算函数
        private void GetAtmosphereTables(out double[] temps, out double[] svps, out double[] arhs)
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

                svps[i] = _atmosphere.SaturationVaporPressure(temps[i]);

            }
        }
        void UpdatePressureExecute()
        {
            //_atmosphere.AirPressure = 1.111f;
            UpdateChart();
        }

        bool CanUpdatePressureNameExecute()
        {
            return true;
        }

        public ICommand UpdatePressureName 
        { 
            get { return new RelayCommand(UpdatePressureExecute, CanUpdatePressureNameExecute); } 
        }
    }
}
