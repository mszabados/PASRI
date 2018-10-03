using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using PASRI.API.Controllers;
using PASRI.API.Core.Domain;
using PASRI.API.Dtos;
using PASRI.API.TestHelper;

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
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceReligionDemographicDto>>(responseString);
            var preDefinedCollection =
                PreDefinedData.ReferenceReligionDemographics.Select(Mapper.Map<ReferenceReligionDemographic, ReferenceReligionDemographicDto>).ToList().AsEnumerable();
            ((List<ReferenceReligionDemographicDto>)apiReturnedCollection).Sort();
            ((List<ReferenceReligionDemographicDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidReligionDemographicCode_HttpOkAndReturnsSingleReligionDemographic()
        {
            // Arrange
            var randomReligionDemographicId = PreDefinedData.GetRandomReligionDemographicId();
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController), randomReligionDemographicId.ToString());

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceReligionDemographicDto>(responseString);

            var preDefinedObject =
                PreDefinedData.ReferenceReligionDemographics
                    .SingleOrDefault(c => c.Id == randomReligionDemographicId);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceReligionDemographic, ReferenceReligionDemographicDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidReligionDemographicId_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController), int.MaxValue.ToString());

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
            var newReligionDemographicDto = new ReferenceReligionDemographicDto
            {
                Code = notExistsReligionDemographicCode,
                LongName = "New ReligionDemographic",
                CreatedDate = DateTime.UtcNow
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
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceReligionDemographicDto>(responseString);

            Assert.That(apiReturnedObject.Id, Is.GreaterThan(0));

            newReligionDemographicDto.Id = apiReturnedObject.Id;
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

            var newReligionDemographicDto = new ReferenceReligionDemographicDto
            {
                // Code is required, keep it missing
                CreatedDate = DateTime.UtcNow
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
            var randomReligionDemographicId = PreDefinedData.GetRandomReligionDemographicId();
            var randomReligionDemographic = PreDefinedData.ReferenceReligionDemographics[randomReligionDemographicId - 1];

            var newReligionDemographicDto = new ReferenceReligionDemographicDto
            {
                Code = randomReligionDemographic.Code,
                LongName = "Create Test",
                CreatedDate = DateTime.UtcNow
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
            var randomReligionDemographicId = PreDefinedData.GetRandomReligionDemographicId();

            var apiUpdatingReligionDemographic = UnitOfWork.ReferenceReligionDemographics.Get(randomReligionDemographicId);
            apiUpdatingReligionDemographic.LongName = "Update Test";
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController), randomReligionDemographicId.ToString());

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingReligionDemographic),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            var dbUpdatedReligionDemographic = UnitOfWork.ReferenceReligionDemographics.Get(apiUpdatingReligionDemographic.Id);
            AssertHelper.AreObjectsEqual(apiUpdatingReligionDemographic, dbUpdatedReligionDemographic);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomReligionDemographicId = PreDefinedData.GetRandomReligionDemographicId();
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController), randomReligionDemographicId.ToString());

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
            var randomReligionDemographicId = PreDefinedData.GetRandomReligionDemographicId();
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController), randomReligionDemographicId.ToString());
            var apiUpdatingReligionDemographic = new ReferenceReligionDemographicDto
            {
                Id = randomReligionDemographicId
                // Code is required, keep it missing
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
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController), int.MaxValue.ToString());
            var apiUpdatingReligionDemographic = new ReferenceReligionDemographicDto
            {
                Id = int.MaxValue,
                Code = notExistsReligionDemographicCode,
                LongName = "Update Test"
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
            var randomReligionDemographicId = PreDefinedData.GetRandomReligionDemographicId();
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController), randomReligionDemographicId.ToString());

            // Act
            var response = await Client.DeleteAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public async Task Delete_InvalidReligionDemographic_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceReligionDemographicsController), int.MaxValue.ToString());

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
