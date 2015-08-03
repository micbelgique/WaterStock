using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SummerCamp.WaterStock.MobileApp.Data
{
    
    public class User
    {
        public int UserID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsEnabled { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public Group[] Groups { get; set; }
        public Badge[] Badges { get; set; }
        public double CreditsLeft { get; set; }
    }

   

    public class Badge
    {
        public int BadgeID { get; set; }
        public string Reference { get; set; }
    }



}
