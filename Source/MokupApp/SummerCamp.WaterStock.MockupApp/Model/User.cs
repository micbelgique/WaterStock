using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerCamp.WaterStock.MockupApp.Model
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
        public float CreditsLeft { get; set; }
        public Group[] Groups { get; set; }
        public Badge[] Badges { get; set; }
    }

    public class Group
    {
        public int GroupID { get; set; }
        public string Name { get; set; }
    }

    public class Badge
    {
        public int BadgeID { get; set; }
        public string Reference { get; set; }
    }

}
