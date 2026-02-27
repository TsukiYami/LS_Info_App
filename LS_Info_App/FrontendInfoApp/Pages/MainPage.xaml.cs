using Entities.DTOs.GET;
using FrontendInfoApp.APIConnection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FrontendInfoApp.Pages
{
  /// <summary>
  /// Interaktionslogik für MainPage.xaml
  /// </summary>
  public partial class MainPage : Page
  {


    public MainPage()
    {
      InitializeComponent();
      LoadWeatherData();
    }

    public async void LoadWeatherData()
    {
      try
      {
        GetWeatherDataDTO oWeatherData = await APIService.Instance.Get().WeatherData();
        List<GetWeatherForecastDataDTO> oWeatherForecastData = await APIService.Instance.Get().WeatherForecastData();

        foreach (GetWeatherForecastDataDTO oData in oWeatherForecastData)
        {
          WeatherForecastList.Items.Add(new ListBoxItem() { Content = $"{oData.oForDate}: {oData.sConditionText}, {oData.dMaxTempC}°C" });
        }

        City.Text = oWeatherData.sCity;
        Temperature.Text = Convert.ToString(oWeatherData.dTempC);
        Country.Text = oWeatherData.sCountry;
        WindSpeed.Text = Convert.ToString(oWeatherData.dWindKph);
        WindDirectory.Text = oWeatherData.sWindDir;
        FeelsLike.Text = Convert.ToString(oWeatherData.dFeelsLikeC);
        ConditionWeather.Text = oWeatherData.sConditionText + "°C";

        //SetWeatherBackground(oWeatherData.sConditionText);

      }
      catch (Exception ex)
      {
        MessageBox.Show("Fehler beim Laden der Wetterdaten: " + ex.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    /*private void SetWeatherBackground(string? conditionText)
    {
      var c = (conditionText ?? "").Trim().ToLowerInvariant();

      string imagePath = c switch
      {
        // Sonne
        var s when s.Contains("sunny") || s.Contains("clear")
            => "/Dateien/Sunny.png",

        // Bewölkt
        var s when s.Contains("cloudy") || s.Contains("overcast")
            => "/Dateien/Cloudy.png",

        // Nebel
        var s when s.Contains("foggy") || s.Contains("mist") || s.Contains("haze")
            => "/Dateien/Foggy.png",

        // Gewitter
        var s when s.Contains("thunder") || s.Contains("storm")
            => "/Dateien/Storm.png",

        // Fallback
        _ => "/Dateien/Cloudy.png"
      };

      var brush = new ImageBrush
      {
        ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative)),
        Stretch = Stretch.UniformToFill
      };

      MainGrid.Background = brush;   
    }*/
  }
}