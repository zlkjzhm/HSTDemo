using HSTDemo.Models;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Windows;
using System.Windows.Media;

namespace HSTDemo.ViewModels
{
    class HSARHViewModel : LineViewModelBase
    {
        public HSARHViewModel(double pressure, SeriesCollection seriesCollection, VisualElementsCollection cartesianVisuals)
        {
            _pressure = pressure;
            _seriesCollection = seriesCollection;
            _cartesianVisuals = cartesianVisuals;
            InitChart();
        }
        private double _pressure;
        private SeriesCollection _seriesCollection;
        private VisualElementsCollection _cartesianVisuals;
        private double[] _tempsList;
        private double[] _svpList;
        private double[] _arhToShowList;




        void InitChart()
        {
            LinesDatabase.GetAtmosphereTables(out _tempsList, out _svpList, out _arhToShowList);
            for (int j = 0; j < LinesDatabase.ARH_LINE_COUNT; j++)
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
                for (int i = 0; i < LinesDatabase.TEMP_LENGTH; i++)
                {
                    //数据计算
                    double mc = HSAtmosphere.MoistureContent(_svpList[i], _arhToShowList[j], _pressure);
                    var op = new ObservablePoint(mc, _tempsList[i]);
                    //显示线
                    ls.Values.Add(op);
                    if (!hasShowEV)
                    {
                        if (_tempsList[i] >= LinesDatabase.TEMP_MAX)
                        {
                            hasShowEV = true;
                            lastX = mc;
                            lastY = _tempsList[i];
                        }
                        else if (mc >= LinesDatabase.MC_MAX)
                        {
                            hasShowEV = true;
                            lastX = LinesDatabase.MC_MAX;
                            lastY = _tempsList[i];
                        }
                    }

                }
                _seriesCollection.Add(ls);
                //显示标志
                ve.X = lastX;
                ve.Y = lastY;
                _cartesianVisuals.Add(ve);
            }

        }
        public void UpdateChart(double pressure)
        {
            for (int j = 0; j < LinesDatabase.ARH_LINE_COUNT; j++)
            {
                double lastX = 0;
                double lastY = 0;
                bool hasShowEV = false;
                for (int i = 0; i < LinesDatabase.TEMP_LENGTH; i++)
                {
                    //数据计算
                    double mc = HSAtmosphere.MoistureContent(_svpList[i], _arhToShowList[j], pressure);
                    // var op = new ObservablePoint(mc, _tempsList[i]);
                    //显示线
                    ObservablePoint op = (ObservablePoint)_seriesCollection[j].Values[i];
                    op.X = mc;
                    op.Y = _tempsList[i];
                    if (!hasShowEV)
                    {
                        if (_tempsList[i] >= LinesDatabase.TEMP_MAX)
                        {
                            hasShowEV = true;
                            lastX = mc;
                            lastY = _tempsList[i];
                        }
                        else if (mc >= LinesDatabase.MC_MAX)
                        {
                            hasShowEV = true;
                            lastX = LinesDatabase.MC_MAX;
                            lastY = _tempsList[i];
                        }
                    }

                }
                //SeriesCollection.Add(ls);
                //显示标志
                _cartesianVisuals[j].X = lastX;
                _cartesianVisuals[j].Y = lastY;
                //CartesianVisuals.Add(ve);
            }
        }

        //计算函数
       
    }
}
