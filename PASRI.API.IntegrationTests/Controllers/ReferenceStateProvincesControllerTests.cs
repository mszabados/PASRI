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
    /// Integration test class for the <see cref="ReferenceStateProvincesController"/> methods.
    /// Method names should reflect the following pattern:
    /// MethodBeingTested_Scenario_ExpectedBehavior
    /// </summary>
    [TestFixture]
    public class ReferenceStateProvincesControllerTests : BaseIntegrationTestProvider
    {
        [Test]
        public async Task GetAll_WhenCalled_HttpOkAndReturnsMatchingCollection()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceStateProvincesController));

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            IEnumerable<ReferenceStateProvinceDto> apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceStateProvinceDto>>(responseString);
            IEnumerable<ReferenceStateProvinceDto> preDefinedCollection =
                PreDefinedData.ReferenceStateProvinces.Select(Mapper.Map<ReferenceStateProvince, ReferenceStateProvinceDto>).ToList().AsEnumerable<ReferenceStateProvinceDto>();
            ((List<ReferenceStateProvinceDto>)apiReturnedCollection).Sort();
            ((List<ReferenceStateProvinceDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidStateProvinceCode_HttpOkAndReturnsSingleStateProvince()
        {
            // Arrange
            var randomStateProvinceId = PreDefinedData.GetRandomStateProvinceId();
            var path = GetRelativePath(nameof(ReferenceStateProvincesController), randomStateProvinceId.ToString());

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            ReferenceStateProvinceDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceStateProvinceDto>(responseString);

            ReferenceStateProvince preDefinedObject =
                PreDefinedData.ReferenceStateProvinces
                    .SingleOrDefault(c => c.Id == randomStateProvinceId);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceStateProvince, ReferenceStateProvinceDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidStateProvinceId_HttpNotFound()
        {
            // Arrange
            var notExistsStateProvinceCode = PreDefinedData.GetNotExistsStateProvinceCode();
            var path = GetRelativePath(nameof(ReferenceStateProvincesController), Int32.MaxValue.ToString());

            // Act
            var response = await Client.GetAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Create_ValidPayload_HttpCreatedAndReturnsNewStateProvince()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceStateProvincesController));
            var notExistsStateProvinceCode = PreDefinedData.GetNotExistsStateProvinceCode();
            var newStateProvinceDto = new ReferenceStateProvinceDto()
            {
                Code = notExistsStateProvinceCode,
                Description = "New StateProvince",
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newStateProvinceDto),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            ReferenceStateProvinceDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceStateProvinceDto>(responseString);

            Assert.That(apiReturnedObject.Id, Is.GreaterThan(0));

            newStateProvinceDto.Id = apiReturnedObject.Id;
            AssertHelper.AreObjectsEqual(apiReturnedObject, newStateProvinceDto);
        }

        [Test]
        public async Task Create_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceStateProvincesController));

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
            var path = GetRelativePath(nameof(ReferenceStateProvincesController));

            var newStateProvinceDto = new ReferenceStateProvinceDto()
            {
                // Code is required, keep it missing
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newStateProvinceDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Create_ExistingStateProvince_HttpConflict()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceStateProvincesController));
            var randomStateProvinceId = PreDefinedData.GetRandomStateProvinceId();
            var randomStateProvince = PreDefinedData.ReferenceStateProvinces[randomStateProvinceId - 1];

            var newStateProvinceDto = new ReferenceStateProvinceDto()
            {
                Code = randomStateProvince.Code,
                Description = "Create Test",
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newStateProvinceDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task Update_ValidStateProvince_HttpNoContent()
        {
            // Arrange
            var randomStateProvinceId = PreDefinedData.GetRandomStateProvinceId();

            ReferenceStateProvince apiUpdatingStateProvince = UnitOfWork.ReferenceStateProvinces.Get(randomStateProvinceId);
            apiUpdatingStateProvince.Description = "Update Test";
            var path = GetRelativePath(nameof(ReferenceStateProvincesController), randomStateProvinceId.ToString());

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingStateProvince),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            ReferenceStateProvince dbUpdatedStateProvince = UnitOfWork.ReferenceStateProvinces.Get(apiUpdatingStateProvince.Id);
            AssertHelper.AreObjectsEqual(apiUpdatingStateProvince, dbUpdatedStateProvince);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomStateProvinceId = PreDefinedData.GetRandomStateProvinceId();
            var path = GetRelativePath(nameof(ReferenceStateProvincesController), randomStateProvinceId.ToString());

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
            var randomStateProvinceId = PreDefinedData.GetRandomStateProvinceId();
            var path = GetRelativePath(nameof(ReferenceStateProvincesController), randomStateProvinceId.ToString());
            var apiUpdatingStateProvince = new ReferenceStateProvinceDto()
            {
                Id = randomStateProvinceId
                // Code is required, keep it missing
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingStateProvince),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Update_InvalidStateProvince_HttpNotFound()
        {
            // Arrange
            var notExistsStateProvinceCode = PreDefinedData.GetNotExistsStateProvinceCode();
            var path = GetRelativePath(nameof(ReferenceStateProvincesController), Int32.MaxValue.ToString());
            var apiUpdatingStateProvince = new ReferenceStateProvinceDto()
            {
                Id = Int32.MaxValue,
                Code = notExistsStateProvinceCode,
                Description = "Update Test"
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingStateProvince),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Delete_ValidStateProvince_HttpNoContent()
        {
            // Arrange
            var randomStateProvinceId = PreDefinedData.GetRandomStateProvinceId();
            var path = GetRelativePath(nameof(ReferenceStateProvincesController), randomStateProvinceId.ToString());

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
        public async Task Delete_InvalidStateProvince_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceStateProvincesController), Int32.MaxValue.ToString());

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
