using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerCamp.WaterStock.MobileApp.Data
{
    

    public class StorePoint
    {
        public int StorePointID { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public float Quantity { get; set; }
        public string Location { get; set; }
        public Consumption[] Consumptions { get; set; }
    }
    

}
