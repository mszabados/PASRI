﻿using Newtonsoft.Json;
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
    /// Integration test class for the <see cref="ReferenceNameSuffixesController"/> methods.
    /// Method names should reflect the following pattern:
    /// MethodBeingTested_Scenario_ExpectedBehavior
    /// </summary>
    [TestFixture]
    public class ReferenceNameSuffixesControllerTests : BaseIntegrationTestProvider
    {
        [Test]
        public async Task GetAll_WhenCalled_HttpOkAndReturnsMatchingCollection()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceNameSuffixesController));

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            IEnumerable<ReferenceNameSuffixDto> apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceNameSuffixDto>>(responseString);
            IEnumerable<ReferenceNameSuffixDto> preDefinedCollection =
                PreDefinedData.ReferenceNameSuffixes.Select(Mapper.Map<ReferenceNameSuffix, ReferenceNameSuffixDto>).ToList().AsEnumerable<ReferenceNameSuffixDto>();
            ((List<ReferenceNameSuffixDto>)apiReturnedCollection).Sort();
            ((List<ReferenceNameSuffixDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidNameSuffixCode_HttpOkAndReturnsSingleNameSuffix()
        {
            // Arrange
            var randomNameSuffixId = PreDefinedData.GetRandomNameSuffixId();
            var path = GetRelativePath(nameof(ReferenceNameSuffixesController), randomNameSuffixId.ToString());

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            ReferenceNameSuffixDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceNameSuffixDto>(responseString);

            ReferenceNameSuffix preDefinedObject =
                PreDefinedData.ReferenceNameSuffixes
                    .SingleOrDefault(c => c.Id == randomNameSuffixId);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceNameSuffix, ReferenceNameSuffixDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidNameSuffixId_HttpNotFound()
        {
            // Arrange
            var notExistsNameSuffixCode = PreDefinedData.GetNotExistsNameSuffixCode();
            var path = GetRelativePath(nameof(ReferenceNameSuffixesController), Int32.MaxValue.ToString());

            // Act
            var response = await Client.GetAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Create_ValidPayload_HttpCreatedAndReturnsNewNameSuffix()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceNameSuffixesController));
            var notExistsNameSuffixCode = PreDefinedData.GetNotExistsNameSuffixCode();
            var newNameSuffixDto = new ReferenceNameSuffixDto()
            {
                Code = notExistsNameSuffixCode,
                Description = "New NameSuffix",
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newNameSuffixDto),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            ReferenceNameSuffixDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceNameSuffixDto>(responseString);

            Assert.That(apiReturnedObject.Id, Is.GreaterThan(0));

            newNameSuffixDto.Id = apiReturnedObject.Id;
            AssertHelper.AreObjectsEqual(apiReturnedObject, newNameSuffixDto);
        }

        [Test]
        public async Task Create_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceNameSuffixesController));

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
            var path = GetRelativePath(nameof(ReferenceNameSuffixesController));

            var newNameSuffixDto = new ReferenceNameSuffixDto()
            {
                // Code is required, keep it missing
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newNameSuffixDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Create_ExistingNameSuffix_HttpConflict()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceNameSuffixesController));
            var randomNameSuffixId = PreDefinedData.GetRandomNameSuffixId();
            var randomNameSuffix = PreDefinedData.ReferenceNameSuffixes[randomNameSuffixId - 1];

            var newNameSuffixDto = new ReferenceNameSuffixDto()
            {
                Code = randomNameSuffix.Code,
                Description = "Create Test",
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newNameSuffixDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task Update_ValidNameSuffix_HttpNoContent()
        {
            // Arrange
            var randomNameSuffixId = PreDefinedData.GetRandomNameSuffixId();

            ReferenceNameSuffix apiUpdatingNameSuffix = UnitOfWork.ReferenceNameSuffixes.Get(randomNameSuffixId);
            apiUpdatingNameSuffix.Description = "Update Test";
            var path = GetRelativePath(nameof(ReferenceNameSuffixesController), randomNameSuffixId.ToString());

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingNameSuffix),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            ReferenceNameSuffix dbUpdatedNameSuffix = UnitOfWork.ReferenceNameSuffixes.Get(apiUpdatingNameSuffix.Id);
            AssertHelper.AreObjectsEqual(apiUpdatingNameSuffix, dbUpdatedNameSuffix);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomNameSuffixId = PreDefinedData.GetRandomNameSuffixId();
            var path = GetRelativePath(nameof(ReferenceNameSuffixesController), randomNameSuffixId.ToString());

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
            var randomNameSuffixId = PreDefinedData.GetRandomNameSuffixId();
            var path = GetRelativePath(nameof(ReferenceNameSuffixesController), randomNameSuffixId.ToString());
            var apiUpdatingNameSuffix = new ReferenceNameSuffixDto()
            {
                Id = randomNameSuffixId
                // Code is required, keep it missing
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingNameSuffix),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Update_InvalidNameSuffix_HttpNotFound()
        {
            // Arrange
            var notExistsNameSuffixCode = PreDefinedData.GetNotExistsNameSuffixCode();
            var path = GetRelativePath(nameof(ReferenceNameSuffixesController), Int32.MaxValue.ToString());
            var apiUpdatingNameSuffix = new ReferenceNameSuffixDto()
            {
                Id = Int32.MaxValue,
                Code = notExistsNameSuffixCode,
                Description = "Update Test"
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingNameSuffix),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Delete_ValidNameSuffix_HttpNoContent()
        {
            // Arrange
            var randomNameSuffixId = PreDefinedData.GetRandomNameSuffixId();
            var path = GetRelativePath(nameof(ReferenceNameSuffixesController), randomNameSuffixId.ToString());

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
        public async Task Delete_InvalidNameSuffix_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceNameSuffixesController), Int32.MaxValue.ToString());

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}