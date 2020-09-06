using Easy.Common;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;

namespace WpfUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DistrictService _districtService;

        public MainWindow()
        {
            InitializeComponent();
            _districtService = new DistrictService();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var districtTotalCount =  await DistrictService.GetDistrictCount();
            districtCount_lbl.Text += districtTotalCount.ToString();
        }
    }


    class DistrictService
    {
        
        public DistrictService()
        {
        }

        public static async Task<int> GetDistrictCount()
        {
            try
            {
                using (var _client = new HttpClient())
                {

                    var response = await _client.GetAsync("http://localhost:44379/district/GetDistrictCount");
                    int districtTotalCount = JsonConvert.DeserializeObject<int>(await response.Content.ReadAsStringAsync());

                    return districtTotalCount;
                }
            }
            catch(Exception e)
            {

            }
            return 0;
        }
    }
}