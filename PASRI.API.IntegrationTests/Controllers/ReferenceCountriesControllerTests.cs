using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using PASRI.API.Core.Domain;
using PASRI.API.Dtos;
using PASRI.API.TestHelper;

namespace PASRI.API.IntegrationTests.Controllers
{
    [TestFixture]
    public class ReferenceCountriesControllerTests : BaseIntegrationTestProvider
    {
        private string _apiRelativePath =
            $"/api/{typeof(ReferenceCountriesControllerTests).Name.Replace("ControllerTests", "")}";

        [Test]
        public async Task GetAll_WhenCalled_HttpOkAndReturnsCollection()
        {
            var response = await Client.GetAsync(_apiRelativePath);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));

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

            ReferenceBaseDto queriedCountry =
                JsonConvert.DeserializeObject<ReferenceBaseDto>(responseString);

            ReferenceCountry preDefinedCountry =
                PreDefinedData.ReferenceCountries
                    .SingleOrDefault(c => c.Code == validCountryCode);

            Helper.AreObjectsEqual(queriedCountry, Mapper.Map<ReferenceCountry, ReferenceBaseDto>(preDefinedCountry));
        }
    }
}
