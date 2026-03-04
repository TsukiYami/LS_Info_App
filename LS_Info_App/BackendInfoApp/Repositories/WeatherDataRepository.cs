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
            EntityEntry<WeatherDataEntity> oEntry = oContext.WeatherData.Update(oEntity);
            oContext.SaveChanges();
            
            return oEntry.Entity;
        }

        /// <summary>
        /// Updated das übergebene WeatherForecastDataEntity in der Datenbank
        /// </summary>
        /// <param name="oForecastEntity"></param>
        /// <returns></returns>
        /*public WeatherForecastDataEntity UpdateWeatherForecast(WeatherForecastDataEntity oForecastEntity) {
            int nRowsChanged = oContext.WeatherForecasts.Where(e => e.zRecordedAt < DateTime.UtcNow).ExecuteUpdate;
            //EntityEntry<WeatherForecastDataEntity> oEntry = oContext.WeatherForecasts.Update(oForecastEntity);
            oContext.SaveChanges();
            
            return  oEntry.Entity;
        }*/

        /// <summary>
        /// Updated das übergebene WeatherForecastDataEntity in der Datenbank
        /// </summary>
        /// <param name="oForecastEntity"></param>
        /// <returns></returns>
        public int UpdateWeatherForecast(WeatherForecastDataEntity oForecastEntity) {
            int nRowsChanged = oContext.WeatherForecasts.Where(e => e.nId == oForecastEntity.nId).ExecuteUpdate(s => s.SetProperty(e => e.zRecordedAt = oForecastEntity.zRecordedAt, e => e.dMinTempC = oForecastEntity.dMinTempC, e => e.));
            //EntityEntry<WeatherForecastDataEntity> oEntry = oContext.WeatherForecasts.Update(oForecastEntity);
            oContext.SaveChanges();

            return nRowsChanged;
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