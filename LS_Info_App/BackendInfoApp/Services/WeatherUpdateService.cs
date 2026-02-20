using BackendInfoApp.DB;
using Entities.Entities;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace BackendInfoApp.Services {
    public class WeatherUpdateService : BackgroundService {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<WeatherUpdateService> _logger;

        public WeatherUpdateService(IServiceProvider serviceProvider, ILogger<WeatherUpdateService> logger) {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            _logger.LogInformation("WeatherUpdateService gestartet");
            await UpdateWeatherData(stoppingToken);

            using PeriodicTimer oTimer = new PeriodicTimer(TimeSpan.FromMinutes(5));
            
            try {
                while (await oTimer.WaitForNextTickAsync(stoppingToken)) {
                    await UpdateWeatherData(stoppingToken);
                }
            } catch (OperationCanceledException) {
                _logger.LogInformation("WeatherUpdateService wurde beendet");
            }
        }

        private async Task UpdateWeatherData(CancellationToken stoppingToken) {
            try {
                using var oScope = _serviceProvider.CreateScope();
                var oDbContext = oScope.ServiceProvider.GetRequiredService<InfoAppDbContext>();

                var oResult = GetWeatherFromApi();
                if (oResult != null) {
                    _logger.LogInformation($"Wetterdaten aktualisiert: {oResult.sCity}");
                } else {
                    _logger.LogWarning("Wetterdaten konnten nicht aktualisiert werden");
                }
            } catch (Exception ex) {
                _logger.LogError(ex, "Fehler beim Aktualisieren der Wetterdaten");
            }

            await Task.CompletedTask;
        }

        public WeatherDataEntity? GetWeatherFromApi() {
            string sJSON = File.ReadAllText("config.json");
            JObject oConfig = JObject.Parse(sJSON);
            string sCity = oConfig["WeatherApi"]["City"].ToString();
            string sApiKey = oConfig["WeatherApi"]["ApiKey"].ToString();

            if (string.IsNullOrWhiteSpace(sCity)) {
                throw new ArgumentException("sCity must be provided", nameof(sCity));
            }

            if (string.IsNullOrWhiteSpace(sApiKey)) {
                throw new ArgumentException("sApiKey must be provided", nameof(sApiKey));
            }

            string sUrl = $"{oConfig["WeatherApi"]["BaseUrl"]}{Uri.EscapeDataString(sApiKey)}&q={Uri.EscapeDataString(sCity)}&aqi=no";

            try {
                string sJson;

                using (HttpClient oClient = new HttpClient { Timeout = TimeSpan.FromSeconds(10) }) {
                    var oResponse = oClient.GetAsync(sUrl).GetAwaiter().GetResult();

                    if (!oResponse.IsSuccessStatusCode) {
                        return null;
                    }
                    sJson = oResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }
                using var oDoc = JsonDocument.Parse(sJson);
                var oRoot = oDoc.RootElement;

                if (!oRoot.TryGetProperty("location", out var oLocation) || !oRoot.TryGetProperty("current", out var oCurrent)) {
                    return null;
                }

                string sResolvedCity = oLocation.GetProperty("name").GetString() ?? sCity;
                string sCountry = oLocation.GetProperty("country").GetString() ?? string.Empty;
                double dTempC = oCurrent.GetProperty("temp_c").GetDouble();
                string sCondition = oCurrent.GetProperty("condition").GetProperty("text").GetString() ?? string.Empty;
                double dWindKph = oCurrent.GetProperty("wind_kph").GetDouble();
                string sWindDir = oCurrent.GetProperty("wind_dir").GetString() ?? string.Empty;
                double dFeelsLikeC = oCurrent.GetProperty("feelslike_c").GetDouble();

                WeatherDataEntity oEntity = new WeatherDataEntity(0, sResolvedCity, sCountry, dTempC, sCondition, dWindKph, sWindDir, dFeelsLikeC);

                return oEntity;
            } catch (Newtonsoft.Json.JsonException) {
                return null;
            } catch (HttpRequestException) {
                return null;
            } catch (Exception) {
                return null;
            }
        }
    }
}