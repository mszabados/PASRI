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
    public class ReferenceEthnicGroupDemographicRepositoryTests : BaseUnitTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceEthnicGroupDemographics.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceEthnicGroupDemographics.Length));
        }

        [Test]
        public void Get_ValidEthnicGroupDemographicCode_ReturnsSingleEthnicGroupDemographic()
        {
            var randomEthnicGroupDemographicId = PreDefinedData.GetRandomEthnicGroupDemographicId();

            var result = UnitOfWork.ReferenceEthnicGroupDemographics.Get(randomEthnicGroupDemographicId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(randomEthnicGroupDemographicId));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidEthnicGroupDemographicCode_ReturnsNull(string invalidEthnicGroupDemographicCode)
        {
            var result = UnitOfWork.ReferenceEthnicGroupDemographics.Find(p => p.Code == invalidEthnicGroupDemographicCode);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Find_PredicateUsedToFindOneEthnicGroupDemographic_ReturnsCollection()
        {
            var randomEthnicGroupDemographicId = PreDefinedData.GetRandomEthnicGroupDemographicId();

            var result = UnitOfWork.ReferenceEthnicGroupDemographics.Find(p => p.Id == randomEthnicGroupDemographicId);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ElementAt(0).Id == randomEthnicGroupDemographicId);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneEthnicGroupDemographic_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceEthnicGroupDemographics.Find(p => p.Id != int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Find_PredicateUsedToFindNoEthnicGroupDemographics_ReturnsEmptyCollection()
        {
            var result = UnitOfWork.ReferenceEthnicGroupDemographics.Find(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneEthnicGroupDemographic_ReturnsOneEthnicGroupDemographic()
        {
            var randomEthnicGroupDemographicId = PreDefinedData.GetRandomEthnicGroupDemographicId();

            var result = UnitOfWork.ReferenceEthnicGroupDemographics.SingleOrDefault(p => p.Id == randomEthnicGroupDemographicId);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneEthnicGroupDemographic_ThrowsInvalidOperationException()
        {
            var randomEthnicGroupDemographicId = PreDefinedData.GetRandomEthnicGroupDemographicId();

            Assert.That(() =>
                UnitOfWork.ReferenceEthnicGroupDemographics.SingleOrDefault(p => p.Id != randomEthnicGroupDemographicId),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoEthnicGroupDemographics_ReturnsNull()
        {
            var result = UnitOfWork.ReferenceEthnicGroupDemographics.SingleOrDefault(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidEthnicGroupDemographicNotExists_FetchNewEthnicGroupDemographic()
        {
            var notExistsEthnicGroupDemographicCode = PreDefinedData.GetNotExistsEthnicGroupDemographicCode();
            var newReferenceEthnicGroupDemographic = new ReferenceEthnicGroupDemographic
            {
                Code = notExistsEthnicGroupDemographicCode,
                LongName = notExistsEthnicGroupDemographicCode
            };

            UnitOfWork.ReferenceEthnicGroupDemographics.Add(newReferenceEthnicGroupDemographic);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceEthnicGroupDemographics.Get(newReferenceEthnicGroupDemographic.Id);

            Assert.That(result, Is.Not.Null);

            AssertHelper.AreObjectsEqual(newReferenceEthnicGroupDemographic, result);
        }

        [Test]
        public void Add_ValidEthnicGroupDemographicExists_ThrowsInvalidOperationException()
        {
            var randomEthnicGroupDemographicId = PreDefinedData.GetRandomEthnicGroupDemographicId();
            var randomEthnicGroupDemographic = UnitOfWork.ReferenceEthnicGroupDemographics.Get(randomEthnicGroupDemographicId);

            Assert.That(() => UnitOfWork.ReferenceEthnicGroupDemographics.Add(
                new ReferenceEthnicGroupDemographic
                {
                    Id = randomEthnicGroupDemographic.Id,
                    Code = randomEthnicGroupDemographic.Code
                }),
                Throws.InvalidOperationException);
        }

        [Test]
        public void Add_InvalidEthnicGroupDemographic_ThrowsDbUpdateException()
        {
            UnitOfWork.ReferenceEthnicGroupDemographics.Add(new ReferenceEthnicGroupDemographic());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void AddRange_TwoValidEthnicGroupDemographics_CountIncreasedByTwo()
        {
        Start:
            var notExistsEthnicGroupDemographicCode1 = PreDefinedData.GetNotExistsEthnicGroupDemographicCode();
            var notExistsEthnicGroupDemographicCode2 = PreDefinedData.GetNotExistsEthnicGroupDemographicCode();
            if (notExistsEthnicGroupDemographicCode1 == notExistsEthnicGroupDemographicCode2)
                goto Start;

            var newEthnicGroupDemographics = new Collection<ReferenceEthnicGroupDemographic>
            {
                new ReferenceEthnicGroupDemographic { Code = notExistsEthnicGroupDemographicCode1, LongName = "" },
                new ReferenceEthnicGroupDemographic { Code = notExistsEthnicGroupDemographicCode2, LongName = "" }
            };
            UnitOfWork.ReferenceEthnicGroupDemographics.AddRange(newEthnicGroupDemographics);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceEthnicGroupDemographics.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceEthnicGroupDemographics.Length + newEthnicGroupDemographics.Count));
        }

        [Test]
        public void AddRange_TwoValidEthnicGroupDemographicsDuplicated_ThrowsInvalidOperationException()
        {
            var notExistsEthnicGroupDemographicCode = PreDefinedData.GetNotExistsEthnicGroupDemographicCode();
            var newEthnicGroupDemographics = new Collection<ReferenceEthnicGroupDemographic>
            {
                new ReferenceEthnicGroupDemographic { Id = int.MaxValue, Code = notExistsEthnicGroupDemographicCode, LongName = "" },
                new ReferenceEthnicGroupDemographic { Id = int.MaxValue, Code = notExistsEthnicGroupDemographicCode, LongName = "" }
            };

            Assert.That(() => UnitOfWork.ReferenceEthnicGroupDemographics.AddRange(newEthnicGroupDemographics),
                Throws.InvalidOperationException);
        }

        [Test]
        public void AddRange_TwoMalformedEthnicGroupDemographics_ThrowsDbUpdateException()
        {
            var newEthnicGroupDemographics = new Collection<ReferenceEthnicGroupDemographic>
            {
                new ReferenceEthnicGroupDemographic(),
                new ReferenceEthnicGroupDemographic()
            };
            UnitOfWork.ReferenceEthnicGroupDemographics.AddRange(newEthnicGroupDemographics);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void Remove_ValidEthnicGroupDemographicNotExists_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceEthnicGroupDemographics.Remove(
                new ReferenceEthnicGroupDemographic
                {
                    Id = int.MaxValue
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidEthnicGroupDemographicExists_EthnicGroupDemographicCannotBeFetched()
        {
            var randomEthnicGroupDemographicId = PreDefinedData.GetRandomEthnicGroupDemographicId();
            var removeReferenceEthnicGroupDemographic = UnitOfWork.ReferenceEthnicGroupDemographics.Get(randomEthnicGroupDemographicId);
            UnitOfWork.ReferenceEthnicGroupDemographics.Remove(removeReferenceEthnicGroupDemographic);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceEthnicGroupDemographics.Get(removeReferenceEthnicGroupDemographic.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidEthnicGroupDemographic_ThrowsInvalidOperationException()
        {
            Assert.That(() => UnitOfWork.ReferenceEthnicGroupDemographics.Remove(new ReferenceEthnicGroupDemographic()),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceEthnicGroupDemographics = UnitOfWork.ReferenceEthnicGroupDemographics.GetAll().ToList();
            var removeReferenceEthnicGroupDemographics = new Collection<ReferenceEthnicGroupDemographic>();
            var removeCount = new Random().Next(1, referenceEthnicGroupDemographics.Count);

            for (var i = 0; i < removeCount; i++)
            {
                removeReferenceEthnicGroupDemographics.Add(referenceEthnicGroupDemographics.ElementAt(i));
            }

            UnitOfWork.ReferenceEthnicGroupDemographics.RemoveRange(removeReferenceEthnicGroupDemographics);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceEthnicGroupDemographics.GetAll();

            Assert.That(result.Count, Is.EqualTo(referenceEthnicGroupDemographics.Count - removeCount));
        }

        [Test]
        public void RemoveRange_TwoValidEthnicGroupDemographicsDuplicated_ThrowsInvalidOperationException()
        {
            var randomEthnicGroupDemographicId = PreDefinedData.GetRandomEthnicGroupDemographicId();
            var randomEthnicGroupDemographic = UnitOfWork.ReferenceEthnicGroupDemographics.Get(randomEthnicGroupDemographicId);

            var existingEthnicGroupDemographics = new Collection<ReferenceEthnicGroupDemographic>
            {
                new ReferenceEthnicGroupDemographic { Id = randomEthnicGroupDemographic.Id, Code = randomEthnicGroupDemographic.Code },
                new ReferenceEthnicGroupDemographic { Id = randomEthnicGroupDemographic.Id, Code = randomEthnicGroupDemographic.Code }
            };

            Assert.That(() => UnitOfWork.ReferenceEthnicGroupDemographics.RemoveRange(existingEthnicGroupDemographics),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedEthnicGroupDemographics_InvalidOperationException()
        {
            var removeReferenceEthnicGroupDemographics = new Collection<ReferenceEthnicGroupDemographic>
            {
                new ReferenceEthnicGroupDemographic(),
                new ReferenceEthnicGroupDemographic()
            };

            Assert.That(() => UnitOfWork.ReferenceEthnicGroupDemographics.RemoveRange(removeReferenceEthnicGroupDemographics),
                Throws.InvalidOperationException);
        }
    }
}
