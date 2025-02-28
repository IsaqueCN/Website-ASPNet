using Org.BouncyCastle.Asn1;
using System.Security.Policy;

namespace Website_ASPNet.SQL.Routers
{
    public class CadastroRouter
    {
        // Class that provides a router for the cadastro table

        string url = APIRouter.URL + "/cadastro";
        SQLTables sqlFunctions;

        public string URL => url;

        public CadastroRouter(SQLTables SQLFunctions)
        {
            sqlFunctions = SQLFunctions;
        }

        public void UseRouter(WebApplication app)
        {
            app.MapGet(url + "/", (HttpContext context) =>
            {
                Dictionary<string, int> cadastroRows = sqlFunctions.Cadastro.Get();

                return Results.Json(new { success = true, data = cadastroRows });
            });

            app.MapGet(url + "/{id:int}", (HttpContext context, int id) =>
            {
                string nome = sqlFunctions.Cadastro.GetById(id);

                if (!string.IsNullOrEmpty(nome))
                    return Results.Json(new { success = true, data = nome });

                return Results.Json(new { success = false, message = "User not found" }, statusCode: 404);
            });
        }
    }
}
