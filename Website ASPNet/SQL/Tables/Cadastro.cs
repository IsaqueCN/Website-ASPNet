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

        // Inserts to table and returns number of rows affected
        public int Post(string name)
        {
            using (MySqlConnection connection = db.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand($"INSERT INTO Cadastro(Nome) Values('{name}')", connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }

        // Deletes from table and returns the number of rows affected
        public int Delete(int id)
        {
            using (MySqlConnection connection = db.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand($"DELETE FROM Cadastro WHERE ID = {id}", connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}
