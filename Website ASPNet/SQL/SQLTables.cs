using Org.BouncyCastle.Asn1.Mozilla;
using Website_ASPNet.SQL.Tables;

namespace Website_ASPNet.SQL
{
    public class SQLTables
    {
        // Class that groups together all the SQL tables making it easier to access their functions

        Cadastro cadastro;
        public Cadastro Cadastro => cadastro;

        public SQLTables(Database db)
        {
            cadastro = new Cadastro(db);
        }
    }
}
