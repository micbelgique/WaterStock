using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SummerCamp.WaterStock.Services.Models.Data
{
    /// <summary />
    public class StorePoint
    {
        /// <summary />
        public int StorePointID { get; set; }

        /// <summary />
        public string Name { get; set; }

        /// <summary />
        public string Reference { get; set; }

        /// <summary />
        public double Quantity { get; set; }

        /// <summary />
        public double QuantityLeft { get; set; }

        /// <summary />
        public string Location { get; set; }

        /// <summary />
        public Consumption[] Consumptions { get; set; }
    }
}