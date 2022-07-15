using Milestone_cst_350.Models;
using System.Data;
using System.Data.SqlClient;

namespace Milestone_cst_350.Services
{
    public class AccountDAO : DataService
    {
        // Default Constructor
        public AccountDAO()
        {
            // ...
        }

        /// <summary>
        /// Attempt to Create a User in the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>true on success, false on failure</returns>
        /// This method will create a user through inserting data using sql statements
        public bool CreateUser(UserModel user)
        {
            Console.WriteLine($"CreateUser: {user}");

            string stmt = "INSERT INTO dbo.users (firstname, lastname, sex, age, state, email, username, password) " +
                "VALUES (@firstname, @lastname, @sex, @age, @state, @email, @username, @password)";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(stmt, con);

                cmd.Parameters.Add("@firstname", SqlDbType.VarChar, 40).Value = user.Firstname;
                cmd.Parameters.Add("@lastname", SqlDbType.VarChar, 40).Value = user.Lastname;
                cmd.Parameters.Add("@sex", SqlDbType.VarChar, 40).Value = user.Sex;
                cmd.Parameters.Add("@age", SqlDbType.Int).Value = user.Age;
                cmd.Parameters.Add("@state", SqlDbType.VarChar, 40).Value = user.State;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 40).Value = user.Email;
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 40).Value = user.Username;
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 40).Value = user.Password;

                // If error occurs in the process, the error msg will print on the console
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

        // Will use an SQL select statement to retrieve a user's information using their username
        public UserModel? GetUserByUsername(string username)
        {
            Console.WriteLine($"GetUserByUsername: {username}");

            string stmt = "SELECT TOP 1 * FROM dbo.users WHERE username=@username";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(stmt, con);

                cmd.Parameters.Add("@username", SqlDbType.VarChar, 40).Value = username;

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        return new UserModel(reader);
                    }
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return null;
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

                cmd.Parameters.Add("@username", SqlDbType.VarChar, 40).Value = user.Username;
                cmd.Parameters.Add("@password", SqlDbType.VarChar, 40).Value = user.Password;

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
