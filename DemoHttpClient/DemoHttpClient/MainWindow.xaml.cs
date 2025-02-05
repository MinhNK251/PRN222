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
using System.Net.Http;
using System;

namespace DemoHttpClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        readonly HttpClient client = new HttpClient();
        private async void btnView_Click(object sender, RoutedEventArgs e)
        {
            string uri = txtURL.Text;
            try
            {
                string response = await client.GetStringAsync(uri);
                txtSource.Text = response.Trim();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Exception : {ex.Message}");
            }
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtSource.Text = string.Empty;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}