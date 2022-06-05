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
        /// Attempt to Authenticate a User.
        /// </summary>
        /// <param name="login"></param>
        /// <returns>TODO: Return ErrorModel OR Deprecate and use Authenticate(UserModel user)</returns>
        public bool AuthenticateUser(LoginModel login)
        {
            UserModel user = new UserModel
            {
                Username = login.Username,
                Password = login.Password
            };

            return _accountDAO.AuthenticateUser(user);
        }
    }
}
