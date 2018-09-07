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
    /// Integration test class for the <see cref="ReferenceStatesController"/> methods.
    /// Method names should reflect the following pattern:
    /// MethodBeingTested_Scenario_ExpectedBehavior
    /// </summary>
    [TestFixture]
    public class ReferenceStatesControllerTests : BaseIntegrationTestProvider
    {
        [Test]
        public async Task GetAll_WhenCalled_HttpOkAndReturnsMatchingCollection()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceStatesController));

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            IEnumerable<ReferenceStateDto> apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceStateDto>>(responseString);
            IEnumerable<ReferenceStateDto> preDefinedCollection =
                PreDefinedData.ReferenceStates.Select(Mapper.Map<ReferenceState, ReferenceStateDto>).ToList().AsEnumerable<ReferenceStateDto>();
            ((List<ReferenceStateDto>)apiReturnedCollection).Sort();
            ((List<ReferenceStateDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidStateCode_HttpOkAndReturnsSingleState()
        {
            // Arrange
            var randomStateCode = PreDefinedData.GetRandomStateCode();
            var path = GetRelativePath(nameof(ReferenceStatesController), randomStateCode);

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            ReferenceStateDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceStateDto>(responseString);

            ReferenceState preDefinedObject =
                PreDefinedData.ReferenceStates
                    .SingleOrDefault(c => c.Code == randomStateCode);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceState, ReferenceStateDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidStateCode_HttpNotFound()
        {
            // Arrange
            var notExistsStateCode = PreDefinedData.GetNotExistsStateCode();
            var path = GetRelativePath(nameof(ReferenceStatesController), notExistsStateCode);

            // Act
            var response = await Client.GetAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Create_ValidPayload_HttpCreatedAndReturnsNewState()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceStatesController));
            var notExistsStateCode = PreDefinedData.GetNotExistsStateCode();
            var newStateDto = new ReferenceStateDto()
            {
                Code = notExistsStateCode,
                DisplayText = "New State",
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newStateDto),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            ReferenceStateDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceStateDto>(responseString);

            AssertHelper.AreObjectsEqual(apiReturnedObject, newStateDto);
        }

        [Test]
        public async Task Create_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceStatesController));

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
            var path = GetRelativePath(nameof(ReferenceStatesController));
            var notExistsStateCode = PreDefinedData.GetNotExistsStateCode();
            var newStateDto = new ReferenceStateDto()
            {
                Code = notExistsStateCode,
                // Display text is required, keep it missing
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newStateDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Create_ExistingState_HttpConflict()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceStatesController));
            var newStateDto = new ReferenceStateDto()
            {
                Code = PreDefinedData.GetRandomStateCode(),
                DisplayText = "Create Test",
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newStateDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task Update_ValidState_HttpNoContent()
        {
            // Arrange
            var randomStateCode = PreDefinedData.GetRandomStateCode();
            ReferenceState apiUpdatingState = UnitOfWork.ReferenceStates.Get(randomStateCode);
            apiUpdatingState.DisplayText = "Update Test";
            var path = GetRelativePath(nameof(ReferenceStatesController), randomStateCode);

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingState),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            ReferenceState dbUpdatedState = UnitOfWork.ReferenceStates.Get(apiUpdatingState.Code);
            AssertHelper.AreObjectsEqual(apiUpdatingState, dbUpdatedState);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomStateCode = PreDefinedData.GetRandomStateCode();
            var path = GetRelativePath(nameof(ReferenceStatesController), randomStateCode);

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
            var randomStateCode = PreDefinedData.GetRandomStateCode();
            var path = GetRelativePath(nameof(ReferenceStatesController), randomStateCode);
            var apiUpdatingState = new ReferenceStateDto()
            {
                Code = randomStateCode,
                // Display text is required, keep it missing
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingState),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Update_InvalidState_HttpNotFound()
        {
            // Arrange
            var notExistsStateCode = PreDefinedData.GetNotExistsStateCode();
            var path = GetRelativePath(nameof(ReferenceStatesController), notExistsStateCode);
            var apiUpdatingState = new ReferenceStateDto()
            {
                Code = notExistsStateCode,
                DisplayText = "Update Test"
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingState),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Delete_ValidState_HttpNoContent()
        {
            // Arrange
            var randomStateCode = PreDefinedData.GetRandomStateCode();
            var path = GetRelativePath(nameof(ReferenceStatesController), randomStateCode);

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
        public async Task Delete_InvalidState_HttpNotFound()
        {
            // Arrange
            var notExistsStateCode = PreDefinedData.GetNotExistsStateCode();
            var path = GetRelativePath(nameof(ReferenceStatesController), notExistsStateCode);

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
