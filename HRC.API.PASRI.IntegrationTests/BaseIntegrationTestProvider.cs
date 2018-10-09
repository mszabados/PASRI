using AutoMapper;
using HRC.DB.Master.Core;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;
using NUnit.Framework;
using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace HRC.API.PASRI.IntegrationTests
{
    /// <summary>
    /// Creates a <see cref="WebHost" /> server and <see cref="HttpClient" />
    /// for integration tests on the API controllers configured using the
    /// <see cref="TestStartup" /> class
    /// </summary>
    /// <example>
    /// <see cref="IUnitOfWork" /> in the UnitOfWork named field is created
    /// from <see cref="TestUnitOfWork" />
    /// </example>
    [TestFixture]
    public abstract class BaseIntegrationTestProvider : IDisposable
    {
        private readonly TestServer _server;
        protected readonly HttpClient Client;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMapper Mapper;

        protected const string HttpExceptionFormattedMessage =
            "An internal server error occurred, read through the response below to find the exception\r\n\r\n{0}";
        protected const string JsonMediaType = "application/json";
        protected const string CreatedModifiedBy = "IntegrationTests";

        protected BaseIntegrationTestProvider()
        {
            var integrationsTestsPath = PlatformServices.Default.Application.ApplicationBasePath;
            var applicationPath = Path.GetFullPath(Path.Combine(integrationsTestsPath, "../../../../HRC.API.PASRI"));

            _server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestStartup>()
                .UseContentRoot(applicationPath)
                .UseEnvironment("Development"));
            Client = _server.CreateClient();

            UnitOfWork = new TestUnitOfWork(new SqliteMasterDbContext());

            var config = new MapperConfiguration(cfg => new MappingProfile());
            Mapper = config.CreateMapper();
        }

        /// <summary>
        /// Builds a relative path to call the API endpoint.  Requires
        /// the Controller name to use the default route of /api/[controller]/[id]
        /// </summary>
        /// <param name="controllerName">Name of the controller class</param>
        /// <param name="identifier"></param>
        /// <returns></returns>
        protected static string GetRelativePath(string controllerName, string identifier = "")
        {
            var path = new StringBuilder();
            path.Append("/api/");
            path.Append(controllerName.ToLower().Replace("controller", string.Empty));
            if (string.IsNullOrWhiteSpace(identifier)) return path.ToString();
            path.Append("/");
            path.Append(identifier);

            return path.ToString();
        }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
            UnitOfWork.Dispose();
        }
    }
}
