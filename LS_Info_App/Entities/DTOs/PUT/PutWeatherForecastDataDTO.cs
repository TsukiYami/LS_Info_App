namespace Entities.DTOs.PUT {
    public class PutWeatherForecastDataDTO {
        public PutWeatherForecastDataDTO(int nId, string sCity, string sCountry, string sConditionText, float dMaxTempC, float dMinTempC, float dAvgTempC)  {
            this.nId = nId;
            this.sCity = sCity;
            this.sCountry = sCountry;
            this.sConditionText = sConditionText;
            this.dMaxTempC = dMaxTempC;
            this.dMinTempC = dMinTempC;
            this.dAvgTempC = dAvgTempC;
        }
        
        public int nId { get; private set; }
        public string sCity { get; private set; }
        public string sCountry { get; private set; }
        public string sConditionText { get; private set; }
        public float dMaxTempC { get; private set; }
        public float dMinTempC { get; private set; }
        public float dAvgTempC { get; private set; }
        public DateOnly oForDate { get; private set; }
    }
}
