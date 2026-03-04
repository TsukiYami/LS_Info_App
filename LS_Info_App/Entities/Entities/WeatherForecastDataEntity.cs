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

        public WeatherForecastDataEntity(string sCondition, float dMaxtemp_c, float dMintemp_c, float dAvgtemp_c, DateTime oForDate, string sCity = "Bremen", string sCountry = "Germany") {
            this.sCity = sCity;
            this.sCountry = sCountry;
            this.sConditionText = sCondition;
            this.dMaxTempC = dMaxtemp_c;
            this.dMinTempC = dMintemp_c;
            this.dAvgTempC = dAvgtemp_c;
            this.oForDate = oForDate;
        }
        public int nId { get; set; }
        public string sCity { get; set; }
        public string sCountry { get; set; }
        public string sConditionText { get; set; }
        public DateTime zRecordedAt { get; set; } = DateTime.UtcNow;
        public DateTime oForDate { get; set; }
        public float dMaxTempC { get; set; }
        public float dMinTempC { get; set; }
        public float dAvgTempC { get; set; }
    }
}