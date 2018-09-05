using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;
using NUnit.Framework;
using PASRI.API.TestHelper;
using System;
using System.IO;
using System.Net.Http;
using AutoMapper;
using PASRI.API.Core;

namespace PASRI.API.IntegrationTests
{
    /// <summary>
    /// Creates a <see cref="WebHost"/> server and <see cref="HttpClient" />
    /// for integration tests on the API controllers configured using the
    /// <see cref="TestStartup"/> class
    /// </summary>
    /// <example>
    /// <see cref="IUnitOfWork"/> in the UnitOfWork named field is created
    /// from <see cref="TestUnitOfWork"/>
    /// </example>
    [TestFixture]
    public class BaseIntegrationTestProvider : IDisposable
    {
        protected readonly TestServer Server;
        protected readonly HttpClient Client;
        protected readonly IMapper Mapper;

        protected const string HttpExceptionFormattedMessage =
            "An internal server error occurred, read through the response below to find the exception\r\n\r\n{0}";

        public BaseIntegrationTestProvider()
        {
            var integrationsTestsPath = PlatformServices.Default.Application.ApplicationBasePath;
            var applicationPath = Path.GetFullPath(Path.Combine(integrationsTestsPath, "../../../../PASRI"));

            Server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestStartup>()
                .UseContentRoot(applicationPath)
                .UseEnvironment("Development"));
            Client = Server.CreateClient();

            var config = new MapperConfiguration(cfg => new MappingProfile());
            Mapper = config.CreateMapper();
        }

        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
        }
    }
}
