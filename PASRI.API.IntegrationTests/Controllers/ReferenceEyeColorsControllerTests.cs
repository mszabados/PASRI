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
    /// Integration test class for the <see cref="ReferenceEyeColorsController"/> methods.
    /// Method names should reflect the following pattern:
    /// MethodBeingTested_Scenario_ExpectedBehavior
    /// </summary>
    [TestFixture]
    public class ReferenceEyeColorsControllerTests : BaseIntegrationTestProvider
    {
        [Test]
        public async Task GetAll_WhenCalled_HttpOkAndReturnsMatchingCollection()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceEyeColorsController));

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            IEnumerable<ReferenceEyeColorDto> apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceEyeColorDto>>(responseString);
            IEnumerable<ReferenceEyeColorDto> preDefinedCollection =
                PreDefinedData.ReferenceEyeColors.Select(Mapper.Map<ReferenceEyeColor, ReferenceEyeColorDto>).ToList().AsEnumerable<ReferenceEyeColorDto>();
            ((List<ReferenceEyeColorDto>)apiReturnedCollection).Sort();
            ((List<ReferenceEyeColorDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidEyeColorCode_HttpOkAndReturnsSingleEyeColor()
        {
            // Arrange
            var randomEyeColorId = PreDefinedData.GetRandomEyeColorId();
            var path = GetRelativePath(nameof(ReferenceEyeColorsController), randomEyeColorId.ToString());

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            ReferenceEyeColorDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceEyeColorDto>(responseString);

            ReferenceEyeColor preDefinedObject =
                PreDefinedData.ReferenceEyeColors
                    .SingleOrDefault(c => c.Id == randomEyeColorId);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceEyeColor, ReferenceEyeColorDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidEyeColorId_HttpNotFound()
        {
            // Arrange
            var notExistsEyeColorCode = PreDefinedData.GetNotExistsEyeColorCode();
            var path = GetRelativePath(nameof(ReferenceEyeColorsController), Int32.MaxValue.ToString());

            // Act
            var response = await Client.GetAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Create_ValidPayload_HttpCreatedAndReturnsNewEyeColor()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceEyeColorsController));
            var notExistsEyeColorCode = PreDefinedData.GetNotExistsEyeColorCode();
            var newEyeColorDto = new ReferenceEyeColorDto()
            {
                Code = notExistsEyeColorCode,
                Description = "New EyeColor",
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newEyeColorDto),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            ReferenceEyeColorDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceEyeColorDto>(responseString);

            Assert.That(apiReturnedObject.Id, Is.GreaterThan(0));

            newEyeColorDto.Id = apiReturnedObject.Id;
            AssertHelper.AreObjectsEqual(apiReturnedObject, newEyeColorDto);
        }

        [Test]
        public async Task Create_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceEyeColorsController));

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
            var path = GetRelativePath(nameof(ReferenceEyeColorsController));

            var newEyeColorDto = new ReferenceEyeColorDto()
            {
                // Code is required, keep it missing
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newEyeColorDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Create_ExistingEyeColor_HttpConflict()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceEyeColorsController));
            var randomEyeColorId = PreDefinedData.GetRandomEyeColorId();
            var randomEyeColor = PreDefinedData.ReferenceEyeColors[randomEyeColorId - 1];

            var newEyeColorDto = new ReferenceEyeColorDto()
            {
                Code = randomEyeColor.Code,
                Description = "Create Test",
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newEyeColorDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task Update_ValidEyeColor_HttpNoContent()
        {
            // Arrange
            var randomEyeColorId = PreDefinedData.GetRandomEyeColorId();

            ReferenceEyeColor apiUpdatingEyeColor = UnitOfWork.ReferenceEyeColors.Get(randomEyeColorId);
            apiUpdatingEyeColor.Description = "Update Test";
            var path = GetRelativePath(nameof(ReferenceEyeColorsController), randomEyeColorId.ToString());

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingEyeColor),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            ReferenceEyeColor dbUpdatedEyeColor = UnitOfWork.ReferenceEyeColors.Get(apiUpdatingEyeColor.Id);
            AssertHelper.AreObjectsEqual(apiUpdatingEyeColor, dbUpdatedEyeColor);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomEyeColorId = PreDefinedData.GetRandomEyeColorId();
            var path = GetRelativePath(nameof(ReferenceEyeColorsController), randomEyeColorId.ToString());

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
            var randomEyeColorId = PreDefinedData.GetRandomEyeColorId();
            var path = GetRelativePath(nameof(ReferenceEyeColorsController), randomEyeColorId.ToString());
            var apiUpdatingEyeColor = new ReferenceEyeColorDto()
            {
                Id = randomEyeColorId
                // Code is required, keep it missing
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingEyeColor),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Update_InvalidEyeColor_HttpNotFound()
        {
            // Arrange
            var notExistsEyeColorCode = PreDefinedData.GetNotExistsEyeColorCode();
            var path = GetRelativePath(nameof(ReferenceEyeColorsController), Int32.MaxValue.ToString());
            var apiUpdatingEyeColor = new ReferenceEyeColorDto()
            {
                Id = Int32.MaxValue,
                Code = notExistsEyeColorCode,
                Description = "Update Test"
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingEyeColor),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Delete_ValidEyeColor_HttpNoContent()
        {
            // Arrange
            var randomEyeColorId = PreDefinedData.GetRandomEyeColorId();
            var path = GetRelativePath(nameof(ReferenceEyeColorsController), randomEyeColorId.ToString());

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
        public async Task Delete_InvalidEyeColor_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceEyeColorsController), Int32.MaxValue.ToString());

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
