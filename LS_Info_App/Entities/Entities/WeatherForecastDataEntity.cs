namespace Entities.Entities {
    public class WeatherForecastDataEntity {
        public WeatherForecastDataEntity(int nId, string sCity, string sCountry, string sConditionText, float dMaxTempC, float dMinTempC, float dAvgTempC, DateTime oForDate) {
            this.nId = nId;
            this.sCity = sCity;
            this.sCountry = sCountry;
            this.sConditionText = sConditionText;
            this.dMaxTempC = dMaxTempC;
            this.dMinTempC = dMinTempC;
            this.dAvgTempC = dAvgTempC;
            this.oForDate = oForDate;
        }

        public WeatherForecastDataEntity(string condition, float maxtemp_c, float mintemp_c, float avgtemp_c, DateTime oForDate, string city = "Bremen", string country = "Germany") {
            this.sCity = city;
            this.sCountry = country;
            this.sConditionText = condition;
            this.dMaxTempC = maxtemp_c;
            this.dMinTempC = mintemp_c;
            this.dAvgTempC = avgtemp_c;
            this.oForDate = oForDate;
        }
        public int nId { get; set; }
        public string sCity { get; set; }
        public string sCountry { get; set; }
        public string sConditionText { get; set; }
        public DateTime zRecordedAt { get; private set; } = DateTime.UtcNow;
        public DateTime oForDate { get; private set; }
        public float dMaxTempC { get; set; }
        public float dMinTempC { get; set; }
        public float dAvgTempC { get; set; }
    }
}