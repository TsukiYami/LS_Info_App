using Entities.DTOs.GET;
using FrontendInfoApp.APIConnection;
using System.Windows;
using System.Windows.Controls;

namespace FrontendInfoApp.Pages
{
    /// <summary>
    /// Interaktionslogik für MainPage.xaml
    /// </summary>
    public partial class MainPage : Page {

       
        public MainPage() {
            InitializeComponent();
            LoadWeatherData();
        }

        public async void LoadWeatherData() {
            try {
                GetWeatherDataDTO oWeatherData = await APIService.Instance.Get().WeatherData();
                List<GetWeatherForecastDataDTO> oWeatherForecastData = await APIService.Instance.Get().WeatherForecastData();

                City.Text = oWeatherData.sCity;
                Temperature.Text = Convert.ToString(oWeatherData.dTempC);
                Country.Text = oWeatherData.sCountry;
                //WindSpeed.Text = Convert.ToString(oWeatherData.dWindKph);
                //WindDirectory.Text = oWeatherData.sWindDir;
                //FeelsLike.Text = Convert.ToString(oWeatherData.dFeelsLikeC);
                ConditionWeather.Text = oWeatherData.sConditionText + "°C";

      } catch (Exception ex) {
                MessageBox.Show("Fehler beim Laden der Wetterdaten: " + ex.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}