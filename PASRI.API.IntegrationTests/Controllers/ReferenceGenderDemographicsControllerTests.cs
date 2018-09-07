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
    /// Integration test class for the <see cref="ReferenceGenderDemographicsController"/> methods.
    /// Method names should reflect the following pattern:
    /// MethodBeingTested_Scenario_ExpectedBehavior
    /// </summary>
    [TestFixture]
    public class ReferenceGenderDemographicsControllerTests : BaseIntegrationTestProvider
    {
        [Test]
        public async Task GetAll_WhenCalled_HttpOkAndReturnsMatchingCollection()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController));

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            IEnumerable<ReferenceGenderDemographicDto> apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceGenderDemographicDto>>(responseString);
            IEnumerable<ReferenceGenderDemographicDto> preDefinedCollection =
                PreDefinedData.ReferenceGenderDemographics.Select(Mapper.Map<ReferenceGenderDemographic, ReferenceGenderDemographicDto>).ToList().AsEnumerable<ReferenceGenderDemographicDto>();
            ((List<ReferenceGenderDemographicDto>)apiReturnedCollection).Sort();
            ((List<ReferenceGenderDemographicDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidGenderDemographicCode_HttpOkAndReturnsSingleGenderDemographic()
        {
            // Arrange
            var randomGenderDemographicCode = PreDefinedData.GetRandomGenderDemographicCode();
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController), randomGenderDemographicCode);

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            ReferenceGenderDemographicDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceGenderDemographicDto>(responseString);

            ReferenceGenderDemographic preDefinedObject =
                PreDefinedData.ReferenceGenderDemographics
                    .SingleOrDefault(c => c.Code == randomGenderDemographicCode);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceGenderDemographic, ReferenceGenderDemographicDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidGenderDemographicCode_HttpNotFound()
        {
            // Arrange
            var notExistsGenderDemographicCode = PreDefinedData.GetNotExistsGenderDemographicCode();
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController), notExistsGenderDemographicCode);

            // Act
            var response = await Client.GetAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Create_ValidPayload_HttpCreatedAndReturnsNewGenderDemographic()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController));
            var notExistsGenderDemographicCode = PreDefinedData.GetNotExistsGenderDemographicCode();
            var newGenderDemographicDto = new ReferenceGenderDemographicDto()
            {
                Code = notExistsGenderDemographicCode,
                DisplayText = "New GenderDemographic",
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newGenderDemographicDto),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            ReferenceGenderDemographicDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceGenderDemographicDto>(responseString);

            AssertHelper.AreObjectsEqual(apiReturnedObject, newGenderDemographicDto);
        }

        [Test]
        public async Task Create_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController));

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
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController));
            var notExistsGenderDemographicCode = PreDefinedData.GetNotExistsGenderDemographicCode();
            var newGenderDemographicDto = new ReferenceGenderDemographicDto()
            {
                Code = notExistsGenderDemographicCode,
                // Display text is required, keep it missing
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newGenderDemographicDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Create_ExistingGenderDemographic_HttpConflict()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController));
            var newGenderDemographicDto = new ReferenceGenderDemographicDto()
            {
                Code = PreDefinedData.GetRandomGenderDemographicCode(),
                DisplayText = "Create Test",
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newGenderDemographicDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task Update_ValidGenderDemographic_HttpNoContent()
        {
            // Arrange
            var randomGenderDemographicCode = PreDefinedData.GetRandomGenderDemographicCode();
            ReferenceGenderDemographic apiUpdatingGenderDemographic = UnitOfWork.ReferenceGenderDemographics.Get(randomGenderDemographicCode);
            apiUpdatingGenderDemographic.DisplayText = "Update Test";
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController), randomGenderDemographicCode);

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingGenderDemographic),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            ReferenceGenderDemographic dbUpdatedGenderDemographic = UnitOfWork.ReferenceGenderDemographics.Get(apiUpdatingGenderDemographic.Code);
            AssertHelper.AreObjectsEqual(apiUpdatingGenderDemographic, dbUpdatedGenderDemographic);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomGenderDemographicCode = PreDefinedData.GetRandomGenderDemographicCode();
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController), randomGenderDemographicCode);

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
            var randomGenderDemographicCode = PreDefinedData.GetRandomGenderDemographicCode();
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController), randomGenderDemographicCode);
            var apiUpdatingGenderDemographic = new ReferenceGenderDemographicDto()
            {
                Code = randomGenderDemographicCode,
                // Display text is required, keep it missing
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingGenderDemographic),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Update_InvalidGenderDemographic_HttpNotFound()
        {
            // Arrange
            var notExistsGenderDemographicCode = PreDefinedData.GetNotExistsGenderDemographicCode();
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController), notExistsGenderDemographicCode);
            var apiUpdatingGenderDemographic = new ReferenceGenderDemographicDto()
            {
                Code = notExistsGenderDemographicCode,
                DisplayText = "Update Test"
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingGenderDemographic),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Delete_ValidGenderDemographic_HttpNoContent()
        {
            // Arrange
            var randomGenderDemographicCode = PreDefinedData.GetRandomGenderDemographicCode();
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController), randomGenderDemographicCode);

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
        public async Task Delete_InvalidGenderDemographic_HttpNotFound()
        {
            // Arrange
            var notExistsGenderDemographicCode = PreDefinedData.GetNotExistsGenderDemographicCode();
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController), notExistsGenderDemographicCode);

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
