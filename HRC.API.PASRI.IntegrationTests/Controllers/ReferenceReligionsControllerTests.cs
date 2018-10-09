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
    /// Integration test class for the <see cref="ReferenceReligionsController"/> methods.
    /// Method names should reflect the following pattern:
    /// MethodBeingTested_Scenario_ExpectedBehavior
    /// </summary>
    [TestFixture]
    public class ReferenceReligionsControllerTests : BaseIntegrationTestProvider
    {
        [Test]
        public async Task GetAll_WhenCalled_HttpOkAndReturnsMatchingCollection()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceReligionsController));

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceReligionDto>>(responseString);
            var preDefinedCollection =
                PreDefinedData.ReferenceReligions.Select(Mapper.Map<ReferenceReligion, ReferenceReligionDto>).ToList().AsEnumerable();
            ((List<ReferenceReligionDto>)apiReturnedCollection).Sort();
            ((List<ReferenceReligionDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidReligionCode_HttpOkAndReturnsSingleReligion()
        {
            // Arrange
            var randomReligionId = PreDefinedData.GetRandomReligionId();
            var path = GetRelativePath(nameof(ReferenceReligionsController), randomReligionId.ToString());

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceReligionDto>(responseString);

            var preDefinedObject =
                PreDefinedData.ReferenceReligions
                    .SingleOrDefault(c => c.Id == randomReligionId);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceReligion, ReferenceReligionDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidReligionId_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceReligionsController), int.MaxValue.ToString());

            // Act
            var response = await Client.GetAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Create_ValidPayload_HttpCreatedAndReturnsNewReligion()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceReligionsController));
            var notExistsReligionCode = PreDefinedData.GetNotExistsReligionCode();
            var newReligionDto = new ReferenceReligionDto
            {
                Code = notExistsReligionCode,
                LongName = "New Religion",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = CreatedModifiedBy,
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = CreatedModifiedBy
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newReligionDto),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceReligionDto>(responseString);

            Assert.That(apiReturnedObject.Id, Is.GreaterThan(0));

            newReligionDto.Id = apiReturnedObject.Id;
            AssertHelper.AreObjectsEqual(apiReturnedObject, newReligionDto);
        }

        [Test]
        public async Task Create_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceReligionsController));

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
            var path = GetRelativePath(nameof(ReferenceReligionsController));

            var newReligionDto = new ReferenceReligionDto
            {
                // Code is required, keep it missing
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newReligionDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Create_ExistingReligion_HttpConflict()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceReligionsController));
            var randomReligionId = PreDefinedData.GetRandomReligionId();
            var randomReligion = PreDefinedData.ReferenceReligions[randomReligionId - 1];

            var newReligionDto = new ReferenceReligionDto
            {
                Code = randomReligion.Code,
                LongName = "Create Test",
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newReligionDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task Update_ValidReligion_HttpNoContent()
        {
            // Arrange
            var randomReligionId = PreDefinedData.GetRandomReligionId();

            var apiUpdatingReligion = UnitOfWork.ReferenceReligions.Get(randomReligionId);
            apiUpdatingReligion.LongName = "Update Test";
            var path = GetRelativePath(nameof(ReferenceReligionsController), randomReligionId.ToString());

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                JsonConvert.SerializeObject(Mapper.Map<ReferenceReligion, ReferenceReligionDto>(apiUpdatingReligion)),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            var dbUpdatedReligion = UnitOfWork.ReferenceReligions.Get(apiUpdatingReligion.Id);
            AssertHelper.AreObjectsEqual(apiUpdatingReligion, dbUpdatedReligion);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomReligionId = PreDefinedData.GetRandomReligionId();
            var path = GetRelativePath(nameof(ReferenceReligionsController), randomReligionId.ToString());

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
            var randomReligionId = PreDefinedData.GetRandomReligionId();
            var path = GetRelativePath(nameof(ReferenceReligionsController), randomReligionId.ToString());
            var apiUpdatingReligion = new ReferenceReligionDto
            {
                Id = randomReligionId
                // Code is required, keep it missing
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingReligion),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Update_InvalidReligion_HttpNotFound()
        {
            // Arrange
            var notExistsReligionCode = PreDefinedData.GetNotExistsReligionCode();
            var path = GetRelativePath(nameof(ReferenceReligionsController), int.MaxValue.ToString());
            var apiUpdatingReligion = new ReferenceReligionDto
            {
                Id = int.MaxValue,
                Code = notExistsReligionCode,
                LongName = "Update Test"
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingReligion),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Delete_ValidReligion_HttpNoContent()
        {
            // Arrange
            var randomReligionId = PreDefinedData.GetRandomReligionId();
            var path = GetRelativePath(nameof(ReferenceReligionsController), randomReligionId.ToString());

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
        public async Task Delete_InvalidReligion_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceReligionsController), int.MaxValue.ToString());

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
