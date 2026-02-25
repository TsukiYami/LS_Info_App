namespace Entities.Entities {
    public class WeatherForecastDataEntity : WeatherDataEntity {
        public WeatherForecastDataEntity(int nId, string sCity, string sCountry, float dTempC, string sConditionText, float dWindKph, string sWindDir, float dFeelsLikeC, float dMaxTempC, float dMinTempC, float dAvgTempC, int nForeignKey) {
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

        public WeatherForecastDataEntity(string sCity, string sCountry, float dTempC, string sConditionText, float dWindKph, string sWindDir, float dFeelsLikeC, float dMaxTempC, float dMinTempC, float dAvgTempC) {
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

        public float dMaxTempC { get; set; }
        public float dMinTempC { get; set; }
        public float dAvgTempC { get; set; }
        public int nForeignKey { get; set; }
    }
}