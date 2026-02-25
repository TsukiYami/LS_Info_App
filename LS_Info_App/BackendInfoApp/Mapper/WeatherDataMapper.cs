using Entities.DTOs.GET;
using Entities.DTOs.PUT;
using Entities.Entities;

namespace BackendInfoApp.Mapper {
    public class WeatherDataMapper {
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

        public GetWeatherForecastDataDTO ForecastEntityToGetDTO(WeatherForecastDataEntity oEntity) {
            return new GetWeatherForecastDataDTO (
                oEntity.nId,
                oEntity.sCity,
                oEntity.sCountry,
                oEntity.dTempC,
                oEntity.sConditionText,
                oEntity.dWindKph,
                oEntity.sWindDir,
                oEntity.dFeelsLikeC,
                oEntity.dMaxTempC,
                oEntity.dMinTempC,
                oEntity.dAvgTempC,
                oEntity.nForeignKey
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

        public static WeatherForecastDataEntity PutForecastDTOToEntity(PutWeatherForecastDataDTO oDTO) {
            return new WeatherForecastDataEntity(
                oDTO.sCity,
                oDTO.sCountry,
                oDTO.dTempC,
                oDTO.sConditionText,
                oDTO.dWindKph,
                oDTO.sWindDir,
                oDTO.dFeelsLikeC,
                oDTO.dMaxTempC,
                oDTO.dMinTempC,
                oDTO.dAvgTempC
            );
        }
    }
}