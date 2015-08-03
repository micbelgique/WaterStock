using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerCamp.WaterStock.MockupApp.Model
{

    public class StorePoint
    {
        public int StorePointID { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public float Quantity { get; set; }
        public float QuantityLeft { get; set; }
        public string Location { get; set; }
        public Consumption[] Consumptions { get; set; }
    }

    public class Consumption
    {
        public int ConsumptionID { get; set; }
        public int StorePointID { get; set; }
        public float QuantityAdded { get; set; }
        public DateTime RecordedDate { get; set; }
        public int UserID { get; set; }
        public string UserLastname { get; set; }
        public string UserFirstname { get; set; }
        public string UserEmail { get; set; }
    }

}
