using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trasys.Dev.Data.Annotations;

namespace SummerCamp.WaterStock.Services.Models.Data
{
    /// <summary />
    public class User
    {
        [Column("AppUserID")]
        /// <summary />
        public int UserID { get; set; }

        /// <summary />
        public string Login { get; set; }

        /// <summary />
        public string Password { get; set; }

        /// <summary />
        public bool IsEnabled { get; set; }

        /// <summary />
        public string Email { get; set; }

        /// <summary />
        public string Role { get; set; }

        /// <summary />
        public string Lastname { get; set; }

        /// <summary />
        public string Firstname { get; set; }

        /// <summary />
        public double CreditsLeft { get; set; }

        /// <summary />
        public Group[] Groups { get; set; }

        /// <summary />
        public Badge[] Badges { get; set; }
    }
}