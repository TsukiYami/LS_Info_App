using Entities;
using Entities.DTOs.GET;
using FrontendInfoApp.APIConnection.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace FrontendInfoApp.APIConnection {
    public class GetFromAPI {
        private HttpClientHandler oHandler;
        private HttpClient oClient;

        private const string csAPILink = "http://localhost:8080/api/";

        public GetFromAPI() {
            oHandler = new HttpClientHandler();
            oHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
            oHandler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => {
                return true;
            };

            oClient = new HttpClient();
        }

        ~GetFromAPI() {
            oClient.Dispose();
            oHandler.Dispose();
        }

        public async Task<GetWeatherDataDTO> WeatherData() {
            GetWeatherDataDTO oWeatherDeserializedData = null;

            try {
                using (HttpRequestMessage request = PrepareRequest(csAPILink + "GetRecentWeatherData")) {
                    using (HttpResponseMessage response = oClient.GetAsync(request.RequestUri).Result) {
                        if (response.StatusCode == HttpStatusCode.OK) {
                            using (Stream stream = response.Content.ReadAsStream()) {
                                using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8)) {
                                    string MainJson = streamReader.ReadToEnd();
                                    if (MainJson == null) {
                                        throw new Exception("no Text found");
                                    }
                                    oWeatherDeserializedData = JsonConvert.DeserializeObject<GetWeatherDataDTO>(MainJson);
                                }
                            }
                        }
                    }
                }
            } catch (Exception) {
                Debug.Assert(false);
                return null;
            }
            return oWeatherDeserializedData;
        }

        public async Task<List<GetWeatherForecastDataDTO>> WeatherForecastData() {
            List<GetWeatherForecastDataDTO> voWeatherDeserializedData = new List<GetWeatherForecastDataDTO>();

            try {
                using (HttpRequestMessage oRequest = PrepareRequest(csAPILink + "GetWeatherForecastData")) {
                    using (HttpResponseMessage oResponse = oClient.GetAsync(oRequest.RequestUri).Result) {
                        if (oResponse.StatusCode == HttpStatusCode.OK) {
                            using (Stream oStream = oResponse.Content.ReadAsStream()) {
                                using (StreamReader streamReader = new StreamReader(oStream, Encoding.UTF8)) {
                                    string MainJson = streamReader.ReadToEnd();
                                    if (MainJson == null) {
                                        throw new Exception("no Text found");
                                    }
                                    foreach(GetWeatherForecastDataDTO oDTO in JsonConvert.DeserializeObject<List<GetWeatherForecastDataDTO>>(MainJson)) {
                                        voWeatherDeserializedData.Add(oDTO);
                                    }
                                }
                            }
                        }
                    }
                }
            } catch (Exception) {
                Debug.Assert(false);
                return null;
            }
            return voWeatherDeserializedData;
        }

        private HttpRequestMessage PrepareRequest(string sURL) {
            try {
                return new HttpRequestMessage(HttpMethod.Get, new Uri(sURL));
            } catch (HttpRequestException) {
                Debug.Assert(false);
                return null;
            }
        }
    }
}
