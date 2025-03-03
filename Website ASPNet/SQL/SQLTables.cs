using Org.BouncyCastle.Asn1.Mozilla;
using Website_ASPNet.SQL.Functions;

namespace Website_ASPNet.SQL
{
    public class SQLTables
    {
        // Class that groups together all the SQL tables making it easier to access their functions

        CadastroFunctions cadastro;
        public CadastroFunctions Cadastro => cadastro;

        public SQLTables(Database db)
        {
            cadastro = new CadastroFunctions(db);
        }
    }
}
