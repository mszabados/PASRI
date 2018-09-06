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
    public class ReferenceReligionDemographicRepositoryTests : BaseUnitTestProvider
    {
        /// <summary>
        /// Helper method to retrieve a religion demographic code, which is the primary key
        /// of the <see cref="ReferenceReligionDemographic"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceReligionDemographics"/> test collection
        /// </summary>
        private static string GetNotExistsReligionDemographicCode() =>
            AssertHelper.GetValueNotInArray(PreDefinedData.ReferenceReligionDemographics,
                "Code", 2);

        /// <summary>
        /// Helper method to retrieve a religion demographic code, which is the primary key
        /// of the <see cref="ReferenceReligionDemographic"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceReligionDemographics"/> test collection
        /// </summary>
        private static string GetRandomReligionDemographicCode() =>
            PreDefinedData.ReferenceReligionDemographics[
                new Random().Next(0, PreDefinedData.ReferenceReligionDemographics.Length)
            ].Code;

        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceReligionDemographics.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceReligionDemographics.Length));
        }

        [Test]
        public void Get_ValidReligionDemographicCode_ReturnsSingleReligionDemographic()
        {
            var randomReligionDemographicCode = GetRandomReligionDemographicCode();
            var result = UnitOfWork.ReferenceReligionDemographics.Get(randomReligionDemographicCode);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Code, Is.EqualTo(randomReligionDemographicCode));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidReligionDemographicCode_ReturnsNull(string invalidReligionDemographicCode)
        {
            var result = UnitOfWork.ReferenceReligionDemographics.Get(invalidReligionDemographicCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Find_PredicateUsedToFindOneReligionDemographic_ReturnsCollection()
        {
            var randomReligionDemographicCode = GetRandomReligionDemographicCode();
            Expression<Func<ReferenceReligionDemographic, bool>> predicate =
                (p => p.Code == randomReligionDemographicCode);
            var result = UnitOfWork.ReferenceReligionDemographics.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToList()[0].Code == randomReligionDemographicCode);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneReligionDemographic_ReturnsCollection()
        {
            var randomReligionDemographicCode = GetRandomReligionDemographicCode();
            Expression<Func<ReferenceReligionDemographic, bool>> predicate =
                (p => p.Code != randomReligionDemographicCode);
            var result = UnitOfWork.ReferenceReligionDemographics.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(
                PreDefinedData.ReferenceReligionDemographics.Length - 1));
        }

        [Test]
        public void Find_PredicateUsedToFindNoReligionDemographics_ReturnsEmptyCollection()
        {
            var notExistsReligionDemographicCode = GetNotExistsReligionDemographicCode();
            Expression<Func<ReferenceReligionDemographic, bool>> predicate =
                (p => p.Code == notExistsReligionDemographicCode);
            var result = UnitOfWork.ReferenceReligionDemographics.Find(predicate);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneReligionDemographic_ReturnsOneReligionDemographic()
        {
            var randomReligionDemographicCode = GetRandomReligionDemographicCode();
            Expression<Func<ReferenceReligionDemographic, bool>> predicate =
                (p => p.Code == randomReligionDemographicCode);
            var result = UnitOfWork.ReferenceReligionDemographics.SingleOrDefault(predicate);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneReligionDemographic_ThrowsInvalidOperationException()
        {
            var randomReligionDemographicCode = GetRandomReligionDemographicCode();
            Expression<Func<ReferenceReligionDemographic, bool>> predicate =
                (p => p.Code != randomReligionDemographicCode);

            Assert.That(() =>
                UnitOfWork.ReferenceReligionDemographics.SingleOrDefault(predicate),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoReligionDemographics_ReturnsNull()
        {
            var notExistsReligionDemographicCode = GetNotExistsReligionDemographicCode();
            Expression<Func<ReferenceReligionDemographic, bool>> predicate =
                (p => p.Code == notExistsReligionDemographicCode);
            var result = UnitOfWork.ReferenceReligionDemographics.SingleOrDefault(predicate);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidReligionDemographicNotExists_FetchNewReligionDemographic()
        {
            var notExistsReligionDemographicCode = GetNotExistsReligionDemographicCode();
            var newReferenceReligionDemographic = new ReferenceReligionDemographic()
            {
                Code = notExistsReligionDemographicCode,
                DisplayText = notExistsReligionDemographicCode
            };

            UnitOfWork.ReferenceReligionDemographics.Add(newReferenceReligionDemographic);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceReligionDemographics.Get(notExistsReligionDemographicCode);

            Assert.That(result, Is.Not.Null);
            AssertHelper.AreObjectsEqual(newReferenceReligionDemographic, result);
        }

        [Test]
        public void Add_ValidReligionDemographicExists_ThrowsInvalidOperationException()
        {
            var randomReligionDemographicCode = GetRandomReligionDemographicCode();
            Assert.That(() => UnitOfWork.ReferenceReligionDemographics.Add(
                new ReferenceReligionDemographic()
                {
                    Code = randomReligionDemographicCode
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
            var notExistsReligionDemographicCode1 = GetNotExistsReligionDemographicCode();
            var notExistsReligionDemographicCode2 = GetNotExistsReligionDemographicCode();
            if (notExistsReligionDemographicCode1 == notExistsReligionDemographicCode2)
                goto Start;

            var newReligionDemographics = new Collection<ReferenceReligionDemographic>
            {
                new ReferenceReligionDemographic() { Code = notExistsReligionDemographicCode1, DisplayText = "" },
                new ReferenceReligionDemographic() { Code = notExistsReligionDemographicCode2, DisplayText = "" }
            };
            UnitOfWork.ReferenceReligionDemographics.AddRange(newReligionDemographics);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceReligionDemographics.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceReligionDemographics.Length + newReligionDemographics.Count));
        }

        [Test]
        public void AddRange_TwoValidReligionDemographicsDuplicated_ThrowsInvalidOperationException()
        {
            var notExistsReligionDemographicCode = GetNotExistsReligionDemographicCode();
            var newReligionDemographics = new Collection<ReferenceReligionDemographic>
            {
                new ReferenceReligionDemographic() { Code = notExistsReligionDemographicCode, DisplayText = "" },
                new ReferenceReligionDemographic() { Code = notExistsReligionDemographicCode, DisplayText = "" }
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
            var notExistsReligionDemographicCode = GetNotExistsReligionDemographicCode();
            UnitOfWork.ReferenceReligionDemographics.Remove(
                new ReferenceReligionDemographic()
                {
                    Code = notExistsReligionDemographicCode
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidReligionDemographicExists_ReligionDemographicCannotBeFetched()
        {
            var randomReligionDemographicCode = GetRandomReligionDemographicCode();
            var removeReferenceReligionDemographic = UnitOfWork.ReferenceReligionDemographics.Get(randomReligionDemographicCode);
            UnitOfWork.ReferenceReligionDemographics.Remove(removeReferenceReligionDemographic);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceReligionDemographics.Get(randomReligionDemographicCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidReligionDemographic_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceReligionDemographics.Remove(new ReferenceReligionDemographic());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
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
            var randomReligionDemographicCode = GetRandomReligionDemographicCode();
            var newReligionDemographics = new Collection<ReferenceReligionDemographic>
            {
                new ReferenceReligionDemographic() { Code = randomReligionDemographicCode },
                new ReferenceReligionDemographic() { Code = randomReligionDemographicCode }
            };

            Assert.That(() => UnitOfWork.ReferenceReligionDemographics.RemoveRange(newReligionDemographics),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedReligionDemographics_DbUpdateConcurrencyException()
        {
            var removeReferenceReligionDemographics = new Collection<ReferenceReligionDemographic>
            {
                new ReferenceReligionDemographic(),
                new ReferenceReligionDemographic()
            };

            UnitOfWork.ReferenceReligionDemographics.RemoveRange(removeReferenceReligionDemographics);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }
    }
}
