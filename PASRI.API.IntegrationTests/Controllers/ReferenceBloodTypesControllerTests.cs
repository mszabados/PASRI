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
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            IEnumerable<ReferenceBloodTypeDto> apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceBloodTypeDto>>(responseString);
            IEnumerable<ReferenceBloodTypeDto> preDefinedCollection =
                PreDefinedData.ReferenceBloodTypes.Select(Mapper.Map<ReferenceBloodType, ReferenceBloodTypeDto>).ToList().AsEnumerable<ReferenceBloodTypeDto>();
            ((List<ReferenceBloodTypeDto>)apiReturnedCollection).Sort();
            ((List<ReferenceBloodTypeDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidBloodTypeCode_HttpOkAndReturnsSingleBloodType()
        {
            // Arrange
            var randomBloodTypeCode = PreDefinedData.GetRandomBloodTypeCode();
            var path = GetRelativePath(nameof(ReferenceBloodTypesController), randomBloodTypeCode);

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            ReferenceBloodTypeDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceBloodTypeDto>(responseString);

            ReferenceBloodType preDefinedObject =
                PreDefinedData.ReferenceBloodTypes
                    .SingleOrDefault(c => c.Code == randomBloodTypeCode);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceBloodType, ReferenceBloodTypeDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidBloodTypeCode_HttpNotFound()
        {
            // Arrange
            var notExistsBloodTypeCode = PreDefinedData.GetNotExistsBloodTypeCode();
            var path = GetRelativePath(nameof(ReferenceBloodTypesController), notExistsBloodTypeCode);

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
            var newBloodTypeDto = new ReferenceBloodTypeDto()
            {
                Code = notExistsBloodTypeCode,
                DisplayText = "New BloodType",
                StartDate = DateTime.UtcNow
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
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            ReferenceBloodTypeDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceBloodTypeDto>(responseString);

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
            var notExistsBloodTypeCode = PreDefinedData.GetNotExistsBloodTypeCode();
            var newBloodTypeDto = new ReferenceBloodTypeDto()
            {
                Code = notExistsBloodTypeCode,
                // Display text is required, keep it missing
                StartDate = DateTime.UtcNow
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
            var newBloodTypeDto = new ReferenceBloodTypeDto()
            {
                Code = PreDefinedData.GetRandomBloodTypeCode(),
                DisplayText = "Create Test",
                StartDate = DateTime.UtcNow
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
            var randomBloodTypeCode = PreDefinedData.GetRandomBloodTypeCode();
            ReferenceBloodType apiUpdatingBloodType = UnitOfWork.ReferenceBloodTypes.Get(randomBloodTypeCode);
            apiUpdatingBloodType.DisplayText = "Update Test";
            var path = GetRelativePath(nameof(ReferenceBloodTypesController), randomBloodTypeCode);

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingBloodType),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            ReferenceBloodType dbUpdatedBloodType = UnitOfWork.ReferenceBloodTypes.Get(apiUpdatingBloodType.Code);
            AssertHelper.AreObjectsEqual(apiUpdatingBloodType, dbUpdatedBloodType);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomBloodTypeCode = PreDefinedData.GetRandomBloodTypeCode();
            var path = GetRelativePath(nameof(ReferenceBloodTypesController), randomBloodTypeCode);

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
            var randomBloodTypeCode = PreDefinedData.GetRandomBloodTypeCode();
            var path = GetRelativePath(nameof(ReferenceBloodTypesController), randomBloodTypeCode);
            var apiUpdatingBloodType = new ReferenceBloodTypeDto()
            {
                Code = randomBloodTypeCode,
                // Display text is required, keep it missing
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
            var path = GetRelativePath(nameof(ReferenceBloodTypesController), notExistsBloodTypeCode);
            var apiUpdatingBloodType = new ReferenceBloodTypeDto()
            {
                Code = notExistsBloodTypeCode,
                DisplayText = "Update Test"
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
            var randomBloodTypeCode = PreDefinedData.GetRandomBloodTypeCode();
            var path = GetRelativePath(nameof(ReferenceBloodTypesController), randomBloodTypeCode);

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
        public async Task Delete_InvalidBloodType_HttpNotFound()
        {
            // Arrange
            var notExistsBloodTypeCode = PreDefinedData.GetNotExistsBloodTypeCode();
            var path = GetRelativePath(nameof(ReferenceBloodTypesController), notExistsBloodTypeCode);

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}