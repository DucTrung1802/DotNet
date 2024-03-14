namespace RoutingInASPNETCoreWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}