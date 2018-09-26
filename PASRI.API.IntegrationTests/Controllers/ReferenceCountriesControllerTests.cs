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
    /// <summary>
    /// Integration test class for the <see cref="ReferenceCountriesController"/> methods.
    /// Method names should reflect the following pattern:
    /// MethodBeingTested_Scenario_ExpectedBehavior
    /// </summary>
    [TestFixture]
    public class ReferenceCountriesControllerTests : BaseIntegrationTestProvider
    {
        [Test]
        public async Task GetAll_WhenCalled_HttpOkAndReturnsMatchingCollection()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceCountriesController));

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            IEnumerable<ReferenceCountryDto> apiReturnedCollection = 
                JsonConvert.DeserializeObject<IEnumerable<ReferenceCountryDto>>(responseString);
            IEnumerable<ReferenceCountryDto> preDefinedCollection =
                PreDefinedData.ReferenceCountries.Select(Mapper.Map<ReferenceCountry, ReferenceCountryDto>).ToList().AsEnumerable<ReferenceCountryDto>();
            ((List<ReferenceCountryDto>)apiReturnedCollection).Sort();
            ((List<ReferenceCountryDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidCountryCode_HttpOkAndReturnsSingleCountry()
        {
            // Arrange
            var randomCountryId = PreDefinedData.GetRandomCountryId();
            var path = GetRelativePath(nameof(ReferenceCountriesController), randomCountryId.ToString());

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            ReferenceCountryDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceCountryDto>(responseString);

            ReferenceCountry preDefinedObject =
                PreDefinedData.ReferenceCountries
                    .SingleOrDefault(c => c.Id == randomCountryId);

            AssertHelper.AreObjectsEqual(apiReturnedObject, 
                Mapper.Map<ReferenceCountry, ReferenceCountryDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidCountryId_HttpNotFound()
        {
            // Arrange
            var notExistsCountryCode = PreDefinedData.GetNotExistsCountryCode();
            var path = GetRelativePath(nameof(ReferenceCountriesController), Int32.MaxValue.ToString());

            // Act
            var response = await Client.GetAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Create_ValidPayload_HttpCreatedAndReturnsNewCountry()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceCountriesController));
            var notExistsCountryCode = PreDefinedData.GetNotExistsCountryCode();
            var newCountryDto = new ReferenceCountryDto()
            {
                Code = notExistsCountryCode,
                LongName = "New Country",
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newCountryDto), 
                    Encoding.UTF8, 
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            ReferenceCountryDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceCountryDto>(responseString);

            Assert.That(apiReturnedObject.Id, Is.GreaterThan(0));

            newCountryDto.Id = apiReturnedObject.Id;
            AssertHelper.AreObjectsEqual(apiReturnedObject, newCountryDto);
        }

        [Test]
        public async Task Create_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceCountriesController));

            // Act
            var response = await Client.PostAsync(path,
                new StringContent(string.Empty, Encoding.UTF8));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Create_MalformedPayload_HttpBadRequest()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceCountriesController));

            var newCountryDto = new ReferenceCountryDto()
            {
                // Code is required, keep it missing
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
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
            var path = GetRelativePath(nameof(ReferenceCountriesController));
            var randomCountryId = PreDefinedData.GetRandomCountryId();
            var randomCountry = PreDefinedData.ReferenceCountries[randomCountryId - 1];

            var newCountryDto = new ReferenceCountryDto()
            {
                Code = randomCountry.Code,
                LongName = "Create Test",
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
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
            var randomCountryId = PreDefinedData.GetRandomCountryId();

            ReferenceCountry apiUpdatingCountry = UnitOfWork.ReferenceCountries.Get(randomCountryId);
            apiUpdatingCountry.LongName = "Update Test";
            var path = GetRelativePath(nameof(ReferenceCountriesController), randomCountryId.ToString());

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingCountry), 
                    Encoding.UTF8, 
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            ReferenceCountry dbUpdatedCountry = UnitOfWork.ReferenceCountries.Get(apiUpdatingCountry.Id);
            AssertHelper.AreObjectsEqual(apiUpdatingCountry, dbUpdatedCountry);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomCountryId = PreDefinedData.GetRandomCountryId();
            var path = GetRelativePath(nameof(ReferenceCountriesController), randomCountryId.ToString());

            // Act
            var response = await Client.PutAsync(path,
                new StringContent(string.Empty, Encoding.UTF8));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Update_MalformedPayload_HttpBadRequest()
        {
            // Arrange
            var randomCountryId = PreDefinedData.GetRandomCountryId();
            var path = GetRelativePath(nameof(ReferenceCountriesController), randomCountryId.ToString());
            var apiUpdatingCountry = new ReferenceCountryDto()
            {
                Id = randomCountryId
                // Code is required, keep it missing
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
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
            var notExistsCountryCode = PreDefinedData.GetNotExistsCountryCode();
            var path = GetRelativePath(nameof(ReferenceCountriesController), Int32.MaxValue.ToString());
            var apiUpdatingCountry = new ReferenceCountryDto()
            {
                Id = Int32.MaxValue,
                Code = notExistsCountryCode,
                LongName = "Update Test"
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
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
            var randomCountryId = PreDefinedData.GetRandomCountryId();
            var path = GetRelativePath(nameof(ReferenceCountriesController), randomCountryId.ToString());

            // Act
            var response = await Client.DeleteAsync(path);
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
            var path = GetRelativePath(nameof(ReferenceCountriesController), Int32.MaxValue.ToString());

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
