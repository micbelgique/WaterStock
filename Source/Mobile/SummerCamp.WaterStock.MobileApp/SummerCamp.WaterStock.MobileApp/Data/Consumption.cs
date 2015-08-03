using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerCamp.WaterStock.MobileApp.Data
{
    
    public class Consumption
    {
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
    }

}
