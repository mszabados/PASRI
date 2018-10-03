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
    public class ReferenceRaceDemographicRepositoryTests : BaseUnitTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceRaceDemographics.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceRaceDemographics.Length));
        }

        [Test]
        public void Get_ValidRaceDemographicCode_ReturnsSingleRaceDemographic()
        {
            var randomRaceDemographicId = PreDefinedData.GetRandomRaceDemographicId();

            var result = UnitOfWork.ReferenceRaceDemographics.Get(randomRaceDemographicId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(randomRaceDemographicId));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidRaceDemographicCode_ReturnsNull(string invalidRaceDemographicCode)
        {
            var result = UnitOfWork.ReferenceRaceDemographics.Find(p => p.Code == invalidRaceDemographicCode);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Find_PredicateUsedToFindOneRaceDemographic_ReturnsCollection()
        {
            var randomRaceDemographicId = PreDefinedData.GetRandomRaceDemographicId();

            var result = UnitOfWork.ReferenceRaceDemographics.Find(p => p.Id == randomRaceDemographicId);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ElementAt(0).Id == randomRaceDemographicId);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneRaceDemographic_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceRaceDemographics.Find(p => p.Id != int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Find_PredicateUsedToFindNoRaceDemographics_ReturnsEmptyCollection()
        {
            var result = UnitOfWork.ReferenceRaceDemographics.Find(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneRaceDemographic_ReturnsOneRaceDemographic()
        {
            var randomRaceDemographicId = PreDefinedData.GetRandomRaceDemographicId();

            var result = UnitOfWork.ReferenceRaceDemographics.SingleOrDefault(p => p.Id == randomRaceDemographicId);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneRaceDemographic_ThrowsInvalidOperationException()
        {
            var randomRaceDemographicId = PreDefinedData.GetRandomRaceDemographicId();

            Assert.That(() =>
                UnitOfWork.ReferenceRaceDemographics.SingleOrDefault(p => p.Id != randomRaceDemographicId),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoRaceDemographics_ReturnsNull()
        {
            var result = UnitOfWork.ReferenceRaceDemographics.SingleOrDefault(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidRaceDemographicNotExists_FetchNewRaceDemographic()
        {
            var notExistsRaceDemographicCode = PreDefinedData.GetNotExistsRaceDemographicCode();
            var newReferenceRaceDemographic = new ReferenceRaceDemographic
            {
                Code = notExistsRaceDemographicCode,
                LongName = notExistsRaceDemographicCode
            };

            UnitOfWork.ReferenceRaceDemographics.Add(newReferenceRaceDemographic);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceRaceDemographics.Get(newReferenceRaceDemographic.Id);

            Assert.That(result, Is.Not.Null);

            AssertHelper.AreObjectsEqual(newReferenceRaceDemographic, result);
        }

        [Test]
        public void Add_ValidRaceDemographicExists_ThrowsInvalidOperationException()
        {
            var randomRaceDemographicId = PreDefinedData.GetRandomRaceDemographicId();
            var randomRaceDemographic = UnitOfWork.ReferenceRaceDemographics.Get(randomRaceDemographicId);

            Assert.That(() => UnitOfWork.ReferenceRaceDemographics.Add(
                new ReferenceRaceDemographic
                {
                    Id = randomRaceDemographic.Id,
                    Code = randomRaceDemographic.Code
                }),
                Throws.InvalidOperationException);
        }

        [Test]
        public void Add_InvalidRaceDemographic_ThrowsDbUpdateException()
        {
            UnitOfWork.ReferenceRaceDemographics.Add(new ReferenceRaceDemographic());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void AddRange_TwoValidRaceDemographics_CountIncreasedByTwo()
        {
        Start:
            var notExistsRaceDemographicCode1 = PreDefinedData.GetNotExistsRaceDemographicCode();
            var notExistsRaceDemographicCode2 = PreDefinedData.GetNotExistsRaceDemographicCode();
            if (notExistsRaceDemographicCode1 == notExistsRaceDemographicCode2)
                goto Start;

            var newRaceDemographics = new Collection<ReferenceRaceDemographic>
            {
                new ReferenceRaceDemographic { Code = notExistsRaceDemographicCode1, LongName = "" },
                new ReferenceRaceDemographic { Code = notExistsRaceDemographicCode2, LongName = "" }
            };
            UnitOfWork.ReferenceRaceDemographics.AddRange(newRaceDemographics);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceRaceDemographics.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceRaceDemographics.Length + newRaceDemographics.Count));
        }

        [Test]
        public void AddRange_TwoValidRaceDemographicsDuplicated_ThrowsInvalidOperationException()
        {
            var notExistsRaceDemographicCode = PreDefinedData.GetNotExistsRaceDemographicCode();
            var newRaceDemographics = new Collection<ReferenceRaceDemographic>
            {
                new ReferenceRaceDemographic { Id = int.MaxValue, Code = notExistsRaceDemographicCode, LongName = "" },
                new ReferenceRaceDemographic { Id = int.MaxValue, Code = notExistsRaceDemographicCode, LongName = "" }
            };

            Assert.That(() => UnitOfWork.ReferenceRaceDemographics.AddRange(newRaceDemographics),
                Throws.InvalidOperationException);
        }

        [Test]
        public void AddRange_TwoMalformedRaceDemographics_ThrowsDbUpdateException()
        {
            var newRaceDemographics = new Collection<ReferenceRaceDemographic>
            {
                new ReferenceRaceDemographic(),
                new ReferenceRaceDemographic()
            };
            UnitOfWork.ReferenceRaceDemographics.AddRange(newRaceDemographics);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void Remove_ValidRaceDemographicNotExists_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceRaceDemographics.Remove(
                new ReferenceRaceDemographic
                {
                    Id = int.MaxValue
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidRaceDemographicExists_RaceDemographicCannotBeFetched()
        {
            var randomRaceDemographicId = PreDefinedData.GetRandomRaceDemographicId();
            var removeReferenceRaceDemographic = UnitOfWork.ReferenceRaceDemographics.Get(randomRaceDemographicId);
            UnitOfWork.ReferenceRaceDemographics.Remove(removeReferenceRaceDemographic);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceRaceDemographics.Get(removeReferenceRaceDemographic.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidRaceDemographic_ThrowsInvalidOperationException()
        {
            Assert.That(() => UnitOfWork.ReferenceRaceDemographics.Remove(new ReferenceRaceDemographic()),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceRaceDemographics = UnitOfWork.ReferenceRaceDemographics.GetAll().ToList();
            var removeReferenceRaceDemographics = new Collection<ReferenceRaceDemographic>();
            var removeCount = new Random().Next(1, referenceRaceDemographics.Count);

            for (var i = 0; i < removeCount; i++)
            {
                removeReferenceRaceDemographics.Add(referenceRaceDemographics.ElementAt(i));
            }

            UnitOfWork.ReferenceRaceDemographics.RemoveRange(removeReferenceRaceDemographics);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceRaceDemographics.GetAll();

            Assert.That(result.Count, Is.EqualTo(referenceRaceDemographics.Count - removeCount));
        }

        [Test]
        public void RemoveRange_TwoValidRaceDemographicsDuplicated_ThrowsInvalidOperationException()
        {
            var randomRaceDemographicId = PreDefinedData.GetRandomRaceDemographicId();
            var randomRaceDemographic = UnitOfWork.ReferenceRaceDemographics.Get(randomRaceDemographicId);

            var existingRaceDemographics = new Collection<ReferenceRaceDemographic>
            {
                new ReferenceRaceDemographic { Id = randomRaceDemographic.Id, Code = randomRaceDemographic.Code },
                new ReferenceRaceDemographic { Id = randomRaceDemographic.Id, Code = randomRaceDemographic.Code }
            };

            Assert.That(() => UnitOfWork.ReferenceRaceDemographics.RemoveRange(existingRaceDemographics),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedRaceDemographics_InvalidOperationException()
        {
            var removeReferenceRaceDemographics = new Collection<ReferenceRaceDemographic>
            {
                new ReferenceRaceDemographic(),
                new ReferenceRaceDemographic()
            };

            Assert.That(() => UnitOfWork.ReferenceRaceDemographics.RemoveRange(removeReferenceRaceDemographics),
                Throws.InvalidOperationException);
        }
    }
}
