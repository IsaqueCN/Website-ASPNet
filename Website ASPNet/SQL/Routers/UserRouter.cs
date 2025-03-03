using Org.BouncyCastle.Asn1;
using System.Security.Policy;
using System.Text.Json;
using Website_ASPNet.SQL.Tables;

namespace Website_ASPNet.SQL.Routers
{
    public class UserRouter
    {
        // Class that provides a router for the user table

        string url = APIRouter.URL + "/users";
        SQLTables sqlFunctions;

        public string URL => url;

        public UserRouter(SQLTables SQLFunctions)
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
                    List<User> result = sqlFunctions.User.Get();

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
                    User? result = sqlFunctions.User.GetById(id);

                    if (result != null && !string.IsNullOrEmpty(result.Name))
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
                    User? result = await context.Request.ReadFromJsonAsync<User>();

                    if (result == null || string.IsNullOrEmpty(result.Name) || string.IsNullOrEmpty(result.Password))
                        return Results.Json(new { success = false, message = "Invalid Request. Expected string 'name' and string 'passsword' on request body" }, statusCode: 400);

                    int rowsAffected = sqlFunctions.User.Post(result.Name, result.Password, result.Email);
                    return Results.Json(new { sucess = true, data = $"{rowsAffected} Rows affected." });
                }
                catch (JsonException err)
                {
                    return Results.Json(new { success = false, message = "Invalid Request. Expected string 'name' and string 'passsword' on request body" }, statusCode: 400);
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
                    User? result = await context.Request.ReadFromJsonAsync<User>();

                    if (result == null || result.ID == 0)
                        return Results.Json(new { success = false, message = "Invalid Request. Expected numeric 'id' on request body" }, statusCode: 400);

                    int rowsAffected = sqlFunctions.User.Delete(result.ID);
                    return Results.Json(new { sucess = true, data = $"{rowsAffected} Rows affected." });
                }
                catch (JsonException err)
                {
                    return Results.Json(new { success = false, message = "Invalid Request. Expected numeric 'id' on request body" }, statusCode: 400);
                }
                catch (Exception err)
                {
                    return ServerErrorHandler(err);
                }
            });
        }
    }
}
