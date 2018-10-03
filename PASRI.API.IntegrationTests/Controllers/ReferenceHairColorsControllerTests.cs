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
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceHairColorDto>>(responseString);
            var preDefinedCollection =
                PreDefinedData.ReferenceHairColors.Select(Mapper.Map<ReferenceHairColor, ReferenceHairColorDto>).ToList().AsEnumerable();
            ((List<ReferenceHairColorDto>)apiReturnedCollection).Sort();
            ((List<ReferenceHairColorDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidHairColorCode_HttpOkAndReturnsSingleHairColor()
        {
            // Arrange
            var randomHairColorId = PreDefinedData.GetRandomHairColorId();
            var path = GetRelativePath(nameof(ReferenceHairColorsController), randomHairColorId.ToString());

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceHairColorDto>(responseString);

            var preDefinedObject =
                PreDefinedData.ReferenceHairColors
                    .SingleOrDefault(c => c.Id == randomHairColorId);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceHairColor, ReferenceHairColorDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidHairColorId_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceHairColorsController), int.MaxValue.ToString());

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
            var newHairColorDto = new ReferenceHairColorDto
            {
                Code = notExistsHairColorCode,
                LongName = "New",
                CreatedDate = DateTime.UtcNow
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
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceHairColorDto>(responseString);

            Assert.That(apiReturnedObject.Id, Is.GreaterThan(0));

            newHairColorDto.Id = apiReturnedObject.Id;
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

            var newHairColorDto = new ReferenceHairColorDto
            {
                // Code is required, keep it missing
                CreatedDate = DateTime.UtcNow
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
            var randomHairColorId = PreDefinedData.GetRandomHairColorId();
            var randomHairColor = PreDefinedData.ReferenceHairColors[randomHairColorId - 1];

            var newHairColorDto = new ReferenceHairColorDto
            {
                Code = randomHairColor.Code,
                LongName = "Create",
                CreatedDate = DateTime.UtcNow
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
            var randomHairColorId = PreDefinedData.GetRandomHairColorId();

            var apiUpdatingHairColor = UnitOfWork.ReferenceHairColors.Get(randomHairColorId);
            apiUpdatingHairColor.LongName = "Update";
            var path = GetRelativePath(nameof(ReferenceHairColorsController), randomHairColorId.ToString());

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingHairColor),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            var dbUpdatedHairColor = UnitOfWork.ReferenceHairColors.Get(apiUpdatingHairColor.Id);
            AssertHelper.AreObjectsEqual(apiUpdatingHairColor, dbUpdatedHairColor);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomHairColorId = PreDefinedData.GetRandomHairColorId();
            var path = GetRelativePath(nameof(ReferenceHairColorsController), randomHairColorId.ToString());

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
            var randomHairColorId = PreDefinedData.GetRandomHairColorId();
            var path = GetRelativePath(nameof(ReferenceHairColorsController), randomHairColorId.ToString());
            var apiUpdatingHairColor = new ReferenceHairColorDto
            {
                Id = randomHairColorId
                // Code is required, keep it missing
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
            var path = GetRelativePath(nameof(ReferenceHairColorsController), int.MaxValue.ToString());
            var apiUpdatingHairColor = new ReferenceHairColorDto
            {
                Id = int.MaxValue,
                Code = notExistsHairColorCode,
                LongName = "Update"
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
            var randomHairColorId = PreDefinedData.GetRandomHairColorId();
            var path = GetRelativePath(nameof(ReferenceHairColorsController), randomHairColorId.ToString());

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
        public async Task Delete_InvalidHairColor_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceHairColorsController), int.MaxValue.ToString());

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
