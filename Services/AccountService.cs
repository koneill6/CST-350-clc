using Milestone_cst_350.Models;

namespace Milestone_cst_350.Services
{
    public class AccountService
    {
        private AccountDAO _accountDAO;

        public AccountService()
        {
            _accountDAO = new AccountDAO();
        }

        /// <summary>
        /// Attempt to Register a User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>TODO: Return ErrorModel</returns>
        public bool RegisterUser(UserModel user)
        {
            return _accountDAO.CreateUser(user);
        }

        /// <summary>
        /// Attempt to Authenticate a User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>TODO: Return ErrorModel</returns>
        public bool AuthenticateUser(UserModel user)
        {
            return _accountDAO.AuthenticateUser(user);
        }

        /// <summary>
        /// Attempt to Get a User by Username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>UserModel or null</returns>
        public UserModel? GetUserByUsername(string username)
        {
            return _accountDAO.GetUserByUsername(username);
        }
    }
}
