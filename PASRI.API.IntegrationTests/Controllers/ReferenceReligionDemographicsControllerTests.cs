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
    /// Integration test class for the <see cref="ReferenceReligionDemographicsController"/> methods.
    /// Method names should reflect the following pattern:
    /// MethodBeingTested_Scenario_ExpectedBehavior
    /// </summary>
    [TestFixture]
    public class ReferenceReligionDemographicsControllerTests : BaseIntegrationTestProvider
    {
        [Test]
        public async Task GetAll_WhenCalled_HttpOkAndReturnsMatchingCollection()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController));

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            IEnumerable<ReferenceReligionDemographicDto> apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceReligionDemographicDto>>(responseString);
            IEnumerable<ReferenceReligionDemographicDto> preDefinedCollection =
                PreDefinedData.ReferenceReligionDemographics.Select(Mapper.Map<ReferenceReligionDemographic, ReferenceReligionDemographicDto>).ToList().AsEnumerable<ReferenceReligionDemographicDto>();
            ((List<ReferenceReligionDemographicDto>)apiReturnedCollection).Sort();
            ((List<ReferenceReligionDemographicDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidReligionDemographicCode_HttpOkAndReturnsSingleReligionDemographic()
        {
            // Arrange
            var randomReligionDemographicCode = PreDefinedData.GetRandomReligionDemographicCode();
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController), randomReligionDemographicCode);

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            ReferenceReligionDemographicDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceReligionDemographicDto>(responseString);

            ReferenceReligionDemographic preDefinedObject =
                PreDefinedData.ReferenceReligionDemographics
                    .SingleOrDefault(c => c.Code == randomReligionDemographicCode);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceReligionDemographic, ReferenceReligionDemographicDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidReligionDemographicCode_HttpNotFound()
        {
            // Arrange
            var notExistsReligionDemographicCode = PreDefinedData.GetNotExistsReligionDemographicCode();
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController), notExistsReligionDemographicCode);

            // Act
            var response = await Client.GetAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Create_ValidPayload_HttpCreatedAndReturnsNewReligionDemographic()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController));
            var notExistsReligionDemographicCode = PreDefinedData.GetNotExistsReligionDemographicCode();
            var newReligionDemographicDto = new ReferenceReligionDemographicDto()
            {
                Code = notExistsReligionDemographicCode,
                DisplayText = "New ReligionDemographic",
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newReligionDemographicDto),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            ReferenceReligionDemographicDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceReligionDemographicDto>(responseString);

            AssertHelper.AreObjectsEqual(apiReturnedObject, newReligionDemographicDto);
        }

        [Test]
        public async Task Create_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController));

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
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController));
            var notExistsReligionDemographicCode = PreDefinedData.GetNotExistsReligionDemographicCode();
            var newReligionDemographicDto = new ReferenceReligionDemographicDto()
            {
                Code = notExistsReligionDemographicCode,
                // Display text is required, keep it missing
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newReligionDemographicDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Create_ExistingReligionDemographic_HttpConflict()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController));
            var newReligionDemographicDto = new ReferenceReligionDemographicDto()
            {
                Code = PreDefinedData.GetRandomReligionDemographicCode(),
                DisplayText = "Create Test",
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newReligionDemographicDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task Update_ValidReligionDemographic_HttpNoContent()
        {
            // Arrange
            var randomReligionDemographicCode = PreDefinedData.GetRandomReligionDemographicCode();
            ReferenceReligionDemographic apiUpdatingReligionDemographic = UnitOfWork.ReferenceReligionDemographics.Get(randomReligionDemographicCode);
            apiUpdatingReligionDemographic.DisplayText = "Update Test";
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController), randomReligionDemographicCode);

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingReligionDemographic),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            ReferenceReligionDemographic dbUpdatedReligionDemographic = UnitOfWork.ReferenceReligionDemographics.Get(apiUpdatingReligionDemographic.Code);
            AssertHelper.AreObjectsEqual(apiUpdatingReligionDemographic, dbUpdatedReligionDemographic);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomReligionDemographicCode = PreDefinedData.GetRandomReligionDemographicCode();
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController), randomReligionDemographicCode);

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
            var randomReligionDemographicCode = PreDefinedData.GetRandomReligionDemographicCode();
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController), randomReligionDemographicCode);
            var apiUpdatingReligionDemographic = new ReferenceReligionDemographicDto()
            {
                Code = randomReligionDemographicCode,
                // Display text is required, keep it missing
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingReligionDemographic),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Update_InvalidReligionDemographic_HttpNotFound()
        {
            // Arrange
            var notExistsReligionDemographicCode = PreDefinedData.GetNotExistsReligionDemographicCode();
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController), notExistsReligionDemographicCode);
            var apiUpdatingReligionDemographic = new ReferenceReligionDemographicDto()
            {
                Code = notExistsReligionDemographicCode,
                DisplayText = "Update Test"
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingReligionDemographic),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Delete_ValidReligionDemographic_HttpNoContent()
        {
            // Arrange
            var randomReligionDemographicCode = PreDefinedData.GetRandomReligionDemographicCode();
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController), randomReligionDemographicCode);

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
        public async Task Delete_InvalidReligionDemographic_HttpNotFound()
        {
            // Arrange
            var notExistsReligionDemographicCode = PreDefinedData.GetNotExistsReligionDemographicCode();
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController), notExistsReligionDemographicCode);

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
