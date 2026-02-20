using Entities.DTOs.GET;
using FrontendInfoApp.APIConnection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Entities.DTOs.GET;
using Newtonsoft.Json;
using System.Net;


namespace FrontendInfoApp.Pages
{
    /// <summary>
    /// Interaktionslogik für MainPage.xaml
    /// </summary>
    public partial class MainPage : Page {

        /*private readonly MainViewModel _vm = new();




        public class MainViewModel
        {
            public ObservableCollection<GetWeatherDataDTO> WeatherItems { get; } = new();

            public void Load()
            {
                var data = APIService.Instance.Get().WeatherData();

                WeatherItems.Clear();

                if (data != null)
                    foreach (var item in data)
                        WeatherItems.Add(item);
            }
        }*/
        public MainPage() {
            InitializeComponent();
            LoadWeatherData();
        }

        public void LoadWeatherData() {
            try {
                GetWeatherDataDTO weatherData = APIService.Instance.Get().WeatherData();
              
                City.Text = weatherData.sCity;
                Temperature.Text = Convert.ToString(weatherData.dTempC);
                Country.Text = weatherData.sCountry;
                WindSpeed.Text = Convert.ToString(weatherData.dWindKph);
                WindDirectory.Text = weatherData.sWindDir;
                FeelsLike.Text = Convert.ToString(weatherData.dFeelsLikeC);
                ConditionWeather.Text = weatherData.sConditionText;

      } catch (Exception ex) {
                MessageBox.Show("Fehler beim Laden der Wetterdaten: " + ex.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}