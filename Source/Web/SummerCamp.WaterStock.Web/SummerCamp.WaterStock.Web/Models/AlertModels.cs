using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerCamp.WaterStock.Web.Models
{
    public class AlertModels : BaseModel
    {
        public int AlertID { get; set; }

        public UserModels User { get; set; }

        public int AppUpserID { get; set; }

        public decimal QuantityGreatherThan { get; set; }

        public DateTime SendDate { get; set; }
    }
}
