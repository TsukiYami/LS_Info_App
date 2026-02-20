using BackendInfoApp.Middleware;
using BackendInfoApp.DB;
using Microsoft.EntityFrameworkCore;
using BackendInfoApp.Services;

namespace BackendInfoApp {
    public class Startup {
        public IConfiguration oConfiguration { get; private set; }

        public Startup(IConfiguration oConfig) {
            oConfiguration = oConfig;
        }

        public void ConfigureServices(IServiceCollection oServices) {
            oServices.AddDbContext<InfoAppDbContext>(options => options.UseNpgsql("Host=127.0.0.1;Port=5432;Database=postgres;Username=ADMIN;Password=ADMIN;SSL Mode=disable;Trust Server Certificate=true;Maximum Pool Size=20;Minimum Pool Size=5;Connection Idle Lifetime=300;Connection Lifetime=600"));

            oServices.AddHostedService<WeatherUpdateService>();
            oServices.AddControllers();
            oServices.AddEndpointsApiExplorer();
        }

        public void Configure(IApplicationBuilder oApp, IWebHostEnvironment oEnv) {
            if (oEnv.IsDevelopment()) {
                oApp.UseDeveloperExceptionPage();

            }

            oApp.UseMiddleware<GlobalExceptionMiddleware>();
            oApp.UseRouting();
            oApp.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}