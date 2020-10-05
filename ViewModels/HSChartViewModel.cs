using System.Collections.ObjectModel;
using System.Windows.Input;
using FzyMVVM;
using HSTDemo.Models;
using LiveCharts;

namespace HSTDemo.ViewModels
{
    class HSChartViewModel : ObservableObject 
    {
        private ObservableCollection<LineViewModelBase> _lines;
        private HSAtmosphereViewModel _atmosphere;
        private HSChart _chart;

        public HSChartViewModel()
        {
            _atmosphere = new HSAtmosphereViewModel();
            _chart = new HSChart();
            _lines = new ObservableCollection<LineViewModelBase>
            {
                new HSARHViewModel(_atmosphere.Pressure, SeriesCollection, CartesianVisuals)
            };

        }

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

        public double Pressure
        {
            get { return _atmosphere.Pressure; }
            set
            {
                _atmosphere.Pressure = value;
                RaisePropertyChanged("Pressure");
            }
        }


        void UpdatePressureExecute()
        {
            HSARHViewModel arhline = (HSARHViewModel)_lines[0];
            arhline.UpdateChart(Pressure);
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
