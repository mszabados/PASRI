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
    /// Integration test class for the <see cref="ReferenceSuffixNamesController"/> methods.
    /// Method names should reflect the following pattern:
    /// MethodBeingTested_Scenario_ExpectedBehavior
    /// </summary>
    [TestFixture]
    public class ReferenceSuffixNamesControllerTests : BaseIntegrationTestProvider
    {
        [Test]
        public async Task GetAll_WhenCalled_HttpOkAndReturnsMatchingCollection()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceSuffixNamesController));

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            IEnumerable<ReferenceSuffixNameDto> apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceSuffixNameDto>>(responseString);
            IEnumerable<ReferenceSuffixNameDto> preDefinedCollection =
                PreDefinedData.ReferenceSuffixNames.Select(Mapper.Map<ReferenceSuffixName, ReferenceSuffixNameDto>).ToList().AsEnumerable<ReferenceSuffixNameDto>();
            ((List<ReferenceSuffixNameDto>)apiReturnedCollection).Sort();
            ((List<ReferenceSuffixNameDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidSuffixNameCode_HttpOkAndReturnsSingleSuffixName()
        {
            // Arrange
            var randomSuffixNameCode = PreDefinedData.GetRandomSuffixNameCode();
            var path = GetRelativePath(nameof(ReferenceSuffixNamesController), randomSuffixNameCode);

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            ReferenceSuffixNameDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceSuffixNameDto>(responseString);

            ReferenceSuffixName preDefinedObject =
                PreDefinedData.ReferenceSuffixNames
                    .SingleOrDefault(c => c.Code == randomSuffixNameCode);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceSuffixName, ReferenceSuffixNameDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidSuffixNameCode_HttpNotFound()
        {
            // Arrange
            var notExistsSuffixNameCode = PreDefinedData.GetNotExistsSuffixNameCode();
            var path = GetRelativePath(nameof(ReferenceSuffixNamesController), notExistsSuffixNameCode);

            // Act
            var response = await Client.GetAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Create_ValidPayload_HttpCreatedAndReturnsNewSuffixName()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceSuffixNamesController));
            var notExistsSuffixNameCode = PreDefinedData.GetNotExistsSuffixNameCode();
            var newSuffixNameDto = new ReferenceSuffixNameDto()
            {
                Code = notExistsSuffixNameCode,
                DisplayText = "New SuffixName",
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newSuffixNameDto),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            ReferenceSuffixNameDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceSuffixNameDto>(responseString);

            AssertHelper.AreObjectsEqual(apiReturnedObject, newSuffixNameDto);
        }

        [Test]
        public async Task Create_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceSuffixNamesController));

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
            var path = GetRelativePath(nameof(ReferenceSuffixNamesController));
            var notExistsSuffixNameCode = PreDefinedData.GetNotExistsSuffixNameCode();
            var newSuffixNameDto = new ReferenceSuffixNameDto()
            {
                Code = notExistsSuffixNameCode,
                // Display text is required, keep it missing
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newSuffixNameDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Create_ExistingSuffixName_HttpConflict()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceSuffixNamesController));
            var newSuffixNameDto = new ReferenceSuffixNameDto()
            {
                Code = PreDefinedData.GetRandomSuffixNameCode(),
                DisplayText = "Create Test",
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newSuffixNameDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task Update_ValidSuffixName_HttpNoContent()
        {
            // Arrange
            var randomSuffixNameCode = PreDefinedData.GetRandomSuffixNameCode();
            ReferenceSuffixName apiUpdatingSuffixName = UnitOfWork.ReferenceSuffixNames.Get(randomSuffixNameCode);
            apiUpdatingSuffixName.DisplayText = "Update Test";
            var path = GetRelativePath(nameof(ReferenceSuffixNamesController), randomSuffixNameCode);

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingSuffixName),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            ReferenceSuffixName dbUpdatedSuffixName = UnitOfWork.ReferenceSuffixNames.Get(apiUpdatingSuffixName.Code);
            AssertHelper.AreObjectsEqual(apiUpdatingSuffixName, dbUpdatedSuffixName);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomSuffixNameCode = PreDefinedData.GetRandomSuffixNameCode();
            var path = GetRelativePath(nameof(ReferenceSuffixNamesController), randomSuffixNameCode);

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
            var randomSuffixNameCode = PreDefinedData.GetRandomSuffixNameCode();
            var path = GetRelativePath(nameof(ReferenceSuffixNamesController), randomSuffixNameCode);
            var apiUpdatingSuffixName = new ReferenceSuffixNameDto()
            {
                Code = randomSuffixNameCode,
                // Display text is required, keep it missing
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingSuffixName),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Update_InvalidSuffixName_HttpNotFound()
        {
            // Arrange
            var notExistsSuffixNameCode = PreDefinedData.GetNotExistsSuffixNameCode();
            var path = GetRelativePath(nameof(ReferenceSuffixNamesController), notExistsSuffixNameCode);
            var apiUpdatingSuffixName = new ReferenceSuffixNameDto()
            {
                Code = notExistsSuffixNameCode,
                DisplayText = "Update Test"
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingSuffixName),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Delete_ValidSuffixName_HttpNoContent()
        {
            // Arrange
            var randomSuffixNameCode = PreDefinedData.GetRandomSuffixNameCode();
            var path = GetRelativePath(nameof(ReferenceSuffixNamesController), randomSuffixNameCode);

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
        public async Task Delete_InvalidSuffixName_HttpNotFound()
        {
            // Arrange
            var notExistsSuffixNameCode = PreDefinedData.GetNotExistsSuffixNameCode();
            var path = GetRelativePath(nameof(ReferenceSuffixNamesController), notExistsSuffixNameCode);

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
