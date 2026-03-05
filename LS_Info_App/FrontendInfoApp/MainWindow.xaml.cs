using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FrontendInfoApp.Pages;

namespace FrontendInfoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
          InitializeComponent();
          var fp = new MainPage();
          NavFrame.Navigate(fp);
    }

        public void SetPage(Page nextPage)
        {
          NavFrame.Navigate(nextPage);
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
          if (e.ChangedButton == MouseButton.Left)
            this.DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
          this.Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
          this.WindowState = WindowState.Minimized;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
          if (this.WindowState == WindowState.Maximized)
            ((Border)this.Content).CornerRadius = new CornerRadius(0);
          else
            ((Border)this.Content).CornerRadius = new CornerRadius(20);
        }

        
  }
}