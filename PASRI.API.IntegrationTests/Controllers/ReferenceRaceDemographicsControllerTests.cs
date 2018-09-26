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
    /// Integration test class for the <see cref="ReferenceRaceDemographicsController"/> methods.
    /// Method names should reflect the following pattern:
    /// MethodBeingTested_Scenario_ExpectedBehavior
    /// </summary>
    [TestFixture]
    public class ReferenceRaceDemographicsControllerTests : BaseIntegrationTestProvider
    {
        [Test]
        public async Task GetAll_WhenCalled_HttpOkAndReturnsMatchingCollection()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController));

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            IEnumerable<ReferenceRaceDemographicDto> apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceRaceDemographicDto>>(responseString);
            IEnumerable<ReferenceRaceDemographicDto> preDefinedCollection =
                PreDefinedData.ReferenceRaceDemographics.Select(Mapper.Map<ReferenceRaceDemographic, ReferenceRaceDemographicDto>).ToList().AsEnumerable<ReferenceRaceDemographicDto>();
            ((List<ReferenceRaceDemographicDto>)apiReturnedCollection).Sort();
            ((List<ReferenceRaceDemographicDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidRaceDemographicCode_HttpOkAndReturnsSingleRaceDemographic()
        {
            // Arrange
            var randomRaceDemographicId = PreDefinedData.GetRandomRaceDemographicId();
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController), randomRaceDemographicId.ToString());

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            ReferenceRaceDemographicDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceRaceDemographicDto>(responseString);

            ReferenceRaceDemographic preDefinedObject =
                PreDefinedData.ReferenceRaceDemographics
                    .SingleOrDefault(c => c.Id == randomRaceDemographicId);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceRaceDemographic, ReferenceRaceDemographicDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidRaceDemographicId_HttpNotFound()
        {
            // Arrange
            var notExistsRaceDemographicCode = PreDefinedData.GetNotExistsRaceDemographicCode();
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController), Int32.MaxValue.ToString());

            // Act
            var response = await Client.GetAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Create_ValidPayload_HttpCreatedAndReturnsNewRaceDemographic()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController));
            var notExistsRaceDemographicCode = PreDefinedData.GetNotExistsRaceDemographicCode();
            var newRaceDemographicDto = new ReferenceRaceDemographicDto()
            {
                Code = notExistsRaceDemographicCode,
                LongName = "New RaceDemographic",
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newRaceDemographicDto),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            ReferenceRaceDemographicDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceRaceDemographicDto>(responseString);

            Assert.That(apiReturnedObject.Id, Is.GreaterThan(0));

            newRaceDemographicDto.Id = apiReturnedObject.Id;
            AssertHelper.AreObjectsEqual(apiReturnedObject, newRaceDemographicDto);
        }

        [Test]
        public async Task Create_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController));

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
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController));

            var newRaceDemographicDto = new ReferenceRaceDemographicDto()
            {
                // Code is required, keep it missing
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newRaceDemographicDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Create_ExistingRaceDemographic_HttpConflict()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController));
            var randomRaceDemographicId = PreDefinedData.GetRandomRaceDemographicId();
            var randomRaceDemographic = PreDefinedData.ReferenceRaceDemographics[randomRaceDemographicId - 1];

            var newRaceDemographicDto = new ReferenceRaceDemographicDto()
            {
                Code = randomRaceDemographic.Code,
                LongName = "Create Test",
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newRaceDemographicDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task Update_ValidRaceDemographic_HttpNoContent()
        {
            // Arrange
            var randomRaceDemographicId = PreDefinedData.GetRandomRaceDemographicId();

            ReferenceRaceDemographic apiUpdatingRaceDemographic = UnitOfWork.ReferenceRaceDemographics.Get(randomRaceDemographicId);
            apiUpdatingRaceDemographic.LongName = "Update Test";
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController), randomRaceDemographicId.ToString());

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingRaceDemographic),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            ReferenceRaceDemographic dbUpdatedRaceDemographic = UnitOfWork.ReferenceRaceDemographics.Get(apiUpdatingRaceDemographic.Id);
            AssertHelper.AreObjectsEqual(apiUpdatingRaceDemographic, dbUpdatedRaceDemographic);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomRaceDemographicId = PreDefinedData.GetRandomRaceDemographicId();
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController), randomRaceDemographicId.ToString());

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
            var randomRaceDemographicId = PreDefinedData.GetRandomRaceDemographicId();
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController), randomRaceDemographicId.ToString());
            var apiUpdatingRaceDemographic = new ReferenceRaceDemographicDto()
            {
                Id = randomRaceDemographicId
                // Code is required, keep it missing
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingRaceDemographic),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Update_InvalidRaceDemographic_HttpNotFound()
        {
            // Arrange
            var notExistsRaceDemographicCode = PreDefinedData.GetNotExistsRaceDemographicCode();
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController), Int32.MaxValue.ToString());
            var apiUpdatingRaceDemographic = new ReferenceRaceDemographicDto()
            {
                Id = Int32.MaxValue,
                Code = notExistsRaceDemographicCode,
                LongName = "Update Test"
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingRaceDemographic),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Delete_ValidRaceDemographic_HttpNoContent()
        {
            // Arrange
            var randomRaceDemographicId = PreDefinedData.GetRandomRaceDemographicId();
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController), randomRaceDemographicId.ToString());

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
        public async Task Delete_InvalidRaceDemographic_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController), Int32.MaxValue.ToString());

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
