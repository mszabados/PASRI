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
    /// Integration test class for the <see cref="ReferenceEthnicGroupsController"/> methods.
    /// Method names should reflect the following pattern:
    /// MethodBeingTested_Scenario_ExpectedBehavior
    /// </summary>
    [TestFixture]
    public class ReferenceEthnicGroupsControllerTests : BaseIntegrationTestProvider
    {
        [Test]
        public async Task GetAll_WhenCalled_HttpOkAndReturnsMatchingCollection()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceEthnicGroupsController));

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceEthnicGroupDto>>(responseString);
            var preDefinedCollection =
                PreDefinedData.ReferenceEthnicGroups.Select(Mapper.Map<ReferenceEthnicGroup, ReferenceEthnicGroupDto>).ToList().AsEnumerable();
            ((List<ReferenceEthnicGroupDto>)apiReturnedCollection).Sort();
            ((List<ReferenceEthnicGroupDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidEthnicGroupCode_HttpOkAndReturnsSingleEthnicGroup()
        {
            // Arrange
            var randomEthnicGroupId = PreDefinedData.GetRandomEthnicGroupId();
            var path = GetRelativePath(nameof(ReferenceEthnicGroupsController), randomEthnicGroupId.ToString());

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceEthnicGroupDto>(responseString);

            var preDefinedObject =
                PreDefinedData.ReferenceEthnicGroups
                    .SingleOrDefault(c => c.Id == randomEthnicGroupId);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceEthnicGroup, ReferenceEthnicGroupDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidEthnicGroupId_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceEthnicGroupsController), int.MaxValue.ToString());

            // Act
            var response = await Client.GetAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Create_ValidPayload_HttpCreatedAndReturnsNewEthnicGroup()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceEthnicGroupsController));
            var notExistsEthnicGroupCode = PreDefinedData.GetNotExistsEthnicGroupCode();
            var newEthnicGroupDto = new ReferenceEthnicGroupDto
            {
                Code = notExistsEthnicGroupCode,
                LongName = "New EthnicGroup",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = CreatedModifiedBy,
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = CreatedModifiedBy
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newEthnicGroupDto),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceEthnicGroupDto>(responseString);

            Assert.That(apiReturnedObject.Id, Is.GreaterThan(0));

            newEthnicGroupDto.Id = apiReturnedObject.Id;
            AssertHelper.AreObjectsEqual(apiReturnedObject, newEthnicGroupDto);
        }

        [Test]
        public async Task Create_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceEthnicGroupsController));

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
            var path = GetRelativePath(nameof(ReferenceEthnicGroupsController));

            var newEthnicGroupDto = new ReferenceEthnicGroupDto
            {
                // Code is required, keep it missing
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newEthnicGroupDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Create_ExistingEthnicGroup_HttpConflict()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceEthnicGroupsController));
            var randomEthnicGroupId = PreDefinedData.GetRandomEthnicGroupId();
            var randomEthnicGroup = PreDefinedData.ReferenceEthnicGroups[randomEthnicGroupId - 1];

            var newEthnicGroupDto = new ReferenceEthnicGroupDto
            {
                Code = randomEthnicGroup.Code,
                LongName = "Create Test",
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newEthnicGroupDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task Update_ValidEthnicGroup_HttpNoContent()
        {
            // Arrange
            var randomEthnicGroupId = PreDefinedData.GetRandomEthnicGroupId();

            var apiUpdatingEthnicGroup = UnitOfWork.ReferenceEthnicGroups.Get(randomEthnicGroupId);
            apiUpdatingEthnicGroup.LongName = "Update Test";
            var path = GetRelativePath(nameof(ReferenceEthnicGroupsController), randomEthnicGroupId.ToString());

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(Mapper.Map<ReferenceEthnicGroup, ReferenceEthnicGroupDto>(apiUpdatingEthnicGroup)),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            var dbUpdatedEthnicGroup = UnitOfWork.ReferenceEthnicGroups.Get(apiUpdatingEthnicGroup.Id);
            AssertHelper.AreObjectsEqual(apiUpdatingEthnicGroup, dbUpdatedEthnicGroup);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomEthnicGroupId = PreDefinedData.GetRandomEthnicGroupId();
            var path = GetRelativePath(nameof(ReferenceEthnicGroupsController), randomEthnicGroupId.ToString());

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
            var randomEthnicGroupId = PreDefinedData.GetRandomEthnicGroupId();
            var path = GetRelativePath(nameof(ReferenceEthnicGroupsController), randomEthnicGroupId.ToString());
            var apiUpdatingEthnicGroup = new ReferenceEthnicGroupDto
            {
                Id = randomEthnicGroupId
                // Code is required, keep it missing
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingEthnicGroup),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Update_InvalidEthnicGroup_HttpNotFound()
        {
            // Arrange
            var notExistsEthnicGroupCode = PreDefinedData.GetNotExistsEthnicGroupCode();
            var path = GetRelativePath(nameof(ReferenceEthnicGroupsController), int.MaxValue.ToString());
            var apiUpdatingEthnicGroup = new ReferenceEthnicGroupDto
            {
                Id = int.MaxValue,
                Code = notExistsEthnicGroupCode,
                LongName = "Update Test"
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingEthnicGroup),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Delete_ValidEthnicGroup_HttpNoContent()
        {
            // Arrange
            var randomEthnicGroupId = PreDefinedData.GetRandomEthnicGroupId();
            var path = GetRelativePath(nameof(ReferenceEthnicGroupsController), randomEthnicGroupId.ToString());

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
        public async Task Delete_InvalidEthnicGroup_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceEthnicGroupsController), int.MaxValue.ToString());

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
