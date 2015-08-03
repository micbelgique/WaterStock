using SummerCamp.WaterStock.Services.Models.Data;
using System;
using System.Linq;
using System.Web.Http;
using Trasys.Dev.Data;

namespace SummerCamp.WaterStock.Services.Controllers
{
    /// <summary>
    /// Management of groups
    /// </summary>
    public class GroupsController : BaseController
    {

        /// <summary>
        /// Returns a list with all group names
        /// </summary>
        /// <returns></returns>
        public Group[] GetAllGroups()
        {
            using (SqlDatabaseCommand cmd = this.DataService.GetDatabaseCommand())
            {
                cmd.CommandText.AppendLine(" SELECT AppGroupID, ");
                cmd.CommandText.AppendLine("        Name ");
                cmd.CommandText.AppendLine("   FROM AppGroup ");
                cmd.CommandText.AppendLine("  ORDER BY Name ");

                return cmd.ExecuteTable<Group>();
            }
        }

        /// <summary>
        /// Returns a list with all group names
        /// </summary>
        /// <returns></returns>
        internal Group[] GetAllGroupsWithUserID()
        {
            using (SqlDatabaseCommand cmd = this.DataService.GetDatabaseCommand())
            {
                cmd.CommandText.AppendLine(" SELECT AppGroupAppUser.AppUserID, ");
                cmd.CommandText.AppendLine("        AppGroup.AppGroupID, ");
                cmd.CommandText.AppendLine("        AppGroup.Name ");
                cmd.CommandText.AppendLine("   FROM AppGroupAppUser ");
                cmd.CommandText.AppendLine("  INNER JOIN AppGroup ON AppGroup.AppGroupID = AppGroupAppUser.AppGroupID ");

                return cmd.ExecuteTable<Group>();
            }
        }

        /// <summary>
        /// Add new groups (GroupID = 0) or update groups (GroupID != 0)
        /// </summary>
        /// <param name="groups"></param>
        public Group[] Update(Group[] groups)
        {
            foreach (Group group in groups)
            {
                using (SqlDatabaseCommand cmd = this.DataService.GetDatabaseCommand())
                {
                    cmd.CommandText.AppendLine(" IF EXISTS(SELECT * FROM AppGroup WHERE AppGroupID = @GroupID) ");
                    cmd.CommandText.AppendLine("   UPDATE AppGroup SET Name = @Name WHERE AppGroupID = @GroupID ");
                    cmd.CommandText.AppendLine(" ELSE ");
                    cmd.CommandText.AppendLine("   INSERT INTO AppGroup (Name) VALUES (@Name) ");

                    cmd.Parameters.AddWithValue("@GroupID", group.GroupID);
                    cmd.Parameters.AddWithValue("@Name", group.Name);

                    cmd.ExecuteNonQuery();
                }
            }

            return this.GetAllGroups();
        }

        [Route("api/groups/delete/{id}")]
        [HttpDelete]
        public Group[] Delete(int id)
        {
            using (SqlDatabaseCommand cmd = this.DataService.GetDatabaseCommand())
            {
                cmd.CommandText.AppendLine(" DELETE FROM AppGroup WHERE AppGroupID = @GroupID ");

                cmd.Parameters.AddWithValue("@GroupID", id);

                cmd.ExecuteNonQuery();
            }

            return this.GetAllGroups();
        }
    }
}
