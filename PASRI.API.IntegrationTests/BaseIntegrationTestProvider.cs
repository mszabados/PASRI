using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;
using NUnit.Framework;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace PASRI.API.IntegrationTests
{
    [TestFixture]
    public class BaseIntegrationTestProvider : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public BaseIntegrationTestProvider()
        {
            var integrationsTestsPath = PlatformServices.Default.Application.ApplicationBasePath;
            var applicationPath = Path.GetFullPath(Path.Combine(integrationsTestsPath, "../../../../PASRI"));

            _server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestStartup>()
                .UseContentRoot(applicationPath)
                .UseEnvironment("Development"));
            _client = _server.CreateClient();
        }

        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }

        [Test]
        public async Task Index_Get_ReturnsSwaggerUIBundle()
        {
            var response = await _client.GetAsync("/index.html");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(responseString.Contains("SwaggerUIBundle"));
        }
    }
}
