using Org.BouncyCastle.Asn1;
using System.Security.Policy;
using System.Text.Json;
using Website_ASPNet.SQL.Tables;

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

        private IResult ServerErrorHandler(Exception err)
        {
            Console.WriteLine(err);
            return Results.Json(new { success = false, message = $"{err.Message}" }, statusCode: 500);
        }
        public void UseRouter(WebApplication app)
        {
            // Get
            app.MapGet(url + "/", (HttpContext context) =>
            {
                try
                {
                    List<Cadastro> result = sqlFunctions.Cadastro.Get();

                    return Results.Json(new { success = true, data = result });
                }
                catch (Exception err)
                {
                    return ServerErrorHandler(err);
                }
            });

            // GetByID
            app.MapGet(url + "/{id:int}", (HttpContext context, int id) =>
            {
                try
                {
                    Cadastro result = sqlFunctions.Cadastro.GetById(id);

                    if (result != null && !string.IsNullOrEmpty(result.Nome))
                        return Results.Json(new { success = true, data = result });

                    return Results.Json(new { success = false, message = "User not found" }, statusCode: 404);
                }
                catch (Exception err)
                {
                    return ServerErrorHandler(err);
                }
            });

            // Post
            app.MapPost(url + "/", async (HttpContext context) =>
            {
                try
                {
                    Cadastro? result = await context.Request.ReadFromJsonAsync<Cadastro>();

                    if (result == null || string.IsNullOrEmpty(result.Nome))
                        return Results.Json(new { success = false, message = "Invalid Request. Expected 'nome' on request body" }, statusCode: 400);


                    int rowsAffected = sqlFunctions.Cadastro.Post(result.Nome);
                    return Results.Json(new { sucess = true, data = $"{rowsAffected} Rows affected." });
                }
                catch (Exception err)
                {
                    return ServerErrorHandler(err);
                }
            });

            // Delete
            app.MapDelete(url + "/", async (HttpContext context) =>
            {
                try
                {
                    Cadastro? result = await context.Request.ReadFromJsonAsync<Cadastro>();

                    if (result == null || result.ID == 0)
                        return Results.Json(new { success = false, message = "Invalid Request. Expected 'id' on request body" }, statusCode: 400);

                    int rowsAffected = sqlFunctions.Cadastro.Delete(result.ID);
                    return Results.Json(new { sucess = true, data = $"{rowsAffected} Rows affected." });
                }
                catch (JsonException err)
                {
                    return Results.Json(new { success = false, message = "Invalid Request. Expected 'id' to be a number" }, statusCode: 400);
                }
                catch (Exception err)
                {
                    return ServerErrorHandler(err);
                }
            });
        }
    }
}
