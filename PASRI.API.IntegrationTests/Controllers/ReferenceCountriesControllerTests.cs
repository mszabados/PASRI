using Newtonsoft.Json;
using NUnit.Framework;
using PASRI.API.Controllers;
using PASRI.API.Core.Domain;
using PASRI.API.Dtos;
using PASRI.API.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PASRI.API.IntegrationTests.Controllers
{
    [TestFixture]
    public class ReferenceCountriesControllerTests : BaseIntegrationTestProvider
    {
        private string _apiRelativePath =
            $"/api/{nameof(ReferenceCountriesController).Replace("Controller", "")}";

        [Test]
        public async Task GetAll_WhenCalled_HttpOkAndReturnsCollection()
        {
            var response = await Client.GetAsync(_apiRelativePath);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            IEnumerable<ReferenceBaseDto> queriedCountries = 
                JsonConvert.DeserializeObject<IEnumerable<ReferenceBaseDto>>(responseString);

            IEnumerable<ReferenceBaseDto> preDefinedCountries =
                PreDefinedData.ReferenceCountries.Select(Mapper.Map<ReferenceCountry, ReferenceBaseDto>).ToList().AsEnumerable<ReferenceBaseDto>();

            Helper.AreObjectsEqual(queriedCountries, preDefinedCountries);
        }

        [Test]
        public async Task Get_ValidCountryCode_HttpOkAndReturnsSingleCountry()
        {
            var validCountryCode = "US";
            var response = await Client.GetAsync($"{_apiRelativePath}/{validCountryCode}");
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            ReferenceBaseDto queriedCountry =
                JsonConvert.DeserializeObject<ReferenceBaseDto>(responseString);

            ReferenceCountry preDefinedCountry =
                PreDefinedData.ReferenceCountries
                    .SingleOrDefault(c => c.Code == validCountryCode);

            Helper.AreObjectsEqual(queriedCountry, Mapper.Map<ReferenceCountry, ReferenceBaseDto>(preDefinedCountry));
        }

        [Test]
        public async Task Get_InvalidCountryCode_HttpNotFound()
        {
            var invalidCountryCode = "ZZ";
            var response = await Client.GetAsync($"{_apiRelativePath}/{invalidCountryCode}");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
