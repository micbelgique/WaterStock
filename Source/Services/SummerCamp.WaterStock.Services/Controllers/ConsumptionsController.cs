using SummerCamp.WaterStock.Services.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Trasys.Dev.Data;

namespace SummerCamp.WaterStock.Services.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ConsumptionsController : BaseController
    {
        /// <summary>
        /// Returns a list with all store points existing in the database
        /// </summary>
        /// <param name="days">Number of days</param>
        /// <returns></returns>
        [Route("api/consumptions/bydays/{days}")]
        public IEnumerable<ConsumptionsByDay> GetConsumptionsByDay(int days)
        {
            using (SqlDatabaseCommand cmd = this.DataService.GetDatabaseCommand())
            {

                cmd.CommandText.AppendLine(" SELECT CONVERT(DATE, RecordedDate) AS Date, SUM(QuantityAdded) AS Quantity ");
                cmd.CommandText.AppendLine("   FROM Consumption ");
                cmd.CommandText.AppendLine("  WHERE RecordedDate > GETDATE() - @days ");
                cmd.CommandText.AppendLine(" GROUP BY CONVERT(DATE, RecordedDate) ");

                cmd.Parameters.AddWithValue("@days", days <= 0 ? 30 : days);

                return cmd.ExecuteTable<ConsumptionsByDay>().AsEnumerable();
            }
        }

        /// <summary>
        /// Returns a list with all store points existing in the database
        /// </summary>
        /// <param name="userID">ID of user</param>
        /// <param name="days">Number of days</param>
        /// <returns></returns>
        [Route("api/consumptions/{userid}/{days}")]
        public IEnumerable<ConsumptionsForUser> GetConsumptionsForUser(int userID, int days)
        {
            using (SqlDatabaseCommand cmd = this.DataService.GetDatabaseCommand())
            {

                cmd.CommandText.AppendLine(" SELECT Consumption.AppUserID AS UserID, ");
                cmd.CommandText.AppendLine("        Consumption.RecordedDate, ");
                cmd.CommandText.AppendLine("        Consumption.QuantityAdded, ");
                cmd.CommandText.AppendLine("        StorePoint.Name, ");
                cmd.CommandText.AppendLine("        StorePoint.Reference, ");
                cmd.CommandText.AppendLine("        StorePoint.Quantity, ");
                cmd.CommandText.AppendLine("        StorePoint.Location ");
                cmd.CommandText.AppendLine("   FROM Consumption ");
                cmd.CommandText.AppendLine("  INNER JOIN StorePoint ON StorePoint.StorePointID = Consumption.StorePointID ");
                cmd.CommandText.AppendLine("  WHERE RecordedDate > GETDATE() - @days ");
                cmd.CommandText.AppendLine("    AND AppUserID = @userid ");
                cmd.CommandText.AppendLine("  ORDER BY Consumption.RecordedDate ");

                cmd.Parameters.AddWithValue("@days", days <= 0 ? 30 : days);
                cmd.Parameters.AddWithValue("@userid", userID);

                return cmd.ExecuteTable<ConsumptionsForUser>().AsEnumerable();
            }
        }

        /// <summary>
        /// Create a consumption
        ///     Quantity consumed must uses milliliters
        /// </summary>
        /// <param name="userID">Id of the user</param>
        [Route("api/consumptions/{userid}/add/{storePointReference}/{quantityConsumed}")]
        [HttpGet]
        public StorePoint Add(int userID,string storePointReference,int quantityConsumed)
        {
            using (SqlDatabaseCommand cmd = this.DataService.GetDatabaseCommand())
            {
                StorePoint foundStorePoint = new StorePointsController().GetAllStorePoint().FirstOrDefault(s => s.Reference == storePointReference);
                if(foundStorePoint == null)
                {
                    throw new ArgumentException(String.Format("Storepoint {0} not found", storePointReference));
                }
                cmd.CommandText.AppendLine(" INSERT INTO[Consumption]([RecordedDate],[StorePointID],[AppUserID],[QuantityAdded]) ");
                cmd.CommandText.AppendLine("    VALUES(GETDATE(), @StorePointID, @AppUserID, @QuantityAdded) ");

                cmd.Parameters.AddWithValue("@StorePointID", foundStorePoint.StorePointID);
                cmd.Parameters.AddWithValue("@AppUserID", userID);
                cmd.Parameters.AddWithValue("@QuantityAdded", quantityConsumed / 1000f);

                cmd.ExecuteNonQuery();
                return new StorePointsController().GetAllStorePoint().FirstOrDefault(s => s.Reference == storePointReference); ;
            }
        }


        /// <summary />
        public class ConsumptionsByDay
        {
            /// <summary />
            public DateTime Date { get; set; }

            /// <summary />
            public double Quantity { get; set; }
        }

        public class ConsumptionsForUser
        {
            public int UserID { get; set; }
            public DateTime RecordedDate { get; set; }
            public double QuantityAdded { get; set; }
            public string Name { get; set; }
            public string Reference { get; set; }
            public double Quantity { get; set; }
            public string Location { get; set; }
        }

    }
}