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
    /// Integration test class for the <see cref="ReferenceBloodTypesController"/> methods.
    /// Method names should reflect the following pattern:
    /// MethodBeingTested_Scenario_ExpectedBehavior
    /// </summary>
    [TestFixture]
    public class ReferenceBloodTypesControllerTests : BaseIntegrationTestProvider
    {
        [Test]
        public async Task GetAll_WhenCalled_HttpOkAndReturnsMatchingCollection()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceBloodTypesController));

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceBloodTypeDto>>(responseString);
            var preDefinedCollection =
                PreDefinedData.ReferenceBloodTypes.Select(Mapper.Map<ReferenceBloodType, ReferenceBloodTypeDto>).ToList().AsEnumerable();
            ((List<ReferenceBloodTypeDto>)apiReturnedCollection).Sort();
            ((List<ReferenceBloodTypeDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidBloodTypeCode_HttpOkAndReturnsSingleBloodType()
        {
            // Arrange
            var randomBloodTypeId = PreDefinedData.GetRandomBloodTypeId();
            var path = GetRelativePath(nameof(ReferenceBloodTypesController), randomBloodTypeId.ToString());

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceBloodTypeDto>(responseString);

            var preDefinedObject =
                PreDefinedData.ReferenceBloodTypes
                    .SingleOrDefault(c => c.Id == randomBloodTypeId);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceBloodType, ReferenceBloodTypeDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidBloodTypeId_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceBloodTypesController), int.MaxValue.ToString());

            // Act
            var response = await Client.GetAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Create_ValidPayload_HttpCreatedAndReturnsNewBloodType()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceBloodTypesController));
            var notExistsBloodTypeCode = PreDefinedData.GetNotExistsBloodTypeCode();
            var newBloodTypeDto = new ReferenceBloodTypeDto
            {
                Code = notExistsBloodTypeCode,
                LongName = "New",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = CreatedModifiedBy,
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = CreatedModifiedBy
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newBloodTypeDto),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceBloodTypeDto>(responseString);

            Assert.That(apiReturnedObject.Id, Is.GreaterThan(0));

            newBloodTypeDto.Id = apiReturnedObject.Id;
            AssertHelper.AreObjectsEqual(apiReturnedObject, newBloodTypeDto);
        }

        [Test]
        public async Task Create_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceBloodTypesController));

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
            var path = GetRelativePath(nameof(ReferenceBloodTypesController));

            var newBloodTypeDto = new ReferenceBloodTypeDto
            {
                // Code is required, keep it missing
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newBloodTypeDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Create_ExistingBloodType_HttpConflict()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceBloodTypesController));
            var randomBloodTypeId = PreDefinedData.GetRandomBloodTypeId();
            var randomBloodType = PreDefinedData.ReferenceBloodTypes[randomBloodTypeId - 1];

            var newBloodTypeDto = new ReferenceBloodTypeDto
            {
                Code = randomBloodType.Code,
                LongName = "Create Test",
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newBloodTypeDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task Update_ValidBloodType_HttpNoContent()
        {
            // Arrange
            var randomBloodTypeId = PreDefinedData.GetRandomBloodTypeId();

            var apiUpdatingBloodType = UnitOfWork.ReferenceBloodTypes.Get(randomBloodTypeId);
            apiUpdatingBloodType.LongName = "Update Test";
            var path = GetRelativePath(nameof(ReferenceBloodTypesController), randomBloodTypeId.ToString());

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(Mapper.Map<ReferenceBloodType, ReferenceBloodTypeDto>(apiUpdatingBloodType)),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            var dbUpdatedBloodType = UnitOfWork.ReferenceBloodTypes.Get(apiUpdatingBloodType.Id);
            AssertHelper.AreObjectsEqual(apiUpdatingBloodType, dbUpdatedBloodType);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomBloodTypeId = PreDefinedData.GetRandomBloodTypeId();
            var path = GetRelativePath(nameof(ReferenceBloodTypesController), randomBloodTypeId.ToString());

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
            var randomBloodTypeId = PreDefinedData.GetRandomBloodTypeId();
            var path = GetRelativePath(nameof(ReferenceBloodTypesController), randomBloodTypeId.ToString());
            var apiUpdatingBloodType = new ReferenceBloodTypeDto
            {
                Id = randomBloodTypeId
                // Code is required, keep it missing
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingBloodType),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Update_InvalidBloodType_HttpNotFound()
        {
            // Arrange
            var notExistsBloodTypeCode = PreDefinedData.GetNotExistsBloodTypeCode();
            var path = GetRelativePath(nameof(ReferenceBloodTypesController), int.MaxValue.ToString());
            var apiUpdatingBloodType = new ReferenceBloodTypeDto
            {
                Id = int.MaxValue,
                Code = notExistsBloodTypeCode,
                LongName = "Update Test"
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingBloodType),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Delete_ValidBloodType_HttpNoContent()
        {
            // Arrange
            var randomBloodTypeId = PreDefinedData.GetRandomBloodTypeId();
            var path = GetRelativePath(nameof(ReferenceBloodTypesController), randomBloodTypeId.ToString());

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
        public async Task Delete_InvalidBloodType_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceBloodTypesController), int.MaxValue.ToString());

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
