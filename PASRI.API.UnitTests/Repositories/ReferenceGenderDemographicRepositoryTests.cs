using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PASRI.API.Core.Domain;
using PASRI.API.TestHelper;
// ReSharper disable PossibleMultipleEnumeration

namespace PASRI.API.UnitTests.Repositories
{
    [TestFixture]
    public class ReferenceGenderDemographicRepositoryTests : BaseUnitTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceGenderDemographics.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceGenderDemographics.Length));
        }

        [Test]
        public void Get_ValidGenderDemographicCode_ReturnsSingleGenderDemographic()
        {
            var randomGenderDemographicId = PreDefinedData.GetRandomGenderDemographicId();

            var result = UnitOfWork.ReferenceGenderDemographics.Get(randomGenderDemographicId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(randomGenderDemographicId));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidGenderDemographicCode_ReturnsNull(string invalidGenderDemographicCode)
        {
            var result = UnitOfWork.ReferenceGenderDemographics.Find(p => p.Code == invalidGenderDemographicCode);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Find_PredicateUsedToFindOneGenderDemographic_ReturnsCollection()
        {
            var randomGenderDemographicId = PreDefinedData.GetRandomGenderDemographicId();

            var result = UnitOfWork.ReferenceGenderDemographics.Find(p => p.Id == randomGenderDemographicId);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id == randomGenderDemographicId);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneGenderDemographic_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceGenderDemographics.Find(p => p.Id != int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Find_PredicateUsedToFindNoGenderDemographics_ReturnsEmptyCollection()
        {
            var result = UnitOfWork.ReferenceGenderDemographics.Find(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneGenderDemographic_ReturnsOneGenderDemographic()
        {
            var randomGenderDemographicId = PreDefinedData.GetRandomGenderDemographicId();

            var result = UnitOfWork.ReferenceGenderDemographics.SingleOrDefault(p => p.Id == randomGenderDemographicId);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneGenderDemographic_ThrowsInvalidOperationException()
        {
            var randomGenderDemographicId = PreDefinedData.GetRandomGenderDemographicId();

            Assert.That(() =>
                UnitOfWork.ReferenceGenderDemographics.SingleOrDefault(p => p.Id != randomGenderDemographicId),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoGenderDemographics_ReturnsNull()
        {
            var result = UnitOfWork.ReferenceGenderDemographics.SingleOrDefault(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidGenderDemographicNotExists_FetchNewGenderDemographic()
        {
            var notExistsGenderDemographicCode = PreDefinedData.GetNotExistsGenderDemographicCode();
            var newReferenceGenderDemographic = new ReferenceGenderDemographic
            {
                Code = notExistsGenderDemographicCode,
                LongName = notExistsGenderDemographicCode
            };

            UnitOfWork.ReferenceGenderDemographics.Add(newReferenceGenderDemographic);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceGenderDemographics.Get(newReferenceGenderDemographic.Id);

            Assert.That(result, Is.Not.Null);

            AssertHelper.AreObjectsEqual(newReferenceGenderDemographic, result);
        }

        [Test]
        public void Add_ValidGenderDemographicExists_ThrowsInvalidOperationException()
        {
            var randomGenderDemographicId = PreDefinedData.GetRandomGenderDemographicId();
            var randomGenderDemographic = UnitOfWork.ReferenceGenderDemographics.Get(randomGenderDemographicId);

            Assert.That(() => UnitOfWork.ReferenceGenderDemographics.Add(
                new ReferenceGenderDemographic
                {
                    Id = randomGenderDemographic.Id,
                    Code = randomGenderDemographic.Code
                }),
                Throws.InvalidOperationException);
        }

        [Test]
        public void Add_InvalidGenderDemographic_ThrowsDbUpdateException()
        {
            UnitOfWork.ReferenceGenderDemographics.Add(new ReferenceGenderDemographic());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void AddRange_TwoValidGenderDemographics_CountIncreasedByTwo()
        {
        Start:
            var notExistsGenderDemographicCode1 = PreDefinedData.GetNotExistsGenderDemographicCode();
            var notExistsGenderDemographicCode2 = PreDefinedData.GetNotExistsGenderDemographicCode();
            if (notExistsGenderDemographicCode1 == notExistsGenderDemographicCode2)
                goto Start;

            var newGenderDemographics = new Collection<ReferenceGenderDemographic>
            {
                new ReferenceGenderDemographic { Code = notExistsGenderDemographicCode1, LongName = "" },
                new ReferenceGenderDemographic { Code = notExistsGenderDemographicCode2, LongName = "" }
            };
            UnitOfWork.ReferenceGenderDemographics.AddRange(newGenderDemographics);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceGenderDemographics.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceGenderDemographics.Length + newGenderDemographics.Count));
        }

        [Test]
        public void AddRange_TwoValidGenderDemographicsDuplicated_ThrowsInvalidOperationException()
        {
            var notExistsGenderDemographicCode = PreDefinedData.GetNotExistsGenderDemographicCode();
            var newGenderDemographics = new Collection<ReferenceGenderDemographic>
            {
                new ReferenceGenderDemographic { Id = int.MaxValue, Code = notExistsGenderDemographicCode, LongName = "" },
                new ReferenceGenderDemographic { Id = int.MaxValue, Code = notExistsGenderDemographicCode, LongName = "" }
            };

            Assert.That(() => UnitOfWork.ReferenceGenderDemographics.AddRange(newGenderDemographics),
                Throws.InvalidOperationException);
        }

        [Test]
        public void AddRange_TwoMalformedGenderDemographics_ThrowsDbUpdateException()
        {
            var newGenderDemographics = new Collection<ReferenceGenderDemographic>
            {
                new ReferenceGenderDemographic(),
                new ReferenceGenderDemographic()
            };
            UnitOfWork.ReferenceGenderDemographics.AddRange(newGenderDemographics);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void Remove_ValidGenderDemographicNotExists_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceGenderDemographics.Remove(
                new ReferenceGenderDemographic
                {
                    Id = int.MaxValue
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidGenderDemographicExists_GenderDemographicCannotBeFetched()
        {
            var randomGenderDemographicId = PreDefinedData.GetRandomGenderDemographicId();
            var removeReferenceGenderDemographic = UnitOfWork.ReferenceGenderDemographics.Get(randomGenderDemographicId);
            UnitOfWork.ReferenceGenderDemographics.Remove(removeReferenceGenderDemographic);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceGenderDemographics.Get(removeReferenceGenderDemographic.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidGenderDemographic_ThrowsInvalidOperationException()
        {
            Assert.That(() => UnitOfWork.ReferenceGenderDemographics.Remove(new ReferenceGenderDemographic()),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceGenderDemographics = UnitOfWork.ReferenceGenderDemographics.GetAll().ToList();
            var removeReferenceGenderDemographics = new Collection<ReferenceGenderDemographic>();
            var removeCount = new Random().Next(1, referenceGenderDemographics.Count);

            for (var i = 0; i < removeCount; i++)
            {
                removeReferenceGenderDemographics.Add(referenceGenderDemographics.ElementAt(i));
            }

            UnitOfWork.ReferenceGenderDemographics.RemoveRange(removeReferenceGenderDemographics);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceGenderDemographics.GetAll();

            Assert.That(result.Count, Is.EqualTo(referenceGenderDemographics.Count - removeCount));
        }

        [Test]
        public void RemoveRange_TwoValidGenderDemographicsDuplicated_ThrowsInvalidOperationException()
        {
            var randomGenderDemographicId = PreDefinedData.GetRandomGenderDemographicId();
            var randomGenderDemographic = UnitOfWork.ReferenceGenderDemographics.Get(randomGenderDemographicId);

            var existingGenderDemographics = new Collection<ReferenceGenderDemographic>
            {
                new ReferenceGenderDemographic { Id = randomGenderDemographic.Id, Code = randomGenderDemographic.Code },
                new ReferenceGenderDemographic { Id = randomGenderDemographic.Id, Code = randomGenderDemographic.Code }
            };

            Assert.That(() => UnitOfWork.ReferenceGenderDemographics.RemoveRange(existingGenderDemographics),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedGenderDemographics_InvalidOperationException()
        {
            var removeReferenceGenderDemographics = new Collection<ReferenceGenderDemographic>
            {
                new ReferenceGenderDemographic(),
                new ReferenceGenderDemographic()
            };

            Assert.That(() => UnitOfWork.ReferenceGenderDemographics.RemoveRange(removeReferenceGenderDemographics),
                Throws.InvalidOperationException);
        }
    }
}
