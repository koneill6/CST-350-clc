using Milestone_cst_350.Models;
using System.Data.SqlClient;

namespace Milestone_cst_350.Services
{
    public class SaveGameDAO : DataService
    {
        public List<SaveGameModel> AllGames()
        {
            List<SaveGameModel> foundGames = new List<SaveGameModel>();

            string sqlstatment = "select * from dbo.savegame";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlstatment, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundGames.Add(new SaveGameModel((int)reader[0], (int)reader[1], (DateTime)reader[2], (string)reader[3]));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }


            return foundGames;
        }

        public int SaveGame(SaveGameModel game)
        {
            int rows = 0;

            string statement = "INSERT INTO dbo.savegame (user_id, datetime, savestate) VALUES (@user_id, @savestate)";

            using (SqlConnection connection = new(ConnectionString))
            {
                // Add parameters to command
                SqlCommand command = new(statement, connection);
                command.Parameters.AddWithValue("@user_id", game.user_id);
                command.Parameters.AddWithValue("@savestate", game.save_state);

                try
                {
                    // Open connection and execute command
                    connection.Open();
                    rows = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return rows;
        }

        public List<SaveGameModel> GetAllGamesByUserId(int userId)
        {
            // All games
            List<SaveGameModel> foundGames = new List<SaveGameModel>();

            // Sql statement to execute
            string sqlstatment = "SELECT * FROM dbo.savegame WHERE user_id=@user_id";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlstatment, connection);
                command.Parameters.AddWithValue("@user_id", userId);

                try
                {
                    // Open connection and execute command
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundGames.Add(new SaveGameModel((int)reader[0], (int)reader[1], (DateTime)reader[2], (string)reader[3]));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return foundGames;
        }

        public SaveGameModel GetGameById(int id)
        {
            SaveGameModel foundGame = null;
            string sqlStatement = "select * from dbo.savegame where Id = @id";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundGame = new SaveGameModel((int)reader[0], (int)reader[1], (DateTime)reader[2], (string)reader[3]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return foundGame;
        }

        public int DeleteGameById(int id)
        {
            int rowsImpacted = 0;
            string sqlStatement = "delete from dbo.savegame where Id = @id";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    rowsImpacted = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }


            return rowsImpacted;
        }
    }
}
