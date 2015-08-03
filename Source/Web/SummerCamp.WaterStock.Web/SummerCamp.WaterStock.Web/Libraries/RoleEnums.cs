using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerCamp.WaterStock.Web.Libraries
{
    public enum RoleEnums
    {
        /// <summary>
        /// Administrator user
        /// </summary>
        Admin,
        /// <summary>
        /// Dummy user
        /// </summary>
        User,
        /// <summary>
        /// Check user
        /// </summary>
        Check
    }
    public class RoleConverter
    {
        public static RoleEnums getEnum(string value)
        {
            switch (value.ToUpper())
            {
                case "A":
                    return RoleEnums.Admin;
                case "C":
                    return RoleEnums.Check;
                default:
                    return RoleEnums.User;
            }
        }
    }
}
