using System;
using System.Net.Http;
using System.Net.Http.Headers;
using InternalAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace APITests.Integration_Tests
{
    class InternalApiFakeServer
    {
        private HttpClient _internalClient;
        private TestServer _internalServer;

        public HttpClient GetInternalClient()
        {
            return _internalClient;
        }

        public void StartServer()
        {
            _internalServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _internalClient = _internalServer.CreateClient();
            _internalClient.BaseAddress = new Uri("http://localhost:44379/");
            _internalClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Dispose()
        {
            _internalClient.Dispose();
            _internalServer.Dispose();
        }
    }
}
