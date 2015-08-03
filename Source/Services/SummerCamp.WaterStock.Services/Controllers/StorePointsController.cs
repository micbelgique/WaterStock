using SummerCamp.WaterStock.Services.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Trasys.Dev.Data;

namespace SummerCamp.WaterStock.Services.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class StorePointsController : BaseController
    {
        /// <summary>
        /// Returns a list with all store points existing in the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StorePoint> GetAllStorePoint()
        {
            StorePoint[] storepoints;

            using (SqlDatabaseCommand cmd = this.DataService.GetDatabaseCommand())
            {
                cmd.CommandText.AppendLine(" SELECT StorePointID, ");
                cmd.CommandText.AppendLine("        Name, ");
                cmd.CommandText.AppendLine("        Reference, ");
                cmd.CommandText.AppendLine("        Quantity, ");
                cmd.CommandText.AppendLine("        Location, ");
                cmd.CommandText.AppendLine("         (SELECT Quantity - SUM(QuantityAdded) FROM Consumption WHERE StorePointID = StorePoint.StorePointID) AS QuantityLeft ");
                cmd.CommandText.AppendLine("   FROM StorePoint ");

                storepoints = cmd.ExecuteTable<StorePoint>();
            }

            foreach (StorePoint store in storepoints)
            {
                store.Consumptions = this.GetLast30Consumptions(store.StorePointID).ToArray();
            }

            return storepoints;
        }

        /// <summary>
        /// Returns the 30 lastest Consumption
        /// </summary>
        /// <param name="storePointID"></param>
        /// <returns></returns>
        private IEnumerable<Consumption> GetLast30Consumptions(int storePointID)
        {
            using (SqlDatabaseCommand cmd = this.DataService.GetDatabaseCommand())
            {
                cmd.CommandText.AppendLine(" SELECT TOP 30 ");
                cmd.CommandText.AppendLine("        Consumption.ConsumptionID, ");
                cmd.CommandText.AppendLine("        Consumption.StorePointID, ");
                cmd.CommandText.AppendLine("        Consumption.RecordedDate, ");
                cmd.CommandText.AppendLine("        Consumption.QuantityAdded, ");
                cmd.CommandText.AppendLine("        Consumption.AppUserID AS UserID, ");
                cmd.CommandText.AppendLine("        AppUser.Lastname AS UserLastname, ");
                cmd.CommandText.AppendLine("        AppUser.Firstname AS UserFirstname,");
                cmd.CommandText.AppendLine("        AppUser.Email AS UserEmail");
                cmd.CommandText.AppendLine("   FROM Consumption ");
                cmd.CommandText.AppendLine("  INNER JOIN AppUser ON Consumption.AppUserID = AppUser.AppUserID ");
                cmd.CommandText.AppendLine("  WHERE StorePointID = @storePointID ");
                cmd.CommandText.AppendLine("  ORDER BY Consumption.RecordedDate ");

                cmd.Parameters.AddWithValue("@storePointID", storePointID);

                return cmd.ExecuteTable<Consumption>().AsEnumerable();
            }
        }

    }
}
