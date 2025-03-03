using Org.BouncyCastle.Asn1.Mozilla;
using Website_ASPNet.SQL.Functions;

namespace Website_ASPNet.SQL
{
    public class SQLTables
    {
        // Class that groups together all the SQL functions making it easier to access their functions

        UserFunctions user;
        public UserFunctions User => user;

        public SQLTables(Database db)
        {
            user = new UserFunctions(db);
        }
    }
}
