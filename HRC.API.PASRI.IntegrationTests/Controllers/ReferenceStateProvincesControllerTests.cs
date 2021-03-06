﻿using HRC.API.PASRI.Controllers;
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
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceStateProvinceDto>>(responseString);
            var preDefinedCollection =
                PreDefinedData.ReferenceStateProvinces.Select(Mapper.Map<ReferenceStateProvince, ReferenceStateProvinceDto>).ToList().AsEnumerable();
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
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceStateProvinceDto>(responseString);

            var preDefinedObject =
                PreDefinedData.ReferenceStateProvinces
                    .SingleOrDefault(c => c.Id == randomStateProvinceId);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceStateProvince, ReferenceStateProvinceDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidStateProvinceId_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceStateProvincesController), int.MaxValue.ToString());

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
            var newStateProvinceDto = new ReferenceStateProvinceDto
            {
                CountryCode = "US",
                Code = notExistsStateProvinceCode,
                LongName = "New StateProvince",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = CreatedModifiedBy,
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = CreatedModifiedBy
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
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var apiReturnedObject =
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

            var newStateProvinceDto = new ReferenceStateProvinceDto
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
            
            var newStateProvinceDto = new ReferenceStateProvinceDto
            {
                CountryCode = "US",
                Code = randomStateProvince.Code,
                LongName = "Create Test",
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
            var apiUpdatingStateProvince = UnitOfWork.ReferenceStateProvinces.Get(randomStateProvinceId);
            apiUpdatingStateProvince.LongName = "Update Test";
            var path = GetRelativePath(nameof(ReferenceStateProvincesController), randomStateProvinceId.ToString());

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(Mapper.Map<ReferenceStateProvince, ReferenceStateProvinceDto>(apiUpdatingStateProvince)),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            var dbUpdatedStateProvince = UnitOfWork.ReferenceStateProvinces.Get(apiUpdatingStateProvince.Id);
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
            var apiUpdatingStateProvince = new ReferenceStateProvinceDto
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
            var path = GetRelativePath(nameof(ReferenceStateProvincesController), int.MaxValue.ToString());
            var apiUpdatingStateProvince = new ReferenceStateProvinceDto
            {
                Id = int.MaxValue,
                CountryCode = "US",
                Code = notExistsStateProvinceCode,
                LongName = "Update Test"
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
                string.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public async Task Delete_InvalidStateProvince_HttpNotFound()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceStateProvincesController), int.MaxValue.ToString());

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
