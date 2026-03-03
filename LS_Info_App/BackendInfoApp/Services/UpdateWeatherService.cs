using Entities.Entities;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text.Json;
using BackendInfoApp.DB;
using BackendInfoApp.Repositories;

namespace BackendInfoApp.Services {
    public class UpdateWeatherService : BackgroundService {
        private const string csAPILink = "https://api.weatherapi.com/v1/forecast.json?key=50e9d5ad77e540c7a9f213153261802&q=Bremen&days=3&aqi=no";
        
        private readonly IServiceProvider m_oServiceProvider;
        private readonly ILogger<UpdateWeatherService> m_oLogger;
        
        private HttpClientHandler m_oHandler;
        private HttpClient m_oClient;
        
        public UpdateWeatherService(IServiceProvider serviceProvider, ILogger<UpdateWeatherService> logger) {
            m_oServiceProvider = serviceProvider;
            m_oLogger = logger;
            
            m_oHandler = new HttpClientHandler();
            m_oHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
            m_oHandler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            m_oClient = new HttpClient(m_oHandler);
        }

        ~UpdateWeatherService()
        {
            m_oClient.Dispose();    
            m_oClient.Dispose();
        }

        /// <summary>
        /// Mini Dienst, damit die Wetterdaten regelmäßig aktualisiert werden.
        /// Er startet direkt mit dem Abrufen der Daten und aktualisiert diese dann stündlich.
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            m_oLogger.LogInformation("WeatherUpdateService gestartet");
            UpdateWeatherData(stoppingToken);

            using PeriodicTimer oTimer = new PeriodicTimer(TimeSpan.FromHours(1));
            
            try {
                while (await oTimer.WaitForNextTickAsync(stoppingToken)) {
                    await UpdateWeatherData(stoppingToken);
                }
            } catch (OperationCanceledException) {
                m_oLogger.LogInformation("WeatherUpdateService wurde beendet");
            }
        }

        /// <summary>
        /// Wetterdaten werden von der API abgerufen und in die Datenbank eingetragen oder aktualisiert, je nachdem ob bereits Daten vorhanden sind oder nicht.
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        private async Task UpdateWeatherData(CancellationToken stoppingToken) {
            try {
                using IServiceScope oScope = m_oServiceProvider.CreateScope();
                InfoAppDbContext oDbContext = oScope.ServiceProvider.GetRequiredService<InfoAppDbContext>();
                WeatherDataRepository oRepository = new WeatherDataRepository(oDbContext);

                Tuple<WeatherDataEntity, List<WeatherForecastDataEntity>> oListOfDataEntities = RequestAPI();
                if (oListOfDataEntities.Item1 != null || oListOfDataEntities.Item2 != null)  {
                    if (oRepository.GetNewestWeatherDataBool()) {
                        oRepository.UpdateWeatherData(oListOfDataEntities.Item1);
                        foreach (WeatherForecastDataEntity oEntity in oListOfDataEntities.Item2) {
                            oRepository.UpdateWeatherForcast(oEntity);
                        }
                    } else {
                        oRepository.CreateWeatherDataEntry(oListOfDataEntities.Item1);
                        oRepository.CreateWeatherForecast(oListOfDataEntities.Item2);
                    }
                    m_oLogger.LogInformation("Wetterdaten wurden aktualisiert");
                } else {
                    m_oLogger.LogWarning("Wetterdaten konnten nicht aktualisiert werden");
                }
            } catch (Exception ex) {
                m_oLogger.LogError(ex, "Fehler beim Aktualisieren der Wetterdaten");
            }
            
            await Task.CompletedTask;
        }

        /// <summary>
        /// HttpRequest wird an die API gesendet, um die aktuellen Wetterdaten und die Wettervorhersage zu erhalten.
        /// Diese werden dann in WeatherDataEntity und List<WeatherForecastDataEntity> umgewandelt und zurückgegeben.
        /// </summary>
        /// <returns></returns>
        public Tuple<WeatherDataEntity, List<WeatherForecastDataEntity>> RequestAPI()
        {
            WeatherDataEntity oWeatherData = null;
            List<WeatherForecastDataEntity> oForecastData = new List<WeatherForecastDataEntity>();
            try {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri(csAPILink))) {
                    using (HttpResponseMessage response = m_oClient.GetAsync(request.RequestUri).Result) {
                        if (response.IsSuccessStatusCode) {
                            string sJsonResponse = response.Content.ReadAsStringAsync().Result;

                            using JsonDocument oDoc = JsonDocument.Parse(sJsonResponse);
                            var oRoot = oDoc.RootElement;

                            if (!oRoot.TryGetProperty("location", out var oLocation) || !oRoot.TryGetProperty("current", out var oCurrent) || !oRoot.TryGetProperty("forecast", out var oForecastRoot))
                            {
                                return null;
                            }

                            string sCity = oLocation.GetProperty("name").GetString();
                            string sCountry = oLocation.GetProperty("country").GetString();
                            float dTempC = (float)oCurrent.GetProperty("temp_c").GetDouble();
                            string sCondition = oCurrent.GetProperty("condition").GetProperty("text").GetString();
                            float dWindKph = (float)oCurrent.GetProperty("wind_kph").GetDouble();
                            string sWindDir = oCurrent.GetProperty("wind_dir").GetString();
                            float dFeelsLikeC = (float)oCurrent.GetProperty("feelslike_c").GetDouble();
                            
                            oWeatherData = new WeatherDataEntity(dTempC, sCondition, dWindKph, sWindDir, dFeelsLikeC, sCity, sCountry);

                            JArray oForecastDays = JArray.Parse(oForecastRoot.GetProperty("forecastday").GetRawText());
                            
                            for (int i = 0; i < oForecastDays.Count; i++)
                            {
                                DateTime oForDate = (DateTime)oForecastDays[i].SelectToken("date");
                                float dMaxTempC = (float)oForecastDays[i].SelectToken("day").SelectToken("maxtemp_c");
                                float dMinTempC = (float)oForecastDays[i].SelectToken("day").SelectToken("mintemp_c");
                                float dAvgTempC = (float)oForecastDays[i].SelectToken("day").SelectToken("avgtemp_c");
                                string sConditionForecast = (string)oForecastDays[i].SelectToken("day").SelectToken("condition").SelectToken("text");
                                
                                 oForecastData.Add(new WeatherForecastDataEntity(sConditionForecast,  dMaxTempC,
                                     dMinTempC, dAvgTempC, oForDate.ToUniversalTime(), sCity, sCountry));
                            }
                        }
                    }
                }
            } catch (Exception) {
                Debug.Assert(false);
            }
            return new Tuple<WeatherDataEntity, List<WeatherForecastDataEntity>>(oWeatherData, oForecastData);
        }
    }
}