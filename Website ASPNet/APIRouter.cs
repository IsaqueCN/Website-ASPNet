namespace Website_ASPNet
{
    //Basic router class for "api"
    public class APIRouter
    {
        string url = "/api";
        public string URL => url;

        public void UseRouter(WebApplication app)
        {
            app.MapGet(url + "/", (HttpContext context) =>
            {
                return Results.Content("<h1>API Router is working correctly!</h1>");
            });
        }
    }
}
