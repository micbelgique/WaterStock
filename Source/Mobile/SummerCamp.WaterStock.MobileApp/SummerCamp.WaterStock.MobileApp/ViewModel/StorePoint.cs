using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerCamp.WaterStock.MobileApp.ViewModel
{
    public class StorePoint : INotifyPropertyChanged
    {
        private string _name;
        private string _reference;
        private double _quantity;
        private double _quantityUsed;
        private string _location;
        private ObservableCollection<Consumption> _consumptions;

        public StorePoint(Data.StorePoint storepoint)
        {
            Load(storepoint);
        }

        public int StorePointID { get; set; }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name?.CompareTo(value) != 0)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Reference
        {
            get
            {
                return _reference;
            }
            set
            {
                if (_reference?.CompareTo(value) != 0)
                {
                    _reference = value;
                    OnPropertyChanged(nameof(Reference));
                }
            }
        }

        public double Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (_quantity.CompareTo(value) != 0)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                    OnPropertyChanged(nameof(QuantityUsedFormatted));
                }
            }
        }

        public double QuantityUsed
        {
            get
            {
                return _quantityUsed;
            }
            set
            {
                if (_quantityUsed.CompareTo(value) != 0)
                {
                    _quantityUsed = value;
                    OnPropertyChanged(nameof(QuantityUsed));
                    OnPropertyChanged(nameof(QuantityUsedFormatted));
                }
            }
        }

        public string QuantityUsedFormatted
        {
            get
            {
                return $"{QuantityUsed} L";
            }
        }

        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                if (_location?.CompareTo(value) != 0)
                {
                    _location = value;
                    OnPropertyChanged(nameof(Location));
                }
            }
        }

        public ObservableCollection<Consumption> Consumptions
        {
            get
            {
                if(_consumptions==null)
                {
                    _consumptions = new ObservableCollection<Consumption>();
                }
                return _consumptions;
            }
            set
            {
                _consumptions = value;
            }
        }

        public void Load(Data.StorePoint store)
        {
            this.StorePointID = store.StorePointID;
            this.Name = store.Name;
            this.Reference = store.Reference;
            this.Quantity = store.Quantity;
            this.Location = store.Location;
            Load(store.Consumptions);
        }

        public void Load(Data.Consumption[] consumptions)
        {
            Consumptions.Clear();
            foreach(var consumption in consumptions)
            {
                Consumptions.Add(new Consumption(consumption));
            }
            QuantityUsed = Consumptions.Sum(c => c.QuantityAdded);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
