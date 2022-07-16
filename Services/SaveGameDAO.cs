using Milestone_cst_350.Models;
using System.Data.SqlClient;

namespace Milestone_cst_350.Services
{
    public class SaveGameDAO : DataService
    {
        /// <summary>
        /// Stores all the games in the DAO
        /// </summary>
        /// <returns>will return all the games, else false if there is an error</returns>
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

        /// <summary>
        /// Saves a game and stores it into a database 
        /// </summary>
        /// <param name="game"></param>
        /// <returns>displays the number of rows during the sql injection</returns>
        public int SaveGame(SaveGameModel game)
        {
            int rows = 0;

            string statement = "INSERT INTO dbo.savegame (user_id, datetime, savestate) VALUES (@user_id, @datetime, @savestate)";

            using (SqlConnection connection = new(ConnectionString))
            {
                // Add parameters to command
                SqlCommand command = new(statement, connection);

                command.Parameters.AddWithValue("@user_id", game.user_id);
                command.Parameters.AddWithValue("@datetime", game.save_date);
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

        /// <summary>
        /// Retrives all the games using the user's ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>the list of games</returns>
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

        /// <summary>
        /// Retrieve a game by it's ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns> the found game </returns>
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

        /// <summary>
        /// Deletes a game by it's ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>returns the row that the game was deleted from</returns>
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
