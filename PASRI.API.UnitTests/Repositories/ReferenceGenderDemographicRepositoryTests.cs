using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PASRI.API.Core.Domain;
using PASRI.API.TestHelper;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

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
            var randomGenderDemographicCode = PreDefinedData.GetRandomGenderDemographicCode();
            var result = UnitOfWork.ReferenceGenderDemographics.Get(randomGenderDemographicCode);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Code, Is.EqualTo(randomGenderDemographicCode));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidGenderDemographicCode_ReturnsNull(string invalidGenderDemographicCode)
        {
            var result = UnitOfWork.ReferenceGenderDemographics.Get(invalidGenderDemographicCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Find_PredicateUsedToFindOneGenderDemographic_ReturnsCollection()
        {
            var randomGenderDemographicCode = PreDefinedData.GetRandomGenderDemographicCode();
            Expression<Func<ReferenceGenderDemographic, bool>> predicate =
                (p => p.Code == randomGenderDemographicCode);
            var result = UnitOfWork.ReferenceGenderDemographics.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToList()[0].Code == randomGenderDemographicCode);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneGenderDemographic_ReturnsCollection()
        {
            var randomGenderDemographicCode = PreDefinedData.GetRandomGenderDemographicCode();
            Expression<Func<ReferenceGenderDemographic, bool>> predicate =
                (p => p.Code != randomGenderDemographicCode);
            var result = UnitOfWork.ReferenceGenderDemographics.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(
                PreDefinedData.ReferenceGenderDemographics.Length - 1));
        }

        [Test]
        public void Find_PredicateUsedToFindNoGenderDemographics_ReturnsEmptyCollection()
        {
            var notExistsGenderDemographicCode = PreDefinedData.GetNotExistsGenderDemographicCode();
            Expression<Func<ReferenceGenderDemographic, bool>> predicate =
                (p => p.Code == notExistsGenderDemographicCode);
            var result = UnitOfWork.ReferenceGenderDemographics.Find(predicate);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneGenderDemographic_ReturnsOneGenderDemographic()
        {
            var randomGenderDemographicCode = PreDefinedData.GetRandomGenderDemographicCode();
            Expression<Func<ReferenceGenderDemographic, bool>> predicate =
                (p => p.Code == randomGenderDemographicCode);
            var result = UnitOfWork.ReferenceGenderDemographics.SingleOrDefault(predicate);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneGenderDemographic_ThrowsInvalidOperationException()
        {
            var randomGenderDemographicCode = PreDefinedData.GetRandomGenderDemographicCode();
            Expression<Func<ReferenceGenderDemographic, bool>> predicate =
                (p => p.Code != randomGenderDemographicCode);

            Assert.That(() =>
                UnitOfWork.ReferenceGenderDemographics.SingleOrDefault(predicate),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoGenderDemographics_ReturnsNull()
        {
            var notExistsGenderDemographicCode = PreDefinedData.GetNotExistsGenderDemographicCode();
            Expression<Func<ReferenceGenderDemographic, bool>> predicate =
                (p => p.Code == notExistsGenderDemographicCode);
            var result = UnitOfWork.ReferenceGenderDemographics.SingleOrDefault(predicate);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidGenderDemographicNotExists_FetchNewGenderDemographic()
        {
            var notExistsGenderDemographicCode = PreDefinedData.GetNotExistsGenderDemographicCode();
            var newReferenceGenderDemographic = new ReferenceGenderDemographic()
            {
                Code = notExistsGenderDemographicCode,
                DisplayText = notExistsGenderDemographicCode
            };

            UnitOfWork.ReferenceGenderDemographics.Add(newReferenceGenderDemographic);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceGenderDemographics.Get(notExistsGenderDemographicCode);

            Assert.That(result, Is.Not.Null);
            AssertHelper.AreObjectsEqual(newReferenceGenderDemographic, result);
        }

        [Test]
        public void Add_ValidGenderDemographicExists_ThrowsInvalidOperationException()
        {
            var randomGenderDemographicCode = PreDefinedData.GetRandomGenderDemographicCode();
            Assert.That(() => UnitOfWork.ReferenceGenderDemographics.Add(
                new ReferenceGenderDemographic()
                {
                    Code = randomGenderDemographicCode
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
                new ReferenceGenderDemographic() { Code = notExistsGenderDemographicCode1, DisplayText = "" },
                new ReferenceGenderDemographic() { Code = notExistsGenderDemographicCode2, DisplayText = "" }
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
                new ReferenceGenderDemographic() { Code = notExistsGenderDemographicCode, DisplayText = "" },
                new ReferenceGenderDemographic() { Code = notExistsGenderDemographicCode, DisplayText = "" }
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
            var notExistsGenderDemographicCode = PreDefinedData.GetNotExistsGenderDemographicCode();
            UnitOfWork.ReferenceGenderDemographics.Remove(
                new ReferenceGenderDemographic()
                {
                    Code = notExistsGenderDemographicCode
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidGenderDemographicExists_GenderDemographicCannotBeFetched()
        {
            var randomGenderDemographicCode = PreDefinedData.GetRandomGenderDemographicCode();
            var removeReferenceGenderDemographic = UnitOfWork.ReferenceGenderDemographics.Get(randomGenderDemographicCode);
            UnitOfWork.ReferenceGenderDemographics.Remove(removeReferenceGenderDemographic);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceGenderDemographics.Get(randomGenderDemographicCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidGenderDemographic_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceGenderDemographics.Remove(new ReferenceGenderDemographic());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceGenderDemographics = UnitOfWork.ReferenceGenderDemographics.GetAll().ToList();
            var removeReferenceGenderDemographics = new Collection<ReferenceGenderDemographic>();
            var removeCount = new Random().Next(1, referenceGenderDemographics.Count);

            for (int i = 0; i < removeCount; i++)
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
            var randomGenderDemographicCode = PreDefinedData.GetRandomGenderDemographicCode();
            var newGenderDemographics = new Collection<ReferenceGenderDemographic>
            {
                new ReferenceGenderDemographic() { Code = randomGenderDemographicCode },
                new ReferenceGenderDemographic() { Code = randomGenderDemographicCode }
            };

            Assert.That(() => UnitOfWork.ReferenceGenderDemographics.RemoveRange(newGenderDemographics),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedGenderDemographics_DbUpdateConcurrencyException()
        {
            var removeReferenceGenderDemographics = new Collection<ReferenceGenderDemographic>
            {
                new ReferenceGenderDemographic(),
                new ReferenceGenderDemographic()
            };

            UnitOfWork.ReferenceGenderDemographics.RemoveRange(removeReferenceGenderDemographics);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }
    }
}
