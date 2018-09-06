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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PASRI.API.IntegrationTests.Controllers
{
    [TestFixture]
    public class ReferenceCountriesControllerTests : BaseIntegrationTestProvider
    {
        private string _apiRelativePath =
            $"/api/{nameof(ReferenceCountriesController).Replace("Controller", "")}";

        [Test]
        public async Task GetAll_WhenCalled_HttpOkAndReturnsMatchingCollection()
        {
            // Arrange

            // Act
            var response = await Client.GetAsync(_apiRelativePath);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            IEnumerable<ReferenceBaseDto> apiReturnedCollection = 
                JsonConvert.DeserializeObject<IEnumerable<ReferenceBaseDto>>(responseString);
            IEnumerable<ReferenceBaseDto> preDefinedCollection =
                PreDefinedData.ReferenceCountries.Select(Mapper.Map<ReferenceCountry, ReferenceBaseDto>).ToList().AsEnumerable<ReferenceBaseDto>();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidCountryCode_HttpOkAndReturnsSingleCountry()
        {
            // Arrange
            var randomCountryCode = PreDefinedData.GetRandomCountryCode();

            // Act
            var response = await Client.GetAsync(
                $"{_apiRelativePath}/{randomCountryCode}");
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            ReferenceBaseDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceBaseDto>(responseString);

            ReferenceCountry preDefinedObject =
                PreDefinedData.ReferenceCountries
                    .SingleOrDefault(c => c.Code == randomCountryCode);

            AssertHelper.AreObjectsEqual(apiReturnedObject, 
                Mapper.Map<ReferenceCountry, ReferenceBaseDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidCountryCode_HttpNotFound()
        {
            // Arrange
            var notExistsCountryCode = PreDefinedData.GetNotExistsCountryCode();
            
            // Act
            var response = await Client.GetAsync(
                $"{_apiRelativePath}/{notExistsCountryCode}");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Create_ValidPayload_HttpCreatedAndReturnsNewCountry()
        {
            // Arrange
            var notExistsCountryCode = PreDefinedData.GetNotExistsCountryCode();
            var newCountryDto = new ReferenceBaseDto()
            {
                Code = notExistsCountryCode,
                DisplayText = "New Country",
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(_apiRelativePath, 
                new StringContent(
                    JsonConvert.SerializeObject(newCountryDto), 
                    Encoding.UTF8, 
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            ReferenceBaseDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceBaseDto>(responseString);

            AssertHelper.AreObjectsEqual(apiReturnedObject, newCountryDto);
        }

        [Test]
        public async Task Create_MalformedPayload_HttpBadRequest()
        {
            // Arrange
            var notExistsCountryCode = PreDefinedData.GetNotExistsCountryCode();
            var newCountryDto = new ReferenceBaseDto()
            {
                Code = notExistsCountryCode,
                // Display text is required, keep it missing
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(_apiRelativePath, 
                new StringContent(
                    JsonConvert.SerializeObject(newCountryDto), 
                    Encoding.UTF8, 
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Create_ExistingCountry_HttpConflict()
        {
            // Arrange
            var newCountryDto = new ReferenceBaseDto()
            {
                Code = PreDefinedData.GetRandomCountryCode(),
                DisplayText = "Create Test",
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(_apiRelativePath, 
                new StringContent(
                    JsonConvert.SerializeObject(newCountryDto), 
                    Encoding.UTF8, 
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task Update_ValidCountry_HttpNoContent()
        {
            // Arrange
            var randomCountryCode = PreDefinedData.GetRandomCountryCode();
            ReferenceCountry apiUpdatingCountry = UnitOfWork.ReferenceCountries.Get(randomCountryCode);
            apiUpdatingCountry.DisplayText = "Update Test";

            // Act
            var response = await Client.PutAsync(
                $"{_apiRelativePath}/{apiUpdatingCountry.Code}", 
                new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingCountry), 
                    Encoding.UTF8, 
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            ReferenceCountry dbUpdatedCountry = UnitOfWork.ReferenceCountries.Get(apiUpdatingCountry.Code);
            AssertHelper.AreObjectsEqual(apiUpdatingCountry, dbUpdatedCountry);
        }

        [Test]
        public async Task Update_MalformedPayload_HttpBadRequest()
        {
            // Arrange
            var apiUpdatingCountry = new ReferenceBaseDto()
            {
                Code = PreDefinedData.GetRandomCountryCode(),
                // Display text is required, keep it missing
            };

            // Act
            var response = await Client.PutAsync(
                $"{_apiRelativePath}/{apiUpdatingCountry.Code}", 
                new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingCountry), 
                    Encoding.UTF8, 
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Update_InvalidCountry_HttpNotFound()
        {
            // Arrange
            var apiUpdatingCountry = new ReferenceBaseDto()
            {
                Code = PreDefinedData.GetNotExistsCountryCode(),
                DisplayText = "Update Test"
            };

            // Act
            var response = await Client.PutAsync(
                $"{_apiRelativePath}/{apiUpdatingCountry.Code}", 
                new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingCountry), 
                    Encoding.UTF8, 
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Delete_ValidCountry_HttpNoContent()
        {
            // Arrange
            var randomCountryCode = PreDefinedData.GetRandomCountryCode();

            // Act
            var response = await Client.DeleteAsync(
                $"{_apiRelativePath}/{randomCountryCode}");
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public async Task Delete_InvalidCountry_HttpNotFound()
        {
            // Arrange
            var notExistsCountryCode = PreDefinedData.GetNotExistsCountryCode();

            // Act
            var response = await Client.DeleteAsync(
                $"{_apiRelativePath}/{notExistsCountryCode}");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
