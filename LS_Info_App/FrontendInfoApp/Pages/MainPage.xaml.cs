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

        /// <summary>
        /// Wetter und Wettervorhersagedaten von der API abrufen und die UI entsprechend aktualisieren.
        /// </summary>

        public async void LoadWeatherData() {
            try {               
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

                    // Hintergrundbild basierend auf der Wetterbedingung setzen


                    string condition = oWeatherData.sConditionText.ToLower();

                    if (condition.Contains("sun") || condition.Contains("Partly Cloudy")) {
                        SetBackground("sunny.png");
                    } else if (condition.Contains("rain")) {
                        SetBackground("storm.png");
                    } else if (condition.Contains("fog")) {
                        SetBackground("foggy.png");
                    } else if (condition.Contains("storm") || condition.Contains("thunder")) {
                        SetBackground("storm.png");
                    } else if (condition.Contains("snow")) { 
                       SetBackground("snow.png");
                    } else {
                        SetBackground("sunny.png");
                    }

                    // Windrichtung in lesbare Form umwandeln

                    string directory = oWeatherData.sWindDir;

                    if (directory == "N")
                    {
                        WindDirectory.Text = "North";
                    }
                    else if (directory == "NE")
                    {
                        WindDirectory.Text = "North-East";
                    }
                    else if (directory == "E")
                    {
                        WindDirectory.Text = "East";
                    }
                    else if (directory == "SE")
                    {
                        WindDirectory.Text = "South-East";
                    }
                    else if (directory == "S")
                    {
                        WindDirectory.Text = "South";
                    }
                    else if (directory == "SW")
                    {
                        WindDirectory.Text = "South-West";
                    }
                    else if (directory == "W")
                    {
                        WindDirectory.Text = "West";
                    }
                    else if (directory == "NW")
                    {
                        WindDirectory.Text = "North-West";
                    }
                    else
                    {
                        WindDirectory.Text = directory;
                    }

                    // Wetter-Icon basierend auf der Wetterbedingung setzen

                    if (condition.Contains("sun") || condition.Contains("clear") || condition.Contains("partly cloudy"))
                    {
                        SetWeatherIcon("weather-sunny-custom.png");
                    }
                    else if (condition.Contains("cloud"))
                    {
                        SetWeatherIcon("weather-partly-cloudy-custom.png");
                    }
                    else if (condition.Contains("rain"))
                    {
                        SetWeatherIcon("weather-pouring-custom.png");
                    }
                    else if (condition.Contains("snow"))
                    {
                        SetWeatherIcon("weather-snowy-custom.png");
                    }
                    else if (condition.Contains("fog") || condition.Contains("mist"))
                    {
                        SetWeatherIcon("weather-cloudy-custom.png");
                    }
                    else if (condition.Contains("storm") || condition.Contains("thunder"))
                    {
                        SetWeatherIcon("weather-lightning-rainy-custom.png");
                    }
                    else
                    {
                        SetWeatherIcon("weather-sunny-custom.png");
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

        private void SetWeatherIcon(string fileName)
        {
            var uri = new Uri($"pack://application:,,,/Dateien/{fileName}", UriKind.Absolute);
            WeatherIcon.Source = new BitmapImage(uri);         
        }
    }
}