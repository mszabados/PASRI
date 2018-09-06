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
            var randomRaceDemographicCode = PreDefinedData.GetRandomRaceDemographicCode();
            var result = UnitOfWork.ReferenceRaceDemographics.Get(randomRaceDemographicCode);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Code, Is.EqualTo(randomRaceDemographicCode));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidRaceDemographicCode_ReturnsNull(string invalidRaceDemographicCode)
        {
            var result = UnitOfWork.ReferenceRaceDemographics.Get(invalidRaceDemographicCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Find_PredicateUsedToFindOneRaceDemographic_ReturnsCollection()
        {
            var randomRaceDemographicCode = PreDefinedData.GetRandomRaceDemographicCode();
            Expression<Func<ReferenceRaceDemographic, bool>> predicate =
                (p => p.Code == randomRaceDemographicCode);
            var result = UnitOfWork.ReferenceRaceDemographics.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToList()[0].Code == randomRaceDemographicCode);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneRaceDemographic_ReturnsCollection()
        {
            var randomRaceDemographicCode = PreDefinedData.GetRandomRaceDemographicCode();
            Expression<Func<ReferenceRaceDemographic, bool>> predicate =
                (p => p.Code != randomRaceDemographicCode);
            var result = UnitOfWork.ReferenceRaceDemographics.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(
                PreDefinedData.ReferenceRaceDemographics.Length - 1));
        }

        [Test]
        public void Find_PredicateUsedToFindNoRaceDemographics_ReturnsEmptyCollection()
        {
            var notExistsRaceDemographicCode = PreDefinedData.GetNotExistsRaceDemographicCode();
            Expression<Func<ReferenceRaceDemographic, bool>> predicate =
                (p => p.Code == notExistsRaceDemographicCode);
            var result = UnitOfWork.ReferenceRaceDemographics.Find(predicate);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneRaceDemographic_ReturnsOneRaceDemographic()
        {
            var randomRaceDemographicCode = PreDefinedData.GetRandomRaceDemographicCode();
            Expression<Func<ReferenceRaceDemographic, bool>> predicate =
                (p => p.Code == randomRaceDemographicCode);
            var result = UnitOfWork.ReferenceRaceDemographics.SingleOrDefault(predicate);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneRaceDemographic_ThrowsInvalidOperationException()
        {
            var randomRaceDemographicCode = PreDefinedData.GetRandomRaceDemographicCode();
            Expression<Func<ReferenceRaceDemographic, bool>> predicate =
                (p => p.Code != randomRaceDemographicCode);

            Assert.That(() =>
                UnitOfWork.ReferenceRaceDemographics.SingleOrDefault(predicate),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoRaceDemographics_ReturnsNull()
        {
            var notExistsRaceDemographicCode = PreDefinedData.GetNotExistsRaceDemographicCode();
            Expression<Func<ReferenceRaceDemographic, bool>> predicate =
                (p => p.Code == notExistsRaceDemographicCode);
            var result = UnitOfWork.ReferenceRaceDemographics.SingleOrDefault(predicate);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidRaceDemographicNotExists_FetchNewRaceDemographic()
        {
            var notExistsRaceDemographicCode = PreDefinedData.GetNotExistsRaceDemographicCode();
            var newReferenceRaceDemographic = new ReferenceRaceDemographic()
            {
                Code = notExistsRaceDemographicCode,
                DisplayText = notExistsRaceDemographicCode
            };

            UnitOfWork.ReferenceRaceDemographics.Add(newReferenceRaceDemographic);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceRaceDemographics.Get(notExistsRaceDemographicCode);

            Assert.That(result, Is.Not.Null);
            AssertHelper.AreObjectsEqual(newReferenceRaceDemographic, result);
        }

        [Test]
        public void Add_ValidRaceDemographicExists_ThrowsInvalidOperationException()
        {
            var randomRaceDemographicCode = PreDefinedData.GetRandomRaceDemographicCode();
            Assert.That(() => UnitOfWork.ReferenceRaceDemographics.Add(
                new ReferenceRaceDemographic()
                {
                    Code = randomRaceDemographicCode
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
                new ReferenceRaceDemographic() { Code = notExistsRaceDemographicCode1, DisplayText = "" },
                new ReferenceRaceDemographic() { Code = notExistsRaceDemographicCode2, DisplayText = "" }
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
                new ReferenceRaceDemographic() { Code = notExistsRaceDemographicCode, DisplayText = "" },
                new ReferenceRaceDemographic() { Code = notExistsRaceDemographicCode, DisplayText = "" }
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
            var notExistsRaceDemographicCode = PreDefinedData.GetNotExistsRaceDemographicCode();
            UnitOfWork.ReferenceRaceDemographics.Remove(
                new ReferenceRaceDemographic()
                {
                    Code = notExistsRaceDemographicCode
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidRaceDemographicExists_RaceDemographicCannotBeFetched()
        {
            var randomRaceDemographicCode = PreDefinedData.GetRandomRaceDemographicCode();
            var removeReferenceRaceDemographic = UnitOfWork.ReferenceRaceDemographics.Get(randomRaceDemographicCode);
            UnitOfWork.ReferenceRaceDemographics.Remove(removeReferenceRaceDemographic);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceRaceDemographics.Get(randomRaceDemographicCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidRaceDemographic_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceRaceDemographics.Remove(new ReferenceRaceDemographic());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceRaceDemographics = UnitOfWork.ReferenceRaceDemographics.GetAll().ToList();
            var removeReferenceRaceDemographics = new Collection<ReferenceRaceDemographic>();
            var removeCount = new Random().Next(1, referenceRaceDemographics.Count);

            for (int i = 0; i < removeCount; i++)
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
            var randomRaceDemographicCode = PreDefinedData.GetRandomRaceDemographicCode();
            var newRaceDemographics = new Collection<ReferenceRaceDemographic>
            {
                new ReferenceRaceDemographic() { Code = randomRaceDemographicCode },
                new ReferenceRaceDemographic() { Code = randomRaceDemographicCode }
            };

            Assert.That(() => UnitOfWork.ReferenceRaceDemographics.RemoveRange(newRaceDemographics),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedRaceDemographics_DbUpdateConcurrencyException()
        {
            var removeReferenceRaceDemographics = new Collection<ReferenceRaceDemographic>
            {
                new ReferenceRaceDemographic(),
                new ReferenceRaceDemographic()
            };

            UnitOfWork.ReferenceRaceDemographics.RemoveRange(removeReferenceRaceDemographics);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }
    }
}
