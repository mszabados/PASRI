using HRC.API.PASRI.Controllers;
using HRC.API.PASRI.Dtos;
using HRC.DB.Master.Core.Domain;
using HRC.DB.Master.Test;
using HRC.Helper.Test;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HRC.API.PASRI.IntegrationTests.Controllers
{
    /// <summary>
    /// Integration test class for the <see cref="ReferenceRacesController"/> methods.
    /// Method names should reflect the following pattern:
    /// MethodBeingTested_Scenario_ExpectedBehavior
    /// </summary>
    [TestFixture]
    public class ReferenceRacesControllerTests : BaseIntegrationTestProvider
    {
        [Test]
        public async Task GetAll_WhenCalled_HttpOkAndReturnsMatchingCollection()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceRacesController));

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceRaceDto>>(responseString);
            var preDefinedCollection =
                PreDefinedData.ReferenceRaces.Select(Mapper.Map<ReferenceRace, ReferenceRaceDto>).ToList().AsEnumerable();
            ((List<ReferenceRaceDto>)apiReturnedCollection).Sort();
            ((List<ReferenceRaceDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidRaceCode_HttpOkAndReturnsSingleRace()
        {
            // Arrange
            var randomRaceId = PreDefinedData.GetRandomRaceId();
            var path = GetRelativePath(nameof(ReferenceRacesController), randomRaceId.ToString());

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceRaceDto>(responseString);

            var preDefinedObject =
                PreDefinedData.ReferenceRaces
                    .SingleOrDefault(c => c.Id == randomRaceId);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceRace, ReferenceRaceDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidRaceId_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceRacesController), int.MaxValue.ToString());

            // Act
            var response = await Client.GetAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Create_ValidPayload_HttpCreatedAndReturnsNewRace()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceRacesController));
            var notExistsRaceCode = PreDefinedData.GetNotExistsRaceCode();
            var newRaceDto = new ReferenceRaceDto
            {
                Code = notExistsRaceCode,
                LongName = "New Race",
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newRaceDto),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceRaceDto>(responseString);

            Assert.That(apiReturnedObject.Id, Is.GreaterThan(0));

            newRaceDto.Id = apiReturnedObject.Id;
            AssertHelper.AreObjectsEqual(apiReturnedObject, newRaceDto);
        }

        [Test]
        public async Task Create_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceRacesController));

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
            var path = GetRelativePath(nameof(ReferenceRacesController));

            var newRaceDto = new ReferenceRaceDto
            {
                // Code is required, keep it missing
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newRaceDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Create_ExistingRace_HttpConflict()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceRacesController));
            var randomRaceId = PreDefinedData.GetRandomRaceId();
            var randomRace = PreDefinedData.ReferenceRaces[randomRaceId - 1];

            var newRaceDto = new ReferenceRaceDto
            {
                Code = randomRace.Code,
                LongName = "Create Test",
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newRaceDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task Update_ValidRace_HttpNoContent()
        {
            // Arrange
            var randomRaceId = PreDefinedData.GetRandomRaceId();

            var apiUpdatingRace = UnitOfWork.ReferenceRaces.Get(randomRaceId);
            apiUpdatingRace.LongName = "Update Test";
            var path = GetRelativePath(nameof(ReferenceRacesController), randomRaceId.ToString());

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingRace),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            var dbUpdatedRace = UnitOfWork.ReferenceRaces.Get(apiUpdatingRace.Id);
            AssertHelper.AreObjectsEqual(apiUpdatingRace, dbUpdatedRace);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomRaceId = PreDefinedData.GetRandomRaceId();
            var path = GetRelativePath(nameof(ReferenceRacesController), randomRaceId.ToString());

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
            var randomRaceId = PreDefinedData.GetRandomRaceId();
            var path = GetRelativePath(nameof(ReferenceRacesController), randomRaceId.ToString());
            var apiUpdatingRace = new ReferenceRaceDto
            {
                Id = randomRaceId
                // Code is required, keep it missing
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingRace),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Update_InvalidRace_HttpNotFound()
        {
            // Arrange
            var notExistsRaceCode = PreDefinedData.GetNotExistsRaceCode();
            var path = GetRelativePath(nameof(ReferenceRacesController), int.MaxValue.ToString());
            var apiUpdatingRace = new ReferenceRaceDto
            {
                Id = int.MaxValue,
                Code = notExistsRaceCode,
                LongName = "Update Test"
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingRace),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Delete_ValidRace_HttpNoContent()
        {
            // Arrange
            var randomRaceId = PreDefinedData.GetRandomRaceId();
            var path = GetRelativePath(nameof(ReferenceRacesController), randomRaceId.ToString());

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
        public async Task Delete_InvalidRace_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceRacesController), int.MaxValue.ToString());

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
