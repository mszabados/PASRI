using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PASRI.API.Core.Domain;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using PASRI.API.TestHelper;

namespace PASRI.API.UnitTests.Repositories
{
    [TestFixture]
    public class ReferenceReligionDemographicRepositoryTests : BaseUnitTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceReligionDemographics.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceReligionDemographics.Length));
        }

        [Test]
        public void Get_ValidReligionDemographicCode_ReturnsSingleReligionDemographic()
        {
            var randomReligionDemographicId = PreDefinedData.GetRandomReligionDemographicId();

            var result = UnitOfWork.ReferenceReligionDemographics.Get(randomReligionDemographicId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(randomReligionDemographicId));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidReligionDemographicCode_ReturnsNull(string invalidReligionDemographicCode)
        {
            var result = UnitOfWork.ReferenceReligionDemographics.Find(p => p.Code == invalidReligionDemographicCode);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Find_PredicateUsedToFindOneReligionDemographic_ReturnsCollection()
        {
            var randomReligionDemographicId = PreDefinedData.GetRandomReligionDemographicId();

            var result = UnitOfWork.ReferenceReligionDemographics.Find(p => p.Id == randomReligionDemographicId);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ElementAt(0).Id == randomReligionDemographicId);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneReligionDemographic_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceReligionDemographics.Find(p => p.Id != Int32.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Find_PredicateUsedToFindNoReligionDemographics_ReturnsEmptyCollection()
        {
            var result = UnitOfWork.ReferenceReligionDemographics.Find(p => p.Id == Int32.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneReligionDemographic_ReturnsOneReligionDemographic()
        {
            var randomReligionDemographicId = PreDefinedData.GetRandomReligionDemographicId();

            var result = UnitOfWork.ReferenceReligionDemographics.SingleOrDefault(p => p.Id == randomReligionDemographicId);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneReligionDemographic_ThrowsInvalidOperationException()
        {
            var randomReligionDemographicId = PreDefinedData.GetRandomReligionDemographicId();

            Assert.That(() =>
                UnitOfWork.ReferenceReligionDemographics.SingleOrDefault(p => p.Id != randomReligionDemographicId),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoReligionDemographics_ReturnsNull()
        {
            var result = UnitOfWork.ReferenceReligionDemographics.SingleOrDefault(p => p.Id == Int32.MaxValue);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidReligionDemographicNotExists_FetchNewReligionDemographic()
        {
            var notExistsReligionDemographicCode = PreDefinedData.GetNotExistsReligionDemographicCode();
            var newReferenceReligionDemographic = new ReferenceReligionDemographic()
            {
                Code = notExistsReligionDemographicCode,
                Description = notExistsReligionDemographicCode
            };

            UnitOfWork.ReferenceReligionDemographics.Add(newReferenceReligionDemographic);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceReligionDemographics.Get(newReferenceReligionDemographic.Id);

            Assert.That(result, Is.Not.Null);

            AssertHelper.AreObjectsEqual(newReferenceReligionDemographic, result);
        }

        [Test]
        public void Add_ValidReligionDemographicExists_ThrowsInvalidOperationException()
        {
            var randomReligionDemographicId = PreDefinedData.GetRandomReligionDemographicId();
            var randomReligionDemographic = UnitOfWork.ReferenceReligionDemographics.Get(randomReligionDemographicId);

            Assert.That(() => UnitOfWork.ReferenceReligionDemographics.Add(
                new ReferenceReligionDemographic()
                {
                    Id = randomReligionDemographic.Id,
                    Code = randomReligionDemographic.Code
                }),
                Throws.InvalidOperationException);
        }

        [Test]
        public void Add_InvalidReligionDemographic_ThrowsDbUpdateException()
        {
            UnitOfWork.ReferenceReligionDemographics.Add(new ReferenceReligionDemographic());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void AddRange_TwoValidReligionDemographics_CountIncreasedByTwo()
        {
        Start:
            var notExistsReligionDemographicCode1 = PreDefinedData.GetNotExistsReligionDemographicCode();
            var notExistsReligionDemographicCode2 = PreDefinedData.GetNotExistsReligionDemographicCode();
            if (notExistsReligionDemographicCode1 == notExistsReligionDemographicCode2)
                goto Start;

            var newReligionDemographics = new Collection<ReferenceReligionDemographic>
            {
                new ReferenceReligionDemographic() { Code = notExistsReligionDemographicCode1, Description = "" },
                new ReferenceReligionDemographic() { Code = notExistsReligionDemographicCode2, Description = "" }
            };
            UnitOfWork.ReferenceReligionDemographics.AddRange(newReligionDemographics);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceReligionDemographics.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceReligionDemographics.Length + newReligionDemographics.Count));
        }

        [Test]
        public void AddRange_TwoValidReligionDemographicsDuplicated_ThrowsInvalidOperationException()
        {
            var notExistsReligionDemographicCode = PreDefinedData.GetNotExistsReligionDemographicCode();
            var newReligionDemographics = new Collection<ReferenceReligionDemographic>
            {
                new ReferenceReligionDemographic() { Id = Int32.MaxValue, Code = notExistsReligionDemographicCode, Description = "" },
                new ReferenceReligionDemographic() { Id = Int32.MaxValue, Code = notExistsReligionDemographicCode, Description = "" }
            };

            Assert.That(() => UnitOfWork.ReferenceReligionDemographics.AddRange(newReligionDemographics),
                Throws.InvalidOperationException);
        }

        [Test]
        public void AddRange_TwoMalformedReligionDemographics_ThrowsDbUpdateException()
        {
            var newReligionDemographics = new Collection<ReferenceReligionDemographic>
            {
                new ReferenceReligionDemographic(),
                new ReferenceReligionDemographic()
            };
            UnitOfWork.ReferenceReligionDemographics.AddRange(newReligionDemographics);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void Remove_ValidReligionDemographicNotExists_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceReligionDemographics.Remove(
                new ReferenceReligionDemographic()
                {
                    Id = Int32.MaxValue
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidReligionDemographicExists_ReligionDemographicCannotBeFetched()
        {
            var randomReligionDemographicId = PreDefinedData.GetRandomReligionDemographicId();
            var removeReferenceReligionDemographic = UnitOfWork.ReferenceReligionDemographics.Get(randomReligionDemographicId);
            UnitOfWork.ReferenceReligionDemographics.Remove(removeReferenceReligionDemographic);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceReligionDemographics.Get(removeReferenceReligionDemographic.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidReligionDemographic_ThrowsInvalidOperationException()
        {
            Assert.That(() => UnitOfWork.ReferenceReligionDemographics.Remove(new ReferenceReligionDemographic()),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceReligionDemographics = UnitOfWork.ReferenceReligionDemographics.GetAll().ToList();
            var removeReferenceReligionDemographics = new Collection<ReferenceReligionDemographic>();
            var removeCount = new Random().Next(1, referenceReligionDemographics.Count);

            for (int i = 0; i < removeCount; i++)
            {
                removeReferenceReligionDemographics.Add(referenceReligionDemographics.ElementAt(i));
            }

            UnitOfWork.ReferenceReligionDemographics.RemoveRange(removeReferenceReligionDemographics);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceReligionDemographics.GetAll();

            Assert.That(result.Count, Is.EqualTo(referenceReligionDemographics.Count - removeCount));
        }

        [Test]
        public void RemoveRange_TwoValidReligionDemographicsDuplicated_ThrowsInvalidOperationException()
        {
            var randomReligionDemographicId = PreDefinedData.GetRandomReligionDemographicId();
            var randomReligionDemographic = UnitOfWork.ReferenceReligionDemographics.Get(randomReligionDemographicId);

            var existingReligionDemographics = new Collection<ReferenceReligionDemographic>
            {
                new ReferenceReligionDemographic() { Id = randomReligionDemographic.Id, Code = randomReligionDemographic.Code },
                new ReferenceReligionDemographic() { Id = randomReligionDemographic.Id, Code = randomReligionDemographic.Code }
            };

            Assert.That(() => UnitOfWork.ReferenceReligionDemographics.RemoveRange(existingReligionDemographics),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedReligionDemographics_InvalidOperationException()
        {
            var removeReferenceReligionDemographics = new Collection<ReferenceReligionDemographic>
            {
                new ReferenceReligionDemographic(),
                new ReferenceReligionDemographic()
            };

            Assert.That(() => UnitOfWork.ReferenceReligionDemographics.RemoveRange(removeReferenceReligionDemographics),
                Throws.InvalidOperationException);
        }
    }
}
