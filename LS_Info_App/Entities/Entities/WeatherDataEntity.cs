using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    [Table("WeatherData", Schema = "WeatherData")]
    public class WeatherDataEntity
    {
        public WeatherDataEntity() { }

        public WeatherDataEntity(int nId, string sCity, string sCountry, float dTempC, string sConditionText, float dWindKph, string sWindDir, float dFeelsLikeC) {
            this.nId = nId;
            this.sCity = sCity;
            this.sCountry = sCountry;
            this.dTempC = dTempC;
            this.sConditionText = sConditionText;
            this.dWindKph = dWindKph;
            this.sWindDir = sWindDir;
            this.dFeelsLikeC = dFeelsLikeC;
        } 

        public WeatherDataEntity(float temp_c, string condition, float wind_kph, string wind_dir, float feelslike_c, string sCity = "Bremen", string sCountry = "Germany" ) {
            this.sCity = sCity;
            this.sCountry = sCountry;
            dTempC = temp_c;
            sConditionText = condition;
            dWindKph = wind_kph;
            sWindDir = wind_dir;
            dFeelsLikeC = feelslike_c;
        }

        public int nId { get; set; }
        public string sCity { get; set; }
        public string sCountry { get; set; }
        public float dTempC { get; set; }
        public string sConditionText { get; set; }
        public float dWindKph { get; set; }
        public string sWindDir { get; set; }
        public float dFeelsLikeC { get; set; }
        public DateTime zRecordedAt { get; private set; } = DateTime.UtcNow;
    }
}