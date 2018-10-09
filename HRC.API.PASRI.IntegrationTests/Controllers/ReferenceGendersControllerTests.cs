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
    /// Integration test class for the <see cref="ReferenceGendersController"/> methods.
    /// Method names should reflect the following pattern:
    /// MethodBeingTested_Scenario_ExpectedBehavior
    /// </summary>
    [TestFixture]
    public class ReferenceGendersControllerTests : BaseIntegrationTestProvider
    {
        [Test]
        public async Task GetAll_WhenCalled_HttpOkAndReturnsMatchingCollection()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceGendersController));

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceGenderDto>>(responseString);
            var preDefinedCollection =
                PreDefinedData.ReferenceGenders.Select(Mapper.Map<ReferenceGender, ReferenceGenderDto>).ToList().AsEnumerable();
            ((List<ReferenceGenderDto>)apiReturnedCollection).Sort();
            ((List<ReferenceGenderDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidGenderCode_HttpOkAndReturnsSingleGender()
        {
            // Arrange
            var randomGenderId = PreDefinedData.GetRandomGenderId();
            var path = GetRelativePath(nameof(ReferenceGendersController), randomGenderId.ToString());

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceGenderDto>(responseString);

            var preDefinedObject =
                PreDefinedData.ReferenceGenders
                    .SingleOrDefault(c => c.Id == randomGenderId);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceGender, ReferenceGenderDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidGenderId_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceGendersController), int.MaxValue.ToString());

            // Act
            var response = await Client.GetAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Create_ValidPayload_HttpCreatedAndReturnsNewGender()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceGendersController));
            var notExistsGenderCode = PreDefinedData.GetNotExistsGenderCode();
            var newGenderDto = new ReferenceGenderDto
            {
                Code = notExistsGenderCode,
                LongName = "New",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = CreatedModifiedBy,
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = CreatedModifiedBy
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newGenderDto),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceGenderDto>(responseString);

            Assert.That(apiReturnedObject.Id, Is.GreaterThan(0));

            newGenderDto.Id = apiReturnedObject.Id;
            AssertHelper.AreObjectsEqual(apiReturnedObject, newGenderDto);
        }

        [Test]
        public async Task Create_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceGendersController));

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
            var path = GetRelativePath(nameof(ReferenceGendersController));

            var newGenderDto = new ReferenceGenderDto
            {
                // Code is required, keep it missing
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newGenderDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Create_ExistingGender_HttpConflict()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceGendersController));
            var randomGenderId = PreDefinedData.GetRandomGenderId();
            var randomGender = PreDefinedData.ReferenceGenders[randomGenderId - 1];

            var newGenderDto = new ReferenceGenderDto
            {
                Code = randomGender.Code,
                LongName = "Create",
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newGenderDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task Update_ValidGender_HttpNoContent()
        {
            // Arrange
            var randomGenderId = PreDefinedData.GetRandomGenderId();

            var apiUpdatingGender = UnitOfWork.ReferenceGenders.Get(randomGenderId);
            apiUpdatingGender.LongName = "Update";
            var path = GetRelativePath(nameof(ReferenceGendersController), randomGenderId.ToString());

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                JsonConvert.SerializeObject(Mapper.Map<ReferenceGender, ReferenceGenderDto>(apiUpdatingGender)),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            var dbUpdatedGender = UnitOfWork.ReferenceGenders.Get(apiUpdatingGender.Id);
            AssertHelper.AreObjectsEqual(apiUpdatingGender, dbUpdatedGender);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomGenderId = PreDefinedData.GetRandomGenderId();
            var path = GetRelativePath(nameof(ReferenceGendersController), randomGenderId.ToString());

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
            var randomGenderId = PreDefinedData.GetRandomGenderId();
            var path = GetRelativePath(nameof(ReferenceGendersController), randomGenderId.ToString());
            var apiUpdatingGender = new ReferenceGenderDto
            {
                Id = randomGenderId
                // Code is required, keep it missing
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingGender),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Update_InvalidGender_HttpNotFound()
        {
            // Arrange
            var notExistsGenderCode = PreDefinedData.GetNotExistsGenderCode();
            var path = GetRelativePath(nameof(ReferenceGendersController), int.MaxValue.ToString());
            var apiUpdatingGender = new ReferenceGenderDto
            {
                Id = int.MaxValue,
                Code = notExistsGenderCode,
                LongName = "Update"
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingGender),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Delete_ValidGender_HttpNoContent()
        {
            // Arrange
            var randomGenderId = PreDefinedData.GetRandomGenderId();
            var path = GetRelativePath(nameof(ReferenceGendersController), randomGenderId.ToString());

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
        public async Task Delete_InvalidGender_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceGendersController), int.MaxValue.ToString());

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
