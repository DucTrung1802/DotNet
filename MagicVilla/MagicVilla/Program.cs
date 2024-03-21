
namespace MagicVilla
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Error 406
            //builder.Services.AddControllers(option => { option.ReturnHttpNotAcceptable = true; }).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

            builder.Services.AddControllers().AddNewtonsoftJson().AddXmlDataContractSerializerFormatters(); ;
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
