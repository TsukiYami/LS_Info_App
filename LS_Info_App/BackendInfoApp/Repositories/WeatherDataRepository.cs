using BackendInfoApp.DB;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging.Abstractions;

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

        /// <summary>
        /// Überprüft, ob ein WeatherDataEntity in der Datenbank vorhanden ist und gibt true zurück, wenn dies der Fall ist, andernfalls false
        /// </summary>
        /// <returns></returns>
        public bool GetNewestWeatherDataBool() {
            if (oContext.WeatherData.OrderByDescending(w => w.nId).FirstOrDefault() != null) { 
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gibt das neuste WeatherDataEntity aus der Datenbank zurück
        /// </summary>
        /// <returns></returns>
        public WeatherDataEntity? GetNewestWeatherData() {
            return oContext.WeatherData.OrderByDescending(w => w.nId).FirstOrDefault();
        }

        /// <summary>
        /// Gibt alle WeatherForecastDataEntities zurück, die in der Datenbank gespeichert sind
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WeatherForecastDataEntity> GetAllWeatherForecasts() {
            return oContext.WeatherForecasts;
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
        /// <param name="voEntitiesToAdd">Die von der Datenbank generierte ID des Entity</param>
        /// <returns></returns>
        public IEnumerable<int> CreateWeatherForecast(List<WeatherForecastDataEntity> voEntitiesToAdd)
        {
            List<int> vnIds = new List<int>();
            foreach (var oEntity in voEntitiesToAdd)
            {
                EntityEntry<WeatherForecastDataEntity> oEntry = oContext.WeatherForecasts.Add(oEntity);
                vnIds.Add(oEntry.Entity.nId);
                oContext.SaveChanges();
            }
            return vnIds;
        }
        
        /// <summary>
        /// Updated das übergebene WeatherDataEntity in der Datenbank
        /// </summary>
        /// <param name="oEntity"></param>
        /// <returns></returns>
        public WeatherDataEntity UpdateWeatherData(WeatherDataEntity oEntity) {
            WeatherDataEntity oEntry = oContext.WeatherData.Where(e => e.nId == oEntity.nId).FirstOrDefault();

            oEntry.sCity = oEntity.sCity;
            oEntry.sCountry = oEntity.sCountry;
            oEntry.sConditionText = oEntity.sConditionText;
            oEntry.dFeelsLikeC = oEntity.dFeelsLikeC;
            oEntry.dTempC = oEntity.dTempC;
            oEntry.dWindKph = oEntity.dWindKph;
            oEntry.zRecordedAt = oEntity.zRecordedAt;

            oContext.SaveChanges();
            
            return oEntry;
        }

        /// <summary>
        /// Updated das übergebene WeatherForecastDataEntity in der Datenbank
        /// </summary>
        /// <param name="oForecastEntity"></param>
        /// <returns></returns>
        public WeatherForecastDataEntity UpdateWeatherForecast(WeatherForecastDataEntity oForecastEntity) {
            WeatherForecastDataEntity oEntry = oContext.WeatherForecasts.Where(e => e.nId == oForecastEntity.nId).FirstOrDefault();

            oEntry.sCity = oForecastEntity.sCity;
            oEntry.sCountry = oForecastEntity.sCountry;
            oEntry.sConditionText = oForecastEntity.sConditionText;
            oEntry.zRecordedAt = oForecastEntity.zRecordedAt;
            oEntry.oForDate = oForecastEntity.oForDate;
            oEntry.dMaxTempC = oForecastEntity.dMaxTempC;
            oEntry.dMinTempC = oForecastEntity.dMinTempC;
            oEntry.dAvgTempC = oForecastEntity.dAvgTempC;

            oContext.SaveChanges();

            return oEntry;
        }

        /// <summary>
        /// Implementierung des Dispose-Patterns, um sicherzustellen, dass der DbContext ordnungsgemäß freigegeben wird
        /// </summary>
        /// <param name="bDisposing"></param>
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