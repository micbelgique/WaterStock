using SummerCamp.WaterStock.Services.Models.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Trasys.Dev.Data;

namespace SummerCamp.WaterStock.Services.Controllers
{
    /// <summary>
    /// Management of users
    /// </summary>
    public class UsersController : BaseController
    {
        /// <summary>
        /// Returns a list with all users existing in the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetAllUsers()
        {
            User[] users;
            
            // Users
            using (SqlDatabaseCommand cmd = this.DataService.GetDatabaseCommand())
            {
                cmd.CommandText.AppendLine(" SELECT AppUserID,  ");
                cmd.CommandText.AppendLine("        Login,      ");
                cmd.CommandText.AppendLine("        Password,   ");
                cmd.CommandText.AppendLine("        Email,      ");
                cmd.CommandText.AppendLine("        IsEnabled,  ");
                cmd.CommandText.AppendLine("        Role,       ");
                cmd.CommandText.AppendLine("        Lastname,   ");
                cmd.CommandText.AppendLine("        Firstname,   ");
                cmd.CommandText.AppendLine("        ISNULL((SELECT SUM(QuantityAdded) FROM Consumption WHERE Consumption.AppUserID = AppUser.AppUserID),0) AS CreditsLeft ");
                cmd.CommandText.AppendLine("   FROM AppUser     ");

                users = cmd.ExecuteTable<User>();
            }

            // Badges
            Badge[] badges = new BadgesController().GetAllBadges();

            // Groups
            Group[] groups = new GroupsController().GetAllGroupsWithUserID();

            // Mixing
            foreach (User user in users)
            {
                user.Badges = badges.Where(b => b.AppUserID == user.UserID).ToArray();
                user.Groups = groups.Where(g => g.AppUserID == user.UserID).ToArray();
            }

            return users.AsEnumerable();
        }

        /// <summary>
        /// Return a User information based on his login and password.
        /// </summary>
        /// <param name="login">Login of this user</param>
        /// <param name="password">Password of this user</param>
        /// <returns></returns>
        [Route("api/users/{login}/{password}")]
        public User GetUser(string login, string password)
        {
            return this.GetAllUsers().FirstOrDefault(u => String.Compare(login, u.Login, StringComparison.CurrentCultureIgnoreCase) == 0 &&
                                                               String.Compare(password, u.Password, StringComparison.CurrentCultureIgnoreCase) == 0);
        }

        /// <summary>
        /// Return a User information based on his badge reference
        /// </summary>
        /// <param name="id">Badge reference of user</param>
        /// <returns></returns>
        [Route("api/users/badge/{id}")]
        public User GetUserFromBadge(string id)
        {
            return this.GetAllUsers().FirstOrDefault(u => u.Badges.Any(b => b.Reference == id));
        }

        /// <summary>
        /// Return a User information based on his UserID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns></returns>
        public User GetUser(int id)
        {
            return this.GetAllUsers().FirstOrDefault(u => u.UserID == id);
        }

        /// <summary>
        /// Add new user (UserID = 0) or update a user (UserID != 0)
        /// </summary>
        /// <param name="user"></param>
        public User Update(User user)
        {
            using (SqlDatabaseCommand cmd = this.DataService.GetDatabaseCommand())
            {
                cmd.CommandText.AppendLine(" IF EXISTS(SELECT * FROM AppUser WHERE AppUserID = @UserID) ");
                cmd.CommandText.AppendLine("   BEGIN ");
                cmd.CommandText.AppendLine("      UPDATE AppUser ");
                cmd.CommandText.AppendLine("         SET Login = @Login, ");
                cmd.CommandText.AppendLine("             Password = @Password, ");
                cmd.CommandText.AppendLine("             Email = @Email, ");
                cmd.CommandText.AppendLine("             IsEnabled = @IsEnabled, ");
                cmd.CommandText.AppendLine("             Role = @Role, ");
                cmd.CommandText.AppendLine("             Lastname = @Lastname, ");
                cmd.CommandText.AppendLine("             Firstname = @Firstname ");
                cmd.CommandText.AppendLine("       WHERE AppUserID = @UserID; ");
                cmd.CommandText.AppendLine("      SELECT @UserID; ");
                cmd.CommandText.AppendLine("   END ");
                cmd.CommandText.AppendLine(" ELSE ");
                cmd.CommandText.AppendLine("   BEGIN ");
                cmd.CommandText.AppendLine("      INSERT INTO AppUser (Login, Password, Email, IsEnabled, Role, Lastname, Firstname) ");
                cmd.CommandText.AppendLine("                  VALUES (@Login, @Password, @Email, @IsEnabled, @Role, @Lastname, @Firstname); ");
                cmd.CommandText.AppendLine("      SELECT SCOPE_IDENTITY(); ");
                cmd.CommandText.AppendLine("   END ");

                cmd.Parameters.AddWithValue("@UserID", user.UserID);
                cmd.Parameters.AddWithValue("@Login", user.Login);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@IsEnabled", user.IsEnabled);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.Parameters.AddWithValue("@Lastname", user.Lastname);
                cmd.Parameters.AddWithValue("@Firstname", user.Firstname);

                int newid = cmd.ExecuteScalar<int>();
                return this.GetUser(newid);
            }

            
        }

        /// <summary>
        /// Delete the User, based on his ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/users/delete/{id}")]
        [HttpDelete]
        public IEnumerable<User> Delete(int id)
        {
            using (SqlDatabaseCommand cmd = this.DataService.GetDatabaseCommand())
            {
                cmd.CommandText.AppendLine(" DELETE FROM AppUser WHERE AppUserID = @UserID ");

                cmd.Parameters.AddWithValue("@UserID", id);

                cmd.ExecuteNonQuery();
            }

            return this.GetAllUsers();
        }

        #region INTERNAL CLASSES

        /// <summary />
        private class UserBadge
        {
            /// <summary />
            public int AppUserID { get; set; }
            /// <summary />
            public string Reference { get; set; }
        }

        /// <summary />
        private class UserGroup
        {
            /// <summary />
            public int AppUserID { get; set; }
            /// <summary />
            public string Name { get; set; }
        }
        #endregion
    }
}
