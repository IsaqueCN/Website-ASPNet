using MySql.Data.MySqlClient;
using System.Data;

namespace Website_ASPNet.SQL.Tables
{
    public class Cadastro
    {
        // Class that provides SQL functions for the Cadastro table

        Database db;

        public Cadastro(Database db)
        {
            this.db = db;
        }

        // Returns a dictionary of fields 'Nome' and 'ID'
        public Dictionary<string, int> Get()
        {
            Dictionary<string, int> cadastroRows = new Dictionary<string, int>();

            using (MySqlConnection connection = db.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT * FROM Cadastro", connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cadastroRows.Add(reader.GetFieldValue<string>("Nome"), reader.GetFieldValue<int>("ID"));
                    }
                }
            }
            return cadastroRows;
        }

        // Gets field 'Nome' of the specified ID
        public string GetById(int id)
        {
            using (MySqlConnection connection = db.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand($"SELECT * FROM Cadastro WHERE ID = {id}", connection))
                {
                    return (string)command.ExecuteScalar();
                }
            }
        }
    }
}
