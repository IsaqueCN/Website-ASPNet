using Website_ASPNet.SQL.Routers;

namespace Website_ASPNet.SQL
{
    public class SQLRouters
    {
        // Class that groups together all the SQL Routers allowing for them to be easily used
        CadastroRouter cadastroRouter;

        public SQLRouters(SQLTables sqlFunctions)
        {
            cadastroRouter = new CadastroRouter(sqlFunctions);
        }

        public void UseRouters(WebApplication app)
        {
            cadastroRouter.UseRouter(app);
        }
    }
}
