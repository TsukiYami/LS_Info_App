using Entities.DTOs.GET;
using Entities.DTOs.PUT;
using Entities.Entities;

namespace BackendInfoApp.Mapper {
    public class WeatherDataMapper {

        /// <summary>
        /// Ändert die Daten von einem Entity in ein GetDTO um.
        /// </summary>
        /// <param name="oEntity"></param>
        /// <returns></returns>
        public GetWeatherDataDTO EntityToGetDTO(WeatherDataEntity oEntity) {
            return new GetWeatherDataDTO(
                oEntity.nId,
                oEntity.sCity,
                oEntity.sCountry,
                oEntity.dTempC,
                oEntity.sConditionText,
                oEntity.dWindKph,
                oEntity.sWindDir,
                oEntity.dFeelsLikeC
            );
        }

        /// <summary>
        /// Ändert die Daten von einem Entity in ein GetDTO um.
        /// </summary>
        /// <param name="oForecastEntity"></param>
        /// <returns></returns>
        public GetWeatherForecastDataDTO ForecastEntityToGetDTO(WeatherForecastDataEntity oForecastEntity) {
            return new GetWeatherForecastDataDTO (
                oForecastEntity.nId,
                oForecastEntity.sCity,
                oForecastEntity.sCountry,
                oForecastEntity.sConditionText,
                oForecastEntity.dMaxTempC,
                oForecastEntity.dMinTempC,
                oForecastEntity.dAvgTempC,
                oForecastEntity.oForDate
            );
        }

        public WeatherDataEntity PutDTOToEntity(PutWeatherDataDTO oDTO) {
            return new WeatherDataEntity(
                oDTO.nId,
                oDTO.sCity,
                oDTO.sCountry,
                oDTO.dTempC,
                oDTO.sConditionText,
                oDTO.dWindKph,
                oDTO.sWindDir,
                oDTO.dFeelsLikeC
            );
        }

        public WeatherForecastDataEntity PutForecastDTOToEntity(PutWeatherForecastDataDTO oDTO) {
            return new WeatherForecastDataEntity(
                oDTO.nId,
                oDTO.sCity,
                oDTO.sCountry,
                oDTO.sConditionText,
                oDTO.dMaxTempC,
                oDTO.dMinTempC,
                oDTO.dAvgTempC,
                oDTO.oForDate
            );
        }
    }
}