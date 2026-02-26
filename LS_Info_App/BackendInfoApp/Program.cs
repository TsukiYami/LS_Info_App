using BackendInfoApp.DB;
using BackendInfoApp.Mapper;
using BackendInfoApp.Middleware;
using BackendInfoApp.Repositories;
using BackendInfoApp.Services;
using Microsoft.EntityFrameworkCore;

namespace BackendInfoApp {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<InfoAppDbContext>(options => 
                options.UseNpgsql(connectionString: builder.Configuration.GetConnectionString("InfoAppDB")));

            builder.Services.AddHostedService<UpdateWeatherService>();
            builder.Services.AddScoped<WeatherDataRepository>();
            builder.Services.AddScoped<WeatherDataService>();
            builder.Services.AddScoped<WeatherDataMapper>();
            
            builder.Services.AddEndpointsApiExplorer();
            
            var app = builder.Build();

            if (app.Environment.IsDevelopment()) {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.Urls.Add("http://0.0.0.0:8080");

            app.UseMiddleware<GlobalExceptionMiddleware>();
            
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}