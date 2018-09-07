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
    /// Integration test class for the <see cref="ReferenceEthnicGroupDemographicsController"/> methods.
    /// Method names should reflect the following pattern:
    /// MethodBeingTested_Scenario_ExpectedBehavior
    /// </summary>
    [TestFixture]
    public class ReferenceEthnicGroupDemographicsControllerTests : BaseIntegrationTestProvider
    {
        [Test]
        public async Task GetAll_WhenCalled_HttpOkAndReturnsMatchingCollection()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceEthnicGroupDemographicsController));

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            IEnumerable<ReferenceEthnicGroupDemographicDto> apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceEthnicGroupDemographicDto>>(responseString);
            IEnumerable<ReferenceEthnicGroupDemographicDto> preDefinedCollection =
                PreDefinedData.ReferenceEthnicGroupDemographics.Select(Mapper.Map<ReferenceEthnicGroupDemographic, ReferenceEthnicGroupDemographicDto>).ToList().AsEnumerable<ReferenceEthnicGroupDemographicDto>();
            ((List<ReferenceEthnicGroupDemographicDto>)apiReturnedCollection).Sort();
            ((List<ReferenceEthnicGroupDemographicDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidEthnicGroupDemographicCode_HttpOkAndReturnsSingleEthnicGroupDemographic()
        {
            // Arrange
            var randomEthnicGroupDemographicCode = PreDefinedData.GetRandomEthnicGroupDemographicCode();
            var path = GetRelativePath(nameof(ReferenceEthnicGroupDemographicsController), randomEthnicGroupDemographicCode);

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            ReferenceEthnicGroupDemographicDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceEthnicGroupDemographicDto>(responseString);

            ReferenceEthnicGroupDemographic preDefinedObject =
                PreDefinedData.ReferenceEthnicGroupDemographics
                    .SingleOrDefault(c => c.Code == randomEthnicGroupDemographicCode);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceEthnicGroupDemographic, ReferenceEthnicGroupDemographicDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidEthnicGroupDemographicCode_HttpNotFound()
        {
            // Arrange
            var notExistsEthnicGroupDemographicCode = PreDefinedData.GetNotExistsEthnicGroupDemographicCode();
            var path = GetRelativePath(nameof(ReferenceEthnicGroupDemographicsController), notExistsEthnicGroupDemographicCode);

            // Act
            var response = await Client.GetAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Create_ValidPayload_HttpCreatedAndReturnsNewEthnicGroupDemographic()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceEthnicGroupDemographicsController));
            var notExistsEthnicGroupDemographicCode = PreDefinedData.GetNotExistsEthnicGroupDemographicCode();
            var newEthnicGroupDemographicDto = new ReferenceEthnicGroupDemographicDto()
            {
                Code = notExistsEthnicGroupDemographicCode,
                DisplayText = "New EthnicGroupDemographic",
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newEthnicGroupDemographicDto),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            ReferenceEthnicGroupDemographicDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceEthnicGroupDemographicDto>(responseString);

            AssertHelper.AreObjectsEqual(apiReturnedObject, newEthnicGroupDemographicDto);
        }

        [Test]
        public async Task Create_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceEthnicGroupDemographicsController));

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
            var path = GetRelativePath(nameof(ReferenceEthnicGroupDemographicsController));
            var notExistsEthnicGroupDemographicCode = PreDefinedData.GetNotExistsEthnicGroupDemographicCode();
            var newEthnicGroupDemographicDto = new ReferenceEthnicGroupDemographicDto()
            {
                Code = notExistsEthnicGroupDemographicCode,
                // Display text is required, keep it missing
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newEthnicGroupDemographicDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Create_ExistingEthnicGroupDemographic_HttpConflict()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceEthnicGroupDemographicsController));
            var newEthnicGroupDemographicDto = new ReferenceEthnicGroupDemographicDto()
            {
                Code = PreDefinedData.GetRandomEthnicGroupDemographicCode(),
                DisplayText = "Create Test",
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newEthnicGroupDemographicDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task Update_ValidEthnicGroupDemographic_HttpNoContent()
        {
            // Arrange
            var randomEthnicGroupDemographicCode = PreDefinedData.GetRandomEthnicGroupDemographicCode();
            ReferenceEthnicGroupDemographic apiUpdatingEthnicGroupDemographic = UnitOfWork.ReferenceEthnicGroupDemographics.Get(randomEthnicGroupDemographicCode);
            apiUpdatingEthnicGroupDemographic.DisplayText = "Update Test";
            var path = GetRelativePath(nameof(ReferenceEthnicGroupDemographicsController), randomEthnicGroupDemographicCode);

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingEthnicGroupDemographic),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            ReferenceEthnicGroupDemographic dbUpdatedEthnicGroupDemographic = UnitOfWork.ReferenceEthnicGroupDemographics.Get(apiUpdatingEthnicGroupDemographic.Code);
            AssertHelper.AreObjectsEqual(apiUpdatingEthnicGroupDemographic, dbUpdatedEthnicGroupDemographic);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomEthnicGroupDemographicCode = PreDefinedData.GetRandomEthnicGroupDemographicCode();
            var path = GetRelativePath(nameof(ReferenceEthnicGroupDemographicsController), randomEthnicGroupDemographicCode);

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
            var randomEthnicGroupDemographicCode = PreDefinedData.GetRandomEthnicGroupDemographicCode();
            var path = GetRelativePath(nameof(ReferenceEthnicGroupDemographicsController), randomEthnicGroupDemographicCode);
            var apiUpdatingEthnicGroupDemographic = new ReferenceEthnicGroupDemographicDto()
            {
                Code = randomEthnicGroupDemographicCode,
                // Display text is required, keep it missing
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingEthnicGroupDemographic),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Update_InvalidEthnicGroupDemographic_HttpNotFound()
        {
            // Arrange
            var notExistsEthnicGroupDemographicCode = PreDefinedData.GetNotExistsEthnicGroupDemographicCode();
            var path = GetRelativePath(nameof(ReferenceEthnicGroupDemographicsController), notExistsEthnicGroupDemographicCode);
            var apiUpdatingEthnicGroupDemographic = new ReferenceEthnicGroupDemographicDto()
            {
                Code = notExistsEthnicGroupDemographicCode,
                DisplayText = "Update Test"
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingEthnicGroupDemographic),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Delete_ValidEthnicGroupDemographic_HttpNoContent()
        {
            // Arrange
            var randomEthnicGroupDemographicCode = PreDefinedData.GetRandomEthnicGroupDemographicCode();
            var path = GetRelativePath(nameof(ReferenceEthnicGroupDemographicsController), randomEthnicGroupDemographicCode);

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
        public async Task Delete_InvalidEthnicGroupDemographic_HttpNotFound()
        {
            // Arrange
            var notExistsEthnicGroupDemographicCode = PreDefinedData.GetNotExistsEthnicGroupDemographicCode();
            var path = GetRelativePath(nameof(ReferenceEthnicGroupDemographicsController), notExistsEthnicGroupDemographicCode);

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
