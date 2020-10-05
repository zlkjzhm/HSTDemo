using HSTDemo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FzyMVVM;
using System.Windows.Input;

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
