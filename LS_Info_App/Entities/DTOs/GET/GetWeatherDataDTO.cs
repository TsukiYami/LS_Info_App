namespace Entities.DTOs.GET {
public class GetWeatherDataDTO {
        public GetWeatherDataDTO(int nId, string sCity, string sCountry, float dTempC, string sConditionText, float dWindKph, string sWindDir, float dFeelsLikeC) {
            this.nId = nId;
            this.sCity = sCity;
            this.sCountry = sCountry;
            this.dTempC = dTempC;
            this.sConditionText = sConditionText;
            this.dWindKph = dWindKph;
            this.sWindDir = sWindDir;
            this.dFeelsLikeC = dFeelsLikeC;
        }

        public int nId { get; protected set; }
        public string sCity { get; protected set; }
        public string sCountry { get; protected set; }
        public float dTempC { get; protected set; }
        public string sConditionText { get; protected set; }
        public float dWindKph { get; protected set; }
        public string sWindDir { get; protected set; }
        public float dFeelsLikeC { get; protected set; }
        public DateTime oRecordedAt { get; protected set; } = DateTime.UtcNow;
    }
}