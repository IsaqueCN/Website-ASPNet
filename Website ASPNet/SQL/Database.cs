using MySql.Data.MySqlClient;

namespace Website_ASPNet.SQL
{
    // Class used for getting a connection from the MySQL database
    public class Database
    {
        private string connectionString;
        public string ConnectionString => connectionString;

        public Database(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
