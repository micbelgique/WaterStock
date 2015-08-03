using SummerCamp.WaterStock.Services.Models.Data;
using System;
using System.Web.Http;
using Trasys.Dev.Data;

namespace SummerCamp.WaterStock.Services.Controllers
{
    /// <summary>
    /// Management of badges
    /// </summary>
    public class BadgesController : BaseController
    {
        /// <summary>
        /// Returns a list with all group names
        /// </summary>
        /// <returns></returns>
        public Badge[] GetAllBadges()
        {
            // Groups
            using (SqlDatabaseCommand cmd = this.DataService.GetDatabaseCommand())
            {
                cmd.CommandText.AppendLine(" SELECT AppUserID, ");
                cmd.CommandText.AppendLine("        BadgeID, ");
                cmd.CommandText.AppendLine("        Reference ");
                cmd.CommandText.AppendLine("   FROM Badge ");
                cmd.CommandText.AppendLine("  ORDER BY Reference ");

                return cmd.ExecuteTable<Badge>();
            }
        }

        /// <summary>
        /// Add new badges (BadgeID = 0) or update badges (BadgeID != 0)
        /// </summary>
        /// <param name="groups"></param>
        public Badge[] Update(Badge[] badges)
        {
            foreach (Badge badge in badges)
            {
                using (SqlDatabaseCommand cmd = this.DataService.GetDatabaseCommand())
                {
                    cmd.CommandText.AppendLine(" IF EXISTS(SELECT * FROM Badge WHERE BadgeID = @BadgeID) ");
                    cmd.CommandText.AppendLine("   UPDATE Badge SET Reference = @Reference WHERE BadgeID = @BadgeID ");
                    cmd.CommandText.AppendLine(" ELSE ");
                    cmd.CommandText.AppendLine("   INSERT INTO Badge (Reference) VALUES (@Reference) ");

                    cmd.Parameters.AddWithValue("@BadgeID", badge.BadgeID);
                    cmd.Parameters.AddWithValue("@Reference", badge.Reference);

                    cmd.ExecuteNonQuery();
                }
            }

            return this.GetAllBadges();
        }

        [Route("api/badges/delete/{id}")]
        [HttpDelete]
        public Badge[] Delete(int id)
        {
            using (SqlDatabaseCommand cmd = this.DataService.GetDatabaseCommand())
            {
                cmd.CommandText.AppendLine(" DELETE FROM Badge WHERE BadgeID = @BadgeID ");

                cmd.Parameters.AddWithValue("@BadgeID", id);

                cmd.ExecuteNonQuery();
            }

            return this.GetAllBadges();
        }
    }
}
