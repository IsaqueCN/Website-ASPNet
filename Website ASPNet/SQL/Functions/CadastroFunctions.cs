using MySql.Data.MySqlClient;
using System.Data;
using Website_ASPNet.SQL.Tables;

namespace Website_ASPNet.SQL.Functions
{
    public class CadastroFunctions
    {
        // Class that provides SQL functions for the Cadastro table

        Database db;

        public CadastroFunctions(Database db)
        {
            this.db = db;
        }

        public Cadastro readerToCadastro(MySqlDataReader reader)
        {
            return new Cadastro
            {
                ID = reader.GetInt32("ID"),
                Nome = reader.GetString("Nome")
            };
        }
        // Returns a dictionary of fields 'Nome' and 'ID'
        public List<Cadastro> Get()
        {
            List<Cadastro> cadastros = new List<Cadastro>();
            using (MySqlConnection connection = db.GetConnection())
            {
                connection.Open();
                
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM Cadastro", connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cadastros.Add(readerToCadastro(reader));
                    }
                }
            }
            return cadastros;
        }

        // Gets field 'Nome' of the specified ID
        public Cadastro GetById(int id)
        {
            using (MySqlConnection connection = db.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand($"SELECT * FROM Cadastro WHERE ID = {id}", connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return readerToCadastro(reader);
                    }
                }
            }
            return null;
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
