using MySql.Data.MySqlClient;
using System.Data;
using Website_ASPNet.SQL.Tables;

namespace Website_ASPNet.SQL.Functions
{
    public class UserFunctions
    {
        // Class that provides SQL functions for the User table

        Database db;

        public UserFunctions(Database db)
        {
            this.db = db;
        }

        private string? getReaderString(MySqlDataReader reader, string name)
        {
            return reader.IsDBNull(reader.GetOrdinal(name)) ? null : reader.GetString(name);
        }
        public User readerToUser(MySqlDataReader reader)
        {
            return new User
            {
                ID = reader.GetInt32("ID"),
                Name = getReaderString(reader, "Name"),
                Email = getReaderString(reader, "Email"),
                Password = getReaderString(reader, "Password"),
                Creation_Date = reader.GetDateTime("Creation_Date")
            };
        }
        // Returns a list of every element in the User table
        public List<User> Get()
        {
            List<User> users = new List<User>();
            using (MySqlConnection connection = db.GetConnection())
            {
                connection.Open();
                
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM User", connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(readerToUser(reader));
                    }
                }
            }
            return users;
        }

        // Gets User with specified ID
        public User GetById(int id)
        {
            using (MySqlConnection connection = db.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand($"SELECT * FROM User WHERE ID = @ID", connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return readerToUser(reader);
                        }
                    }
                }
            }
            return null;
        }

        // Inserts to table and returns number of rows affected
        public int Post(string name, string password, string? email)
        {
            using (MySqlConnection connection = db.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand($"INSERT INTO User(Name, Password, Email) Values(@Name, @Password, @Email)", connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
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

                using (MySqlCommand command = new MySqlCommand($"DELETE FROM User WHERE ID = @ID", connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}
