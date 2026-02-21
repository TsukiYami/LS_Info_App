using BackendInfoApp.DB;
using BackendInfoApp.Mapper;
using BackendInfoApp.Repositories;
using Entities.DTOs.GET;
using Entities.Entities;

namespace BackendInfoApp.Services {
    public class WeatherDataService {
        private WeatherDataRepository oRepository;
        private WeatherDataMapper oMapper;

        public WeatherDataService(InfoAppDbContext oContext) {
            oRepository = new WeatherDataRepository(oContext);
            oMapper = new WeatherDataMapper();
        }

        public GetWeatherDataDTO GetLatest() {
            WeatherDataEntity oEntity = oRepository.GetWeatherDataService();
            if (oEntity == null) {
                return null;
            }
            return oMapper.EntityToGetDTO(oEntity);
        }
    }
}