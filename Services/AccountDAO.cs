using Milestone_cst_350.Models;
using System.Data.SqlClient;

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
            Console.WriteLine($"CreateUser: {user}");

            string stmt = "INSERT INTO dbo.users (firstname, lastname, sex, age, state, email, username, password) " +
                "VALUES (@firstname, @lastname, @sex, @age, @state, @email, @username, @password)";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(stmt, con);

                cmd.Parameters.Add("@firstname", System.Data.SqlDbType.VarChar, 40).Value = user.Firstname;
                cmd.Parameters.Add("@lastname", System.Data.SqlDbType.VarChar, 40).Value = user.Lastname;
                cmd.Parameters.Add("@sex", System.Data.SqlDbType.VarChar, 40).Value = user.Sex;
                cmd.Parameters.Add("@age", System.Data.SqlDbType.Int).Value = user.Age;
                cmd.Parameters.Add("@state", System.Data.SqlDbType.VarChar, 40).Value = user.State;
                cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 40).Value = user.Email;
                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.Username;
                cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 40).Value = user.Password;

                try
                {
                    con.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        return true;
                    }
                } catch (Exception ex)
                {
                    // TODO: Determine error & return ErrorModel.
                    Console.WriteLine(ex.Message);
                }
            }


            return false;
        }

        /// <summary>
        /// Attempt to Authenticate a User in the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>true on success, false on failure</returns>
        public bool AuthenticateUser(UserModel user)
        {
            Console.WriteLine($"Authenticate User: {user}");

            string stmt = "SELECT * FROM dbo.users WHERE username=@username AND password=@password";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(stmt, con);

                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.Username;
                cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 40).Value = user.Password;

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        return true;
                    }
                } catch (Exception ex)
                {
                    // TODO: Determine error & return ErrorModel.
                    Console.WriteLine(ex.Message);
                }
            }

            return false;
        }
    }
}
