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

        public IEnumerable<WeatherDataEntity> GetAllWeatherDataService() {
            return oContext.WeatherData;
        }

        public WeatherDataEntity GetWeatherDataService() {
            return oContext.WeatherData.OrderByDescending(w => w.nId).FirstOrDefault();
        }

        public int PostWeatherDataService(WeatherDataEntity oEntity) {
            EntityEntry<WeatherDataEntity> oEntry = oContext.WeatherData.Add(oEntity);
            oContext.SaveChanges();
            return oEntry.Entity.nId;
        }

        public void PutWeatherDataService(WeatherDataEntity oEntity) {
            oContext.WeatherData.Update(oEntity);
            oContext.SaveChanges();
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