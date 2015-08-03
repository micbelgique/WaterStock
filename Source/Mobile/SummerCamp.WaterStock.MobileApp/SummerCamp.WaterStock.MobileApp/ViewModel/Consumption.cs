using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerCamp.WaterStock.MobileApp.ViewModel
{
    public class Consumption : INotifyPropertyChanged
    {
        public Consumption(Data.Consumption consumption)
        {
            this.UserID = consumption.UserID;
            this.RecordedDate = consumption.RecordedDate;
            this.QuantityAdded = consumption.QuantityAdded;
            this.Name = consumption.Name;
            this.Reference = consumption.Reference;
            this.Quantity = consumption.Quantity;
            this.Location = consumption.Location;
            this.ConsumptionID = consumption.ConsumptionID;
            this.StorePointID = consumption.StorePointID;
            this.UserLastname = consumption.UserLastname;
            this.UserFirstname = consumption.UserFirstname;
        }

        public int UserID { get; set; }
        public DateTime RecordedDate { get; set; }
        public double QuantityAdded { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public double Quantity { get; set; }
        public string Location { get; set; }
        public int ConsumptionID { get; set; }
        public int StorePointID { get; set; }
        public string UserLastname { get; set; }
        public string UserFirstname { get; set; }

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
