using Milestone_cst_350.Models;
using System.Data.SqlClient;

namespace Milestone_cst_350.Services
{
    public class SaveGameDAO
    {

        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<SaveGameModel> AllProducts()
        {
            List<SaveGameModel> foundproducts = new List<SaveGameModel>();

            string sqlstatment = "select * from dbo.savegame";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlstatment, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundproducts.Add(new SaveGameModel((int)reader[0], (int)reader[1], (DateTime)reader[2], (string)reader[3]));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }


            return foundproducts;
        }


        public SaveGameModel GetGameById(int id)
        {
            SaveGameModel foundProduct = null;
            string sqlStatement = "select * from dbo.savegame where Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundProduct = new SaveGameModel((int)reader[0], (int)reader[1], (DateTime)reader[2], (string)reader[3]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            return foundProduct;
        }

        public int DeleteProductById(int id)
        {
            int rowsImpacted = 0;
            string sqlStatement = "delete from dbo.savegame where Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
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
