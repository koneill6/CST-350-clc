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

        public void SaveGame(SaveGameModel game)
        {
            // TODO: Save Game
        }

        public List<SaveGameModel> GetAllGamesByUserId(int id)
        {
            // TODO: Get All Games
            return null;
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
