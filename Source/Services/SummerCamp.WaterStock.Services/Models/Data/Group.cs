using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Trasys.Dev.Data.Annotations;

namespace SummerCamp.WaterStock.Services.Models.Data
{
    /// <summary />
    public class Group
    {
        /// <summary />
        [Column("AppGroupID")]
        public int GroupID { get; set; }

        /// <summary />
        [IgnoreDataMember()]
        public int AppUserID { get; set; }

        /// <summary />
        public string Name { get; set; }
    }
}