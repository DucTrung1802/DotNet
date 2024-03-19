namespace AutomapperDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.MapControllers();

            app.Run();
        }
    }
}