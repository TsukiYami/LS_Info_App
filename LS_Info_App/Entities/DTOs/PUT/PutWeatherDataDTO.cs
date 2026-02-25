namespace Entities.DTOs.PUT {
    public class PutWeatherDataDTO {
        public PutWeatherDataDTO(int nId, string sCity, string sCountry, float dTempC, string sConditionText, float dWindKph, string sWindDir, float dFeelsLikeC) {
            this.nId = nId;
            this.sCity = sCity;
            this.sCountry = sCountry;
            this.dTempC = dTempC;
            this.sConditionText = sConditionText;
            this.dWindKph = dWindKph;
            this.sWindDir = sWindDir;
            this.dFeelsLikeC = dFeelsLikeC;
        }

        public PutWeatherDataDTO(string sCity, string sCountry, float dTempC, string sConditionText, float dWindKph, string sWindDir, float dFeelsLikeC) {
            this.sCity = sCity;
            this.sCountry = sCountry;
            this.dTempC = dTempC;
            this.sConditionText = sConditionText;
            this.dWindKph = dWindKph;
            this.sWindDir = sWindDir;
            this.dFeelsLikeC = dFeelsLikeC;
        }

        public int nId { get; set; }
        public string sCity { get; set; }
        public string sCountry { get; set; }
        public float dTempC { get; set; }
        public string sConditionText { get; set; }
        public float dWindKph { get; set; }
        public string sWindDir { get; set; }
        public float dFeelsLikeC { get; set; }
        public DateTime oRecordedAt { get; set; } = DateTime.UtcNow;
    }
}
