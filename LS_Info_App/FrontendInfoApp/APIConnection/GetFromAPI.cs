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
    public class GetFromAPI : IGet {
        private HttpClientHandler oHandler;
        private HttpClient oClient;
        private Guid oSessionToken;
        private GetWeatherDataDTO oWeatherData;

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
            oHandler.Dispose();
        }

        public GetWeatherDataDTO WeatherData() {
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
                                    GetWeatherDataDTO oWeatherDeserializedData = JsonConvert.DeserializeObject<GetWeatherDataDTO>(MainJson);
                                    oWeatherData = oWeatherDeserializedData;
                                }
                            }
                        }
                    }
                }
            } catch (Exception) {
                Debug.Assert(false);
                return null;
            }
            return oWeatherData;
        }

        private HttpRequestMessage PrepareRequest(string sURL) {
            try {
                oClient = new HttpClient(oHandler);

                using (HttpRequestMessage oRequest = new HttpRequestMessage(HttpMethod.Get, new Uri(sURL))) {
                    oClient.DefaultRequestHeaders.Add(RequestValues.HEADER_GUID, oSessionToken.ToString());

                    return oRequest;
                }
            } catch (HttpRequestException) {
                Debug.Assert(false);
                return null;
            }
        }
    }
}
