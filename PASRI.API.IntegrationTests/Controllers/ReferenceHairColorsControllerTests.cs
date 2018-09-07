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
    /// Integration test class for the <see cref="ReferenceHairColorsController"/> methods.
    /// Method names should reflect the following pattern:
    /// MethodBeingTested_Scenario_ExpectedBehavior
    /// </summary>
    [TestFixture]
    public class ReferenceHairColorsControllerTests : BaseIntegrationTestProvider
    {
        [Test]
        public async Task GetAll_WhenCalled_HttpOkAndReturnsMatchingCollection()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceHairColorsController));

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            IEnumerable<ReferenceHairColorDto> apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceHairColorDto>>(responseString);
            IEnumerable<ReferenceHairColorDto> preDefinedCollection =
                PreDefinedData.ReferenceHairColors.Select(Mapper.Map<ReferenceHairColor, ReferenceHairColorDto>).ToList().AsEnumerable<ReferenceHairColorDto>();
            ((List<ReferenceHairColorDto>)apiReturnedCollection).Sort();
            ((List<ReferenceHairColorDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidHairColorCode_HttpOkAndReturnsSingleHairColor()
        {
            // Arrange
            var randomHairColorCode = PreDefinedData.GetRandomHairColorCode();
            var path = GetRelativePath(nameof(ReferenceHairColorsController), randomHairColorCode);

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            ReferenceHairColorDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceHairColorDto>(responseString);

            ReferenceHairColor preDefinedObject =
                PreDefinedData.ReferenceHairColors
                    .SingleOrDefault(c => c.Code == randomHairColorCode);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceHairColor, ReferenceHairColorDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidHairColorCode_HttpNotFound()
        {
            // Arrange
            var notExistsHairColorCode = PreDefinedData.GetNotExistsHairColorCode();
            var path = GetRelativePath(nameof(ReferenceHairColorsController), notExistsHairColorCode);

            // Act
            var response = await Client.GetAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Create_ValidPayload_HttpCreatedAndReturnsNewHairColor()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceHairColorsController));
            var notExistsHairColorCode = PreDefinedData.GetNotExistsHairColorCode();
            var newHairColorDto = new ReferenceHairColorDto()
            {
                Code = notExistsHairColorCode,
                DisplayText = "New HairColor",
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newHairColorDto),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            ReferenceHairColorDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceHairColorDto>(responseString);

            AssertHelper.AreObjectsEqual(apiReturnedObject, newHairColorDto);
        }

        [Test]
        public async Task Create_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceHairColorsController));

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
            var path = GetRelativePath(nameof(ReferenceHairColorsController));
            var notExistsHairColorCode = PreDefinedData.GetNotExistsHairColorCode();
            var newHairColorDto = new ReferenceHairColorDto()
            {
                Code = notExistsHairColorCode,
                // Display text is required, keep it missing
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newHairColorDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Create_ExistingHairColor_HttpConflict()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceHairColorsController));
            var newHairColorDto = new ReferenceHairColorDto()
            {
                Code = PreDefinedData.GetRandomHairColorCode(),
                DisplayText = "Create Test",
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newHairColorDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task Update_ValidHairColor_HttpNoContent()
        {
            // Arrange
            var randomHairColorCode = PreDefinedData.GetRandomHairColorCode();
            ReferenceHairColor apiUpdatingHairColor = UnitOfWork.ReferenceHairColors.Get(randomHairColorCode);
            apiUpdatingHairColor.DisplayText = "Update Test";
            var path = GetRelativePath(nameof(ReferenceHairColorsController), randomHairColorCode);

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingHairColor),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            ReferenceHairColor dbUpdatedHairColor = UnitOfWork.ReferenceHairColors.Get(apiUpdatingHairColor.Code);
            AssertHelper.AreObjectsEqual(apiUpdatingHairColor, dbUpdatedHairColor);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomHairColorCode = PreDefinedData.GetRandomHairColorCode();
            var path = GetRelativePath(nameof(ReferenceHairColorsController), randomHairColorCode);

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
            var randomHairColorCode = PreDefinedData.GetRandomHairColorCode();
            var path = GetRelativePath(nameof(ReferenceHairColorsController), randomHairColorCode);
            var apiUpdatingHairColor = new ReferenceHairColorDto()
            {
                Code = randomHairColorCode,
                // Display text is required, keep it missing
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingHairColor),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Update_InvalidHairColor_HttpNotFound()
        {
            // Arrange
            var notExistsHairColorCode = PreDefinedData.GetNotExistsHairColorCode();
            var path = GetRelativePath(nameof(ReferenceHairColorsController), notExistsHairColorCode);
            var apiUpdatingHairColor = new ReferenceHairColorDto()
            {
                Code = notExistsHairColorCode,
                DisplayText = "Update Test"
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingHairColor),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Delete_ValidHairColor_HttpNoContent()
        {
            // Arrange
            var randomHairColorCode = PreDefinedData.GetRandomHairColorCode();
            var path = GetRelativePath(nameof(ReferenceHairColorsController), randomHairColorCode);

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
        public async Task Delete_InvalidHairColor_HttpNotFound()
        {
            // Arrange
            var notExistsHairColorCode = PreDefinedData.GetNotExistsHairColorCode();
            var path = GetRelativePath(nameof(ReferenceHairColorsController), notExistsHairColorCode);

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
