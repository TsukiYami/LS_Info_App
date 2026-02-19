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

        public List<GetWeatherDataDTO> GetAll() {
            IEnumerable<WeatherDataEntity> voWeatherData = oRepository.GetWeatherDataService();
            List<GetWeatherDataDTO> voDTOs = new List<GetWeatherDataDTO>();
            foreach (WeatherDataEntity oEntity in voWeatherData) {
                voDTOs.Add(oMapper.EntityToGetDTO(oEntity));
            }
            return voDTOs;
        }
    }
}