using System;
using System.Net.Http;

namespace WpfUI.Controllers
{
    public class ServiceClient
    {
        private static ServiceClient _instance;
        private HttpClient _httpClient;

        public static ServiceClient GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ServiceClient();
            }
            return _instance;
        }

        private ServiceClient()
        { 
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44379/");
        }

        public HttpClient GetClient()
        {
            return _httpClient;
        }
    }
}
