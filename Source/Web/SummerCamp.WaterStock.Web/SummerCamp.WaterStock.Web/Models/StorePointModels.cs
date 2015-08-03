using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SummerCamp.WaterStock.Web.Models
{
    public class StorePointHomeModels
    {
        public List<StorePointModels> StorePoints { get; set; }
        public List<ConsumptionsLite> History { get; set; }
    }

    public class ConsumptionsLite
    {
        public decimal Quantity { get; set; }
        public DateTime Date { get; set; }
    }

    public class StorePointModels : BaseModel
    {
        public int StorePointID { get; set; }

        public string Name { get; set; }

        public string Reference { get; set; }

        public decimal Quantity { get; set; }

        public decimal QuantityLeft { get; set; }

        public decimal PercentLeft
        {
            get
            {
                if (Quantity > 0)
                {
                    return (QuantityLeft / Quantity) * 100;
                }
                else
                {
                    return 100;
                }
            }
        }

        public string Location { get; set; }

        public List<Consumption> Consumptions { get; set; }
    }

    public class Consumption
    {
        public int ConsumptionID { get; set; }
        public int StorePointID { get; set; }
        public double QuantityAdded { get; set; }
        public DateTime RecordedDate { get; set; }
        public int UserID { get; set; }
        public string UserLastname { get; set; }
        public string UserFirstname { get; set; }
        public string UserEmail { get; set; }
        public string Hash
        {
            get
            {
                // byte array representation of that string
                byte[] encodedPassword = new UTF8Encoding().GetBytes(UserEmail);

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
    }
}
