using facture_teachers.Models.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace facture_teachers
{
    public class Startup
    {
       
        public static WebApplication InicialiteApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder);
            var app = builder.Build();
            Configure(app); 
            return app;
        }

        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var connectionString = builder.Configuration.GetConnectionString("TeachersConection");
            builder.Services.AddDbContext<facture_profesoresContext>(option => option.UseSqlServer(connectionString));
            builder.Configuration.AddJsonFile("appsettings.json",true, true);

        }
        public static void Configure(WebApplication app)
        {
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
