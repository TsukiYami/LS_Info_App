using Entities.DTOs.GET;
using FrontendInfoApp.APIConnection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace FrontendInfoApp.Pages {
    /// <summary>
    /// Interaktionslogik für MainPage.xaml
    /// </summary>
    public partial class MainPage : Page {
        private DispatcherTimer oTimer;

        public MainPage() {
            InitializeComponent();
            LoadWeatherData();
            SetTimer();
        }

        private void SetTimer() {
            oTimer = new DispatcherTimer();
            oTimer.Interval = TimeSpan.FromHours(1);
            oTimer.Tick += (sender, e) => LoadWeatherData();
            oTimer.Start();
        }

        public async void LoadWeatherData() {
            try {
                MessageBox.Show("Loading weather data...");
                GetWeatherDataDTO oWeatherData = await APIService.Instance.Get().WeatherData();
                List<GetWeatherForecastDataDTO> oWeatherForecastData = await APIService.Instance.Get().WeatherForecastData();

                Application.Current.Dispatcher.Invoke(()=> {

                    WeatherForecastList.Items.Clear();
                    foreach (GetWeatherForecastDataDTO oData in oWeatherForecastData) {
                        WeatherForecastList.Items.Add(new ListBoxItem() { Content = $"{oData.oForDate}: {oData.sConditionText}, {oData.dMaxTempC} °C" });
                    }

                    City.Text = oWeatherData.sCity;
                    Temperature.Text = Convert.ToString(oWeatherData.dTempC + " °C");
                    Country.Text = oWeatherData.sCountry;
                    WindSpeed.Text = Convert.ToString(oWeatherData.dWindKph + " km/h");
                    WindDirectory.Text = oWeatherData.sWindDir;
                    FeelsLike.Text = Convert.ToString(oWeatherData.dFeelsLikeC + " °C");
                    ConditionWeather.Text = oWeatherData.sConditionText;

                    ConditionWeather.Text = oWeatherData.sConditionText;

                    string condition = oWeatherData.sConditionText.ToLower();

                    if (condition.Contains("sun") || condition.Contains("Partly Cloudy")) {
                        SetBackground("sunny.png");
                    } else if (condition.Contains("rain")) {
                        SetBackground("storm.png");
                    } else if (condition.Contains("fog")) {
                        SetBackground("foggy.png");
                    } else if (condition.Contains("storm") || condition.Contains("thunder")) {
                        SetBackground("storm.png");
                    } else {
                        SetBackground("sunny.png");
                    }
                });
            } catch (Exception ex) {
                MessageBox.Show("Fehler beim Laden der Wetterdaten: " + ex.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetBackground(string fileName) {
            var uri = new Uri($"pack://application:,,,/Dateien/{fileName}", UriKind.Absolute);

            MainGrid.Background = new ImageBrush(new BitmapImage(uri)) {
                Stretch = Stretch.UniformToFill
            };
        }
    }
}