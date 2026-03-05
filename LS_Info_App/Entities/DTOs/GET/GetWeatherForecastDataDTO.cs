namespace Entities.DTOs.GET {
    public class GetWeatherForecastDataDTO {
        public GetWeatherForecastDataDTO(int nId, string sCity, string sCountry, string sConditionText, float dMaxTempC, float dMinTempC, float dAvgTempC, DateOnly oForDate) {
            this.nId = nId;
            this.sCity = sCity;
            this.sCountry = sCountry;
            this.sConditionText = sConditionText;
            this.dMaxTempC = dMaxTempC;
            this.dMinTempC = dMinTempC;
            this.dAvgTempC = dAvgTempC;
            this.oForDate = oForDate;
        }
        
        public int nId { get; protected set; }
        public string sCity { get; protected set; }
        public string sCountry { get; protected set; }
        public string sConditionText { get; protected set; }
        public DateTime oRecordedAt { get; protected set; } = DateTime.UtcNow;
        public DateOnly oForDate { get; private set; } 
        public float dMaxTempC { get; private set; }
        public float dMinTempC { get; private set; }
        public float dAvgTempC { get; private set; }
    }
}