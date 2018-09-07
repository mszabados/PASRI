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
    /// Integration test class for the <see cref="ReferenceRaceDemographicsController"/> methods.
    /// Method names should reflect the following pattern:
    /// MethodBeingTested_Scenario_ExpectedBehavior
    /// </summary>
    [TestFixture]
    public class ReferenceRaceDemographicsControllerTests : BaseIntegrationTestProvider
    {
        [Test]
        public async Task GetAll_WhenCalled_HttpOkAndReturnsMatchingCollection()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController));

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            IEnumerable<ReferenceRaceDemographicDto> apiReturnedCollection =
                JsonConvert.DeserializeObject<IEnumerable<ReferenceRaceDemographicDto>>(responseString);
            IEnumerable<ReferenceRaceDemographicDto> preDefinedCollection =
                PreDefinedData.ReferenceRaceDemographics.Select(Mapper.Map<ReferenceRaceDemographic, ReferenceRaceDemographicDto>).ToList().AsEnumerable<ReferenceRaceDemographicDto>();
            ((List<ReferenceRaceDemographicDto>)apiReturnedCollection).Sort();
            ((List<ReferenceRaceDemographicDto>)preDefinedCollection).Sort();

            AssertHelper.AreObjectsEqual(apiReturnedCollection, preDefinedCollection);
        }

        [Test]
        public async Task Get_ValidRaceDemographicCode_HttpOkAndReturnsSingleRaceDemographic()
        {
            // Arrange
            var randomRaceDemographicCode = PreDefinedData.GetRandomRaceDemographicCode();
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController), randomRaceDemographicCode);

            // Act
            var response = await Client.GetAsync(path);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            ReferenceRaceDemographicDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceRaceDemographicDto>(responseString);

            ReferenceRaceDemographic preDefinedObject =
                PreDefinedData.ReferenceRaceDemographics
                    .SingleOrDefault(c => c.Code == randomRaceDemographicCode);

            AssertHelper.AreObjectsEqual(apiReturnedObject,
                Mapper.Map<ReferenceRaceDemographic, ReferenceRaceDemographicDto>(preDefinedObject));
        }

        [Test]
        public async Task Get_InvalidRaceDemographicCode_HttpNotFound()
        {
            // Arrange
            var notExistsRaceDemographicCode = PreDefinedData.GetNotExistsRaceDemographicCode();
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController), notExistsRaceDemographicCode);

            // Act
            var response = await Client.GetAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Create_ValidPayload_HttpCreatedAndReturnsNewRaceDemographic()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController));
            var notExistsRaceDemographicCode = PreDefinedData.GetNotExistsRaceDemographicCode();
            var newRaceDemographicDto = new ReferenceRaceDemographicDto()
            {
                Code = notExistsRaceDemographicCode,
                DisplayText = "New RaceDemographic",
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newRaceDemographicDto),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            ReferenceRaceDemographicDto apiReturnedObject =
                JsonConvert.DeserializeObject<ReferenceRaceDemographicDto>(responseString);

            AssertHelper.AreObjectsEqual(apiReturnedObject, newRaceDemographicDto);
        }

        [Test]
        public async Task Create_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController));

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
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController));
            var notExistsRaceDemographicCode = PreDefinedData.GetNotExistsRaceDemographicCode();
            var newRaceDemographicDto = new ReferenceRaceDemographicDto()
            {
                Code = notExistsRaceDemographicCode,
                // Display text is required, keep it missing
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newRaceDemographicDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Create_ExistingRaceDemographic_HttpConflict()
        {
            // Arrange
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController));
            var newRaceDemographicDto = new ReferenceRaceDemographicDto()
            {
                Code = PreDefinedData.GetRandomRaceDemographicCode(),
                DisplayText = "Create Test",
                StartDate = DateTime.UtcNow
            };

            // Act
            var response = await Client.PostAsync(path, new StringContent(
                    JsonConvert.SerializeObject(newRaceDemographicDto),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task Update_ValidRaceDemographic_HttpNoContent()
        {
            // Arrange
            var randomRaceDemographicCode = PreDefinedData.GetRandomRaceDemographicCode();
            ReferenceRaceDemographic apiUpdatingRaceDemographic = UnitOfWork.ReferenceRaceDemographics.Get(randomRaceDemographicCode);
            apiUpdatingRaceDemographic.DisplayText = "Update Test";
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController), randomRaceDemographicCode);

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingRaceDemographic),
                    Encoding.UTF8,
                    JsonMediaType));
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.DoesNotThrow(
                () => response.EnsureSuccessStatusCode(),
                String.Format(HttpExceptionFormattedMessage, responseString));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            ReferenceRaceDemographic dbUpdatedRaceDemographic = UnitOfWork.ReferenceRaceDemographics.Get(apiUpdatingRaceDemographic.Code);
            AssertHelper.AreObjectsEqual(apiUpdatingRaceDemographic, dbUpdatedRaceDemographic);
        }

        [Test]
        public async Task Update_EmptyPayload_HttpBadRequest()
        {
            // Arrange
            var randomRaceDemographicCode = PreDefinedData.GetRandomRaceDemographicCode();
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController), randomRaceDemographicCode);

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
            var randomRaceDemographicCode = PreDefinedData.GetRandomRaceDemographicCode();
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController), randomRaceDemographicCode);
            var apiUpdatingRaceDemographic = new ReferenceRaceDemographicDto()
            {
                Code = randomRaceDemographicCode,
                // Display text is required, keep it missing
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingRaceDemographic),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Update_InvalidRaceDemographic_HttpNotFound()
        {
            // Arrange
            var notExistsRaceDemographicCode = PreDefinedData.GetNotExistsRaceDemographicCode();
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController), notExistsRaceDemographicCode);
            var apiUpdatingRaceDemographic = new ReferenceRaceDemographicDto()
            {
                Code = notExistsRaceDemographicCode,
                DisplayText = "Update Test"
            };

            // Act
            var response = await Client.PutAsync(path, new StringContent(
                    JsonConvert.SerializeObject(apiUpdatingRaceDemographic),
                    Encoding.UTF8,
                    JsonMediaType));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Delete_ValidRaceDemographic_HttpNoContent()
        {
            // Arrange
            var randomRaceDemographicCode = PreDefinedData.GetRandomRaceDemographicCode();
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController), randomRaceDemographicCode);

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
        public async Task Delete_InvalidRaceDemographic_HttpNotFound()
        {
            // Arrange
            var notExistsRaceDemographicCode = PreDefinedData.GetNotExistsRaceDemographicCode();
            var path = GetRelativePath(nameof(ReferenceRaceDemographicsController), notExistsRaceDemographicCode);

            // Act
            var response = await Client.DeleteAsync(path);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
