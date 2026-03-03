using BackendInfoApp.DB;
using BackendInfoApp.Services;
using Entities.DTOs.GET;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackendInfoApp.Controllers {
    [ApiController]
    [Route("api")]
    public class WeatherDataController : ControllerBase {
        private WeatherDataService oService;

        public WeatherDataController(InfoAppDbContext oContext) {
            oService = new WeatherDataService(oContext);
        }

        /// <summary>
        /// API Endpunkt um die aktuellsten Wetterdaten abzurufen.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRecentWeatherData")]
        public IActionResult GetRecentWeatherData() {
            GetWeatherDataDTO oWeatherData = oService.GetLatest();
            string sJsonReturn = JsonConvert.SerializeObject(oWeatherData);
            return Ok(sJsonReturn);
        }

        /// <summary>
        /// API Endpunkt um die Wettervorhersagedaten der nächsten Tage abzurufen.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetWeatherForecastData")]
        public IActionResult GetWeatherForecastData() {
            IEnumerable<GetWeatherForecastDataDTO> oWeatherForecastData = oService.GetAllForecasts();
            string sJsonReturn = JsonConvert.SerializeObject(oWeatherForecastData);
            return Ok(sJsonReturn);
        }
    }
}
