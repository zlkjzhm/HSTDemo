using HSTDemo.Models;
using FzyMVVM;

namespace HSTDemo.ViewModels
{
    class HSAtmosphereViewModel : ObservableObject
    {
        public HSAtmosphereViewModel()
        {
            _atmosphere = new HSAtmosphere();
        }

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
        public double RelativeHumidity
        {
            get { return _atmosphere.AirRelativeHumidity; }
            set 
            {
                _atmosphere.AirRelativeHumidity = value;
                RaisePropertyChanged("RelativeHumidity");
            }
        }



    }
}
