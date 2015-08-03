using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Trasys.Dev.Data.Annotations;

namespace SummerCamp.WaterStock.Services.Models.Data
{
    /// <summary />
    public class Consumption
    {
        /// <summary />
        public int ConsumptionID { get; set; }

        /// <summary />
        public int StorePointID { get; set; }

        /// <summary />
        public double QuantityAdded { get; set; }

        /// <summary />
        public DateTime RecordedDate { get; set; }

        /// <summary />
        public int UserID { get; set; }

        /// <summary />
        public string UserLastname { get; set; }

        /// <summary />
        public string UserFirstname { get; set; }

        /// <summary />
        public string UserEmail { get; set; }
    }

}