using Entities;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace BackendInfoApp.Services {
    public class UpdateWeatherService {
        private HttpClientHandler oHandler;
        private HttpClient oClient;
        private Guid oSessionToken;

        private const string csAPILink = "https://api.weatherapi.com/v1/forecast.json?key=50e9d5ad77e540c7a9f213153261802&q=Bremen&days=3&aqi=no";

        public async Task UpdateWeatherData() {
            try {
                using (HttpRequestMessage request = await PrepareRequest(csAPILink)) {
                    using (HttpResponseMessage response = await oClient.GetAsync(request.RequestUri)) {
                        if (response.IsSuccessStatusCode) {
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            JObject weatherJson = JObject.Parse(jsonResponse);

                        }
                    }
                }
            } catch (Exception) {
                Debug.Assert(false);
            }
        }

        private async Task<HttpRequestMessage> PrepareRequest(string sURL) {
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