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
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceGenderDemographicDto>>(responseString);
            var preDefinedCollection =
                PreDefinedData.ReferenceGenderDemographics.Select(Mapper.Map<ReferenceGenderDemographic, ReferenceGenderDemographicDto>).ToList().AsEnumerable();
            ((List<ReferenceGenderDemographicDto>)apiReturnedCollection).Sort();
            ((List<ReferenceGenderDemographicDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidGenderDemographicCode_HttpOkAndReturnsSingleGenderDemographic()
        {
            // Arrange
            var randomGenderDemographicId = PreDefinedData.GetRandomGenderDemographicId();
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController), randomGenderDemographicId.ToString());

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceGenderDemographicDto>(responseString);

            var preDefinedObject =
                PreDefinedData.ReferenceGenderDemographics
                    .SingleOrDefault(c => c.Id == randomGenderDemographicId);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceGenderDemographic, ReferenceGenderDemographicDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidGenderDemographicId_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController), int.MaxValue.ToString());

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
            var newGenderDemographicDto = new ReferenceGenderDemographicDto
            {
                Code = notExistsGenderDemographicCode,
                LongName = "New",
                CreatedDate = DateTime.UtcNow
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
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceGenderDemographicDto>(responseString);

            Assert.That(apiReturnedObject.Id, Is.GreaterThan(0));

            newGenderDemographicDto.Id = apiReturnedObject.Id;
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

            var newGenderDemographicDto = new ReferenceGenderDemographicDto
            {
                // Code is required, keep it missing
                CreatedDate = DateTime.UtcNow
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
            var randomGenderDemographicId = PreDefinedData.GetRandomGenderDemographicId();
            var randomGenderDemographic = PreDefinedData.ReferenceGenderDemographics[randomGenderDemographicId - 1];

            var newGenderDemographicDto = new ReferenceGenderDemographicDto
            {
                Code = randomGenderDemographic.Code,
                LongName = "Create",
                CreatedDate = DateTime.UtcNow
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
            var randomGenderDemographicId = PreDefinedData.GetRandomGenderDemographicId();

            var apiUpdatingGenderDemographic = UnitOfWork.ReferenceGenderDemographics.Get(randomGenderDemographicId);
            apiUpdatingGenderDemographic.LongName = "Update";
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController), randomGenderDemographicId.ToString());

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingGenderDemographic),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            var dbUpdatedGenderDemographic = UnitOfWork.ReferenceGenderDemographics.Get(apiUpdatingGenderDemographic.Id);
            AssertHelper.AreObjectsEqual(apiUpdatingGenderDemographic, dbUpdatedGenderDemographic);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomGenderDemographicId = PreDefinedData.GetRandomGenderDemographicId();
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController), randomGenderDemographicId.ToString());

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
            var randomGenderDemographicId = PreDefinedData.GetRandomGenderDemographicId();
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController), randomGenderDemographicId.ToString());
            var apiUpdatingGenderDemographic = new ReferenceGenderDemographicDto
            {
                Id = randomGenderDemographicId
                // Code is required, keep it missing
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
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController), int.MaxValue.ToString());
            var apiUpdatingGenderDemographic = new ReferenceGenderDemographicDto
            {
                Id = int.MaxValue,
                Code = notExistsGenderDemographicCode,
                LongName = "Update"
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
            var randomGenderDemographicId = PreDefinedData.GetRandomGenderDemographicId();
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController), randomGenderDemographicId.ToString());

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
        public async Task Delete_InvalidGenderDemographic_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceGenderDemographicsController), int.MaxValue.ToString());

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
