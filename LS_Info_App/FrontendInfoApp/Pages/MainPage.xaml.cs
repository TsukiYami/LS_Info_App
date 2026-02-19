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
    public partial class MainPage : Page
    {

        private readonly MainViewModel _vm = new();


        public MainPage()
        {
               
            InitializeComponent();

            DataContext = _vm;

            Loaded += (_, __) => _vm.Load();           
        }


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

        }


    }
}
