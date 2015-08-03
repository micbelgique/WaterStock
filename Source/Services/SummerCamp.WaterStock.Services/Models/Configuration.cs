using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SummerCamp.WaterStock.Services.Models
{
    public class Configuration
    {
        /// <summary>
        /// Initializes a new instance of Configuration
        /// </summary>
        /// <param name="service"></param>
        public Configuration(DataService service)
        {
            this.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }

        /// <summary>
        /// Get the Connection String to find SQL Database
        /// </summary>
        public string ConnectionString { get; private set; }
    }
}