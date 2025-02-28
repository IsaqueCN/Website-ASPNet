using MySql.Data.MySqlClient;
using Website_ASPNet.SQL;

namespace Website_ASPNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            string connectionString = app.Configuration.GetConnectionString("DefaultConnection");
            Database database = new Database(connectionString);

            APIRouter apiRouter = new APIRouter();
            apiRouter.UseRouter(app);

            app.Use(async(context, next) =>
            {
                context.Response.OnStarting(() =>
                {
                    if (context.Response.ContentType != null && !context.Response.ContentType.Contains("charset"))
                    {
                        context.Response.ContentType += "; charset=utf-8";
                    }
                    return Task.CompletedTask;
                });

                await next();
            });

            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404)
                {
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync("<h1>Page not found!</h1>");
                }
            });

            app.MapGet("/", (HttpContext context) => {
                return Results.Content("<h1>You are in the main page!", "text/html");
            });

            app.Run();
        }
    }
}
