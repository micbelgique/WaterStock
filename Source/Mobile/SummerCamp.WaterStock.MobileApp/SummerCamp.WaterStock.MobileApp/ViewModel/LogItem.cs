using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerCamp.WaterStock.MobileApp.ViewModel
{
    public class LogItem : INotifyPropertyChanged
    {
        private double _volume;

        public double Volume
        {
            get { return _volume; }
            set
            {
                if (_volume.CompareTo(value) != 0)
                {
                    _volume = value;
                    OnPropertyChanged("Volume");
                    OnPropertyChanged("FormattedVolume");
                }
            }
        }

        public string VolumeFormatted
        {
            get
            {
                return string.Format("{0} L",Volume);
            }
        }

        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date.CompareTo(value) != 0)
                {
                    _date = value;
                    OnPropertyChanged("Date");
                    OnPropertyChanged("FormattedDate");
                }
            }
        }

        public string FormattedDate
        {
            get
            {
                return string.Format("{0:f}",Date);
            }
        }

        private string _adress;

        public string Adress
        {
            get { return _adress; }
            set
            {
                if (_adress?.CompareTo(value) != 0)
                {
                    _adress = value;
                    OnPropertyChanged("Adress");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
