using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SummerCamp.WaterStock.Web.Models
{
    public class UserModels : BaseModel
    {
        public int AppUserID { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool IsEnabled { get; set; }

        public string Role
        {
            set
            {
                ProperRole = Libraries.RoleConverter.getEnum(value);
            }
        }

        public string Hash
        {
            get
            {
                // byte array representation of that string
                byte[] encodedPassword = new UTF8Encoding().GetBytes(Email);

                // need MD5 to calculate the hash
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

                // string representation (similar to UNIX format)
                return BitConverter.ToString(hash)
                   // without dashes
                   .Replace("-", string.Empty)
                   // make lowercase
                   .ToLower();
            }
        }
        public Libraries.RoleEnums ProperRole
        {
            get; set;
        }

        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public List<GroupModels> Groups { get; set; }
        public List<BadgeModels> Badges { get; set; }

    }
}
