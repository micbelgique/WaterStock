using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SummerCamp.WaterStock.Services.Models.Data
{
    /// <summary />
    public class Badge
    {
        /// <summary />
        public int BadgeID { get; set; }

        /// <summary />
        [IgnoreDataMember()]
        public int AppUserID { get; set; }

        /// <summary />
        public string Reference { get; set; }
    }
}