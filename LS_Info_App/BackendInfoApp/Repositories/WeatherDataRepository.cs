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

        public IEnumerable<WeatherForecastDataEntity> GetAllWeatherForcasts() {
            return oContext.WeatherForecasts;
        }

        public WeatherDataEntity? GetNewestWeatherData() {
            return oContext.WeatherData.OrderByDescending(w => w.nId).FirstOrDefault();
        }

        /// <summary>
        /// Fügt das WeatherDataEntity in die Datenbank hinzu
        /// </summary>
        /// <param name="oEntityToAdd">Die von der Datenbank generierte ID des Entity</param>
        /// <returns></returns>
        public int CreateWeatherDataEntry(WeatherDataEntity oEntityToAdd)
        {
            EntityEntry<WeatherDataEntity> oEntry = oContext.WeatherData.Add(oEntityToAdd);
            oContext.SaveChanges();
            return oEntry.Entity.nId;
        }

        /// <summary>
        /// Erstellt ein ForecastEntity in der Datenbank
        /// </summary>
        /// <param name="oEntityToAdd">Die von der Datenbank generierte ID des Entity</param>
        /// <returns></returns>
        public IEnumerable<int> CreateWeatherForecast(List<WeatherForecastDataEntity> voEntityiesToAdd)
        {
            List<int> vnIds = new List<int>();
            foreach (var oEntity in voEntityiesToAdd)
            {
                EntityEntry<WeatherForecastDataEntity> oEntry = oContext.WeatherForecasts.Add(oEntity);
                vnIds.Add(oEntry.Entity.nId);
                oContext.SaveChanges();
            }
            return vnIds;
        }
        
        public WeatherDataEntity  UpdateWeatherData(WeatherDataEntity oEntity) {
            EntityEntry<WeatherDataEntity> oEntry = oContext.WeatherData.Update(oEntity);
            oContext.SaveChangesAsync();
            
            return oEntry.Entity;
        }

        public WeatherForecastDataEntity UpdateWeatherForcast(WeatherForecastDataEntity oEntity) {
            EntityEntry<WeatherForecastDataEntity> oEntry = oContext.WeatherForecasts.Update(oEntity);
            oContext.SaveChangesAsync();
            
            return  oEntry.Entity;
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