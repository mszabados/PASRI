using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;
using NUnit.Framework;
using System;
using System.IO;
using System.Net.Http;
using PASRI.API.TestHelper;

namespace PASRI.API.IntegrationTests
{
    [TestFixture]
    public class BaseIntegrationTestProvider : IDisposable
    {
        protected readonly TestServer Server;
        protected readonly HttpClient Client;

        public BaseIntegrationTestProvider()
        {
            var integrationsTestsPath = PlatformServices.Default.Application.ApplicationBasePath;
            var applicationPath = Path.GetFullPath(Path.Combine(integrationsTestsPath, "../../../../PASRI"));

            Server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestStartup>()
                .UseContentRoot(applicationPath)
                .UseEnvironment("Development"));
            Client = Server.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
        }
    }
}
