using Org.BouncyCastle.Asn1;
using System.Security.Policy;
using System.Text.Json;

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
            // Get
            app.MapGet(url + "/", (HttpContext context) =>
            {
                Dictionary<string, int> cadastroRows = sqlFunctions.Cadastro.Get();

                return Results.Json(new { success = true, data = cadastroRows });
            });

            // GetByID
            app.MapGet(url + "/{id:int}", (HttpContext context, int id) =>
            {
                string nome = sqlFunctions.Cadastro.GetById(id);

                if (!string.IsNullOrEmpty(nome))
                    return Results.Json(new { success = true, data = nome });

                return Results.Json(new { success = false, message = "User not found" }, statusCode: 404);
            });

            // Post
            app.MapPost(url + "/", async (HttpContext context) =>
            {
                try
                {
                    JsonDocument document = await JsonDocument.ParseAsync(context.Request.Body);
                    JsonElement nome;

                    // if ID is not in body | if it is null | if it is an empty string
                    if (!document.RootElement.TryGetProperty("nome", out nome) || nome.ValueKind == JsonValueKind.Null || string.IsNullOrWhiteSpace(nome.GetString()))
                    {
                        return Results.Json(new { success = false, message = "Invalid Request. Expected 'nome' on request body" }, statusCode: 400);
                    }

                    int rowsAffected = sqlFunctions.Cadastro.Post(nome.GetString());
                    return Results.Json(new { sucess = true, data = $"{rowsAffected} Rows affected." });

                } catch (Exception err)
                {
                    return Results.Json(new { success = false, message = $"{err.Message}" }, statusCode: 500);
                }
            });

            // Delete
            app.MapDelete(url + "/", async (HttpContext context) =>
            {
                try
                {
                    JsonDocument document = await JsonDocument.ParseAsync(context.Request.Body);
                    JsonElement idElement;
                    int id;

                    // if ID is not in body | if it is null | if it is a string but empty
                    if (!document.RootElement.TryGetProperty("id", out idElement)
                    || idElement.ValueKind == JsonValueKind.Null 
                    || (idElement.ValueKind == JsonValueKind.String && string.IsNullOrWhiteSpace(idElement.GetString())))
                    {
                        return Results.Json(new { success = false, message = "Invalid Request. Expected 'id' on request body" }, statusCode: 400);
                    }

                    if (idElement.ValueKind == JsonValueKind.Number)
                        id = idElement.GetInt32();
                    else
                        id = int.Parse(idElement.GetString());

                    int rowsAffected = sqlFunctions.Cadastro.Delete(id);
                    return Results.Json(new { sucess = true, data = $"{rowsAffected} Rows affected." });

                } catch (Exception err)
                {
                    return Results.Json(new { success = false, message = $"{err.Message}" }, statusCode: 500);
                }
            });
        }
    }
}
