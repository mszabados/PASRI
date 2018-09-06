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
    public class ReferenceEthnicGroupDemographicRepositoryTests : BaseUnitTestProvider
    {
        /// <summary>
        /// Helper method to retrieve a ethnic group demographic code, which is the primary key
        /// of the <see cref="ReferenceEthnicGroupDemographic"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceEthnicGroupDemographics"/> test collection
        /// </summary>
        private static string GetNotExistsEthnicGroupDemographicCode() =>
            AssertHelper.GetValueNotInArray(PreDefinedData.ReferenceEthnicGroupDemographics,
                "Code", 2);

        /// <summary>
        /// Helper method to retrieve a ethnic group demographic code, which is the primary key
        /// of the <see cref="ReferenceEthnicGroupDemographic"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceEthnicGroupDemographics"/> test collection
        /// </summary>
        private static string GetRandomEthnicGroupDemographicCode() =>
            PreDefinedData.ReferenceEthnicGroupDemographics[
                new Random().Next(0, PreDefinedData.ReferenceEthnicGroupDemographics.Length)
            ].Code;

        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceEthnicGroupDemographics.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceEthnicGroupDemographics.Length));
        }

        [Test]
        public void Get_ValidEthnicGroupDemographicCode_ReturnsSingleEthnicGroupDemographic()
        {
            var randomEthnicGroupDemographicCode = GetRandomEthnicGroupDemographicCode();
            var result = UnitOfWork.ReferenceEthnicGroupDemographics.Get(randomEthnicGroupDemographicCode);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Code, Is.EqualTo(randomEthnicGroupDemographicCode));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidEthnicGroupDemographicCode_ReturnsNull(string invalidEthnicGroupDemographicCode)
        {
            var result = UnitOfWork.ReferenceEthnicGroupDemographics.Get(invalidEthnicGroupDemographicCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Find_PredicateUsedToFindOneEthnicGroupDemographic_ReturnsCollection()
        {
            var randomEthnicGroupDemographicCode = GetRandomEthnicGroupDemographicCode();
            Expression<Func<ReferenceEthnicGroupDemographic, bool>> predicate =
                (p => p.Code == randomEthnicGroupDemographicCode);
            var result = UnitOfWork.ReferenceEthnicGroupDemographics.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToList()[0].Code == randomEthnicGroupDemographicCode);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneEthnicGroupDemographic_ReturnsCollection()
        {
            var randomEthnicGroupDemographicCode = GetRandomEthnicGroupDemographicCode();
            Expression<Func<ReferenceEthnicGroupDemographic, bool>> predicate =
                (p => p.Code != randomEthnicGroupDemographicCode);
            var result = UnitOfWork.ReferenceEthnicGroupDemographics.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(
                PreDefinedData.ReferenceEthnicGroupDemographics.Length - 1));
        }

        [Test]
        public void Find_PredicateUsedToFindNoEthnicGroupDemographics_ReturnsEmptyCollection()
        {
            var notExistsEthnicGroupDemographicCode = GetNotExistsEthnicGroupDemographicCode();
            Expression<Func<ReferenceEthnicGroupDemographic, bool>> predicate =
                (p => p.Code == notExistsEthnicGroupDemographicCode);
            var result = UnitOfWork.ReferenceEthnicGroupDemographics.Find(predicate);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneEthnicGroupDemographic_ReturnsOneEthnicGroupDemographic()
        {
            var randomEthnicGroupDemographicCode = GetRandomEthnicGroupDemographicCode();
            Expression<Func<ReferenceEthnicGroupDemographic, bool>> predicate =
                (p => p.Code == randomEthnicGroupDemographicCode);
            var result = UnitOfWork.ReferenceEthnicGroupDemographics.SingleOrDefault(predicate);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneEthnicGroupDemographic_ThrowsInvalidOperationException()
        {
            var randomEthnicGroupDemographicCode = GetRandomEthnicGroupDemographicCode();
            Expression<Func<ReferenceEthnicGroupDemographic, bool>> predicate =
                (p => p.Code != randomEthnicGroupDemographicCode);

            Assert.That(() =>
                UnitOfWork.ReferenceEthnicGroupDemographics.SingleOrDefault(predicate),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoEthnicGroupDemographics_ReturnsNull()
        {
            var notExistsEthnicGroupDemographicCode = GetNotExistsEthnicGroupDemographicCode();
            Expression<Func<ReferenceEthnicGroupDemographic, bool>> predicate =
                (p => p.Code == notExistsEthnicGroupDemographicCode);
            var result = UnitOfWork.ReferenceEthnicGroupDemographics.SingleOrDefault(predicate);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidEthnicGroupDemographicNotExists_FetchNewEthnicGroupDemographic()
        {
            var notExistsEthnicGroupDemographicCode = GetNotExistsEthnicGroupDemographicCode();
            var newReferenceEthnicGroupDemographic = new ReferenceEthnicGroupDemographic()
            {
                Code = notExistsEthnicGroupDemographicCode,
                DisplayText = notExistsEthnicGroupDemographicCode
            };

            UnitOfWork.ReferenceEthnicGroupDemographics.Add(newReferenceEthnicGroupDemographic);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceEthnicGroupDemographics.Get(notExistsEthnicGroupDemographicCode);

            Assert.That(result, Is.Not.Null);
            AssertHelper.AreObjectsEqual(newReferenceEthnicGroupDemographic, result);
        }

        [Test]
        public void Add_ValidEthnicGroupDemographicExists_ThrowsInvalidOperationException()
        {
            var randomEthnicGroupDemographicCode = GetRandomEthnicGroupDemographicCode();
            Assert.That(() => UnitOfWork.ReferenceEthnicGroupDemographics.Add(
                new ReferenceEthnicGroupDemographic()
                {
                    Code = randomEthnicGroupDemographicCode
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
            var notExistsEthnicGroupDemographicCode1 = GetNotExistsEthnicGroupDemographicCode();
            var notExistsEthnicGroupDemographicCode2 = GetNotExistsEthnicGroupDemographicCode();
            if (notExistsEthnicGroupDemographicCode1 == notExistsEthnicGroupDemographicCode2)
                goto Start;

            var newEthnicGroupDemographics = new Collection<ReferenceEthnicGroupDemographic>
            {
                new ReferenceEthnicGroupDemographic() { Code = notExistsEthnicGroupDemographicCode1, DisplayText = "" },
                new ReferenceEthnicGroupDemographic() { Code = notExistsEthnicGroupDemographicCode2, DisplayText = "" }
            };
            UnitOfWork.ReferenceEthnicGroupDemographics.AddRange(newEthnicGroupDemographics);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceEthnicGroupDemographics.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceEthnicGroupDemographics.Length + newEthnicGroupDemographics.Count));
        }

        [Test]
        public void AddRange_TwoValidEthnicGroupDemographicsDuplicated_ThrowsInvalidOperationException()
        {
            var notExistsEthnicGroupDemographicCode = GetNotExistsEthnicGroupDemographicCode();
            var newEthnicGroupDemographics = new Collection<ReferenceEthnicGroupDemographic>
            {
                new ReferenceEthnicGroupDemographic() { Code = notExistsEthnicGroupDemographicCode, DisplayText = "" },
                new ReferenceEthnicGroupDemographic() { Code = notExistsEthnicGroupDemographicCode, DisplayText = "" }
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
            var notExistsEthnicGroupDemographicCode = GetNotExistsEthnicGroupDemographicCode();
            UnitOfWork.ReferenceEthnicGroupDemographics.Remove(
                new ReferenceEthnicGroupDemographic()
                {
                    Code = notExistsEthnicGroupDemographicCode
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidEthnicGroupDemographicExists_EthnicGroupDemographicCannotBeFetched()
        {
            var randomEthnicGroupDemographicCode = GetRandomEthnicGroupDemographicCode();
            var removeReferenceEthnicGroupDemographic = UnitOfWork.ReferenceEthnicGroupDemographics.Get(randomEthnicGroupDemographicCode);
            UnitOfWork.ReferenceEthnicGroupDemographics.Remove(removeReferenceEthnicGroupDemographic);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceEthnicGroupDemographics.Get(randomEthnicGroupDemographicCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidEthnicGroupDemographic_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceEthnicGroupDemographics.Remove(new ReferenceEthnicGroupDemographic());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceEthnicGroupDemographics = UnitOfWork.ReferenceEthnicGroupDemographics.GetAll().ToList();
            var removeReferenceEthnicGroupDemographics = new Collection<ReferenceEthnicGroupDemographic>();
            var removeCount = new Random().Next(1, referenceEthnicGroupDemographics.Count);

            for (int i = 0; i < removeCount; i++)
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
            var randomEthnicGroupDemographicCode = GetRandomEthnicGroupDemographicCode();
            var newEthnicGroupDemographics = new Collection<ReferenceEthnicGroupDemographic>
            {
                new ReferenceEthnicGroupDemographic() { Code = randomEthnicGroupDemographicCode },
                new ReferenceEthnicGroupDemographic() { Code = randomEthnicGroupDemographicCode }
            };

            Assert.That(() => UnitOfWork.ReferenceEthnicGroupDemographics.RemoveRange(newEthnicGroupDemographics),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedEthnicGroupDemographics_DbUpdateConcurrencyException()
        {
            var removeReferenceEthnicGroupDemographics = new Collection<ReferenceEthnicGroupDemographic>
            {
                new ReferenceEthnicGroupDemographic(),
                new ReferenceEthnicGroupDemographic()
            };

            UnitOfWork.ReferenceEthnicGroupDemographics.RemoveRange(removeReferenceEthnicGroupDemographics);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }
    }
}
