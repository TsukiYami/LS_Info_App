using BackendInfoApp.DB;
using Entities.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BackendInfoApp.Repositories {
    public class WeatherDataRepository {
        private InfoAppDbContext oContext;
        private bool bDisposedValue;

        public WeatherDataRepository(InfoAppDbContext oContext) {
            this.oContext = oContext;
        }

        ~WeatherDataRepository() {
            Dispose();
        }

        public WeatherDataEntity GetByID(int nID) {
            return oContext.WeatherData.Find(nID);
        }

        public IEnumerable<WeatherForecastDataEntity> GetWeatherForecastDataService() {
            return oContext.WeatherForecasts;
        }

        public WeatherDataEntity GetWeatherDataService() {
            return oContext.WeatherData.OrderByDescending(w => w.nId).FirstOrDefault();
        }

        /*public async Task PostWeatherDataService(WeatherDataEntity oEntity) {
            EntityEntry<WeatherDataEntity> oEntry = oContext.WeatherData.Add(oEntity);
            await oContext.SaveChangesAsync();
        }

        public async Task PostWeatherForecastDataService(WeatherForecastDataEntity oEntity) {
            EntityEntry<WeatherForecastDataEntity> oEntry = oContext.WeatherForecasts.Add(oEntity);
            await oContext.SaveChangesAsync();
        }*/

        public async Task PutWeatherDataService(WeatherDataEntity oEntity) {
            oContext.WeatherData.Update(oEntity);
            await oContext.SaveChangesAsync();
        }

        public async Task PutWeatherForecastDataService(WeatherForecastDataEntity oEntity) {
            oContext.WeatherForecasts.Update(oEntity);
            await oContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool bDisposing) {
            if (!bDisposedValue) {
                if (bDisposing) {
                    oContext.Dispose();
                }
                bDisposedValue = true;
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}