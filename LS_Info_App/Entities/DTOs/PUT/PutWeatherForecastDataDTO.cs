namespace Entities.DTOs.PUT {
    public class PutWeatherForecastDataDTO : PutWeatherDataDTO {
        public PutWeatherForecastDataDTO(int nId, string sCity, string sCountry, float dTempC, string sConditionText, float dWindKph, string sWindDir, float dFeelsLikeC, float dMaxTempC, float dMinTempC, float dAvgTempC, int nForeignKey) : base(nId, sCity, sCountry, dTempC, sConditionText, dWindKph, sWindDir, dFeelsLikeC) {
            this.nId = nId;
            this.sCity = sCity;
            this.sCountry = sCountry;
            this.dTempC = dTempC;
            this.sConditionText = sConditionText;
            this.dWindKph = dWindKph;
            this.sWindDir = sWindDir;
            this.dFeelsLikeC = dFeelsLikeC;
            this.dMaxTempC = dMaxTempC;
            this.dMinTempC = dMinTempC;
            this.dAvgTempC = dAvgTempC;
            this.nForeignKey = nForeignKey;
        }

        public PutWeatherForecastDataDTO(string sCity, string sCountry, float dTempC, string sConditionText, float dWindKph, string sWindDir, float dFeelsLikeC, float dMaxTempC, float dMinTempC, float dAvgTempC) : base(sCity, sCountry, dTempC, sConditionText, dWindKph, sWindDir, dFeelsLikeC) {
            this.sCity = sCity;
            this.sCountry = sCountry;
            this.dTempC = dTempC;
            this.sConditionText = sConditionText;
            this.dWindKph = dWindKph;
            this.sWindDir = sWindDir;
            this.dFeelsLikeC = dFeelsLikeC;
            this.dMaxTempC = dMaxTempC;
            this.dMinTempC = dMinTempC;
            this.dAvgTempC = dAvgTempC;
        }

        public float dMaxTempC { get; private set; }
        public float dMinTempC { get; private set; }
        public float dAvgTempC { get; private set; }
        public int nForeignKey { get; private set; }
    }
}
