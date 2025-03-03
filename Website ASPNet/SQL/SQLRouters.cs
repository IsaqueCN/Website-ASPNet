using Website_ASPNet.SQL.Routers;

namespace Website_ASPNet.SQL
{
    public class SQLRouters
    {
        // Class that groups together all the SQL Routers allowing for them to be easily used
        UserRouter userRouter;

        public SQLRouters(SQLTables sqlFunctions)
        {
            userRouter = new UserRouter(sqlFunctions);
        }

        public void UseRouters(WebApplication app)
        {
            userRouter.UseRouter(app);
        }
    }
}
