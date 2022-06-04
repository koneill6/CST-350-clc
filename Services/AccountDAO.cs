using Milestone_cst_350.Models;

namespace Milestone_cst_350.Services
{
    public class AccountDAO : DataService
    {
        public AccountDAO()
        {
            // ...
        }

        /// <summary>
        /// Attempt to Create a User in the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>true on success, false on failure</returns>
        public bool CreateUser(UserModel user)
        {
            return false;
        }

        /// <summary>
        /// Attempt to Authenticate a User in the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>true on success, false on failure</returns>
        public bool AuthenticateUser(UserModel user)
        {
            return false;
        }
    }
}
