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
    public class ReferenceCountryRepositoryTests : BaseUnitTestProvider
    {
        /// <summary>
        /// Helper method to retrieve a country code, which is the primary key
        /// of the <see cref="ReferenceCountry"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceCountries"/> test collection
        /// </summary>
        private static string GetNotExistsCountryCode() =>
            AssertHelper.GetValueNotInArray(PreDefinedData.ReferenceCountries,
                "Code", 2);

        /// <summary>
        /// Helper method to retrieve a country code, which is the primary key
        /// of the <see cref="ReferenceCountry"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceCountries"/> test collection
        /// </summary>
        private static string GetRandomCountryCode() =>
            PreDefinedData.ReferenceCountries[
                new Random().Next(0, PreDefinedData.ReferenceCountries.Length)
            ].Code;

        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceCountries.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceCountries.Length));
        }

        [Test]
        public void Get_ValidCountryCode_ReturnsSingleCountry()
        {
            var randomCountryCode = GetRandomCountryCode();
            var result = UnitOfWork.ReferenceCountries.Get(randomCountryCode);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Code, Is.EqualTo(randomCountryCode));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidCountryCode_ReturnsNull(string invalidCountryCode)
        {
            var result = UnitOfWork.ReferenceCountries.Get(invalidCountryCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Find_PredicateUsedToFindOneCountry_ReturnsCollection()
        {
            var randomCountryCode = GetRandomCountryCode();
            Expression<Func<ReferenceCountry, bool>> predicate =
                (p => p.Code == randomCountryCode);
            var result = UnitOfWork.ReferenceCountries.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToList()[0].Code == randomCountryCode);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneCountry_ReturnsCollection()
        {
            var randomCountryCode = GetRandomCountryCode();
            Expression<Func<ReferenceCountry, bool>> predicate =
                (p => p.Code != randomCountryCode);
            var result = UnitOfWork.ReferenceCountries.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(
                PreDefinedData.ReferenceCountries.Length - 1));
        }

        [Test]
        public void Find_PredicateUsedToFindNoCountries_ReturnsEmptyCollection()
        {
            var notExistsCountryCode = GetNotExistsCountryCode();
            Expression<Func<ReferenceCountry, bool>> predicate =
                (p => p.Code == notExistsCountryCode);
            var result = UnitOfWork.ReferenceCountries.Find(predicate);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneCountry_ReturnsOneCountry()
        {
            var randomCountryCode = GetRandomCountryCode();
            Expression<Func<ReferenceCountry, bool>> predicate =
                (p => p.Code == randomCountryCode);
            var result = UnitOfWork.ReferenceCountries.SingleOrDefault(predicate);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneCountry_ThrowsInvalidOperationException()
        {
            var randomCountryCode = GetRandomCountryCode();
            Expression<Func<ReferenceCountry, bool>> predicate =
                (p => p.Code != randomCountryCode);

            Assert.That(() =>
                UnitOfWork.ReferenceCountries.SingleOrDefault(predicate),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoCountries_ReturnsNull()
        {
            var notExistsCountryCode = GetNotExistsCountryCode();
            Expression<Func<ReferenceCountry, bool>> predicate =
                (p => p.Code == notExistsCountryCode);
            var result = UnitOfWork.ReferenceCountries.SingleOrDefault(predicate);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidCountryNotExists_FetchNewCountry()
        {
            var notExistsCountryCode = GetNotExistsCountryCode();
            var newReferenceCountry = new ReferenceCountry()
            {
                Code = notExistsCountryCode,
                DisplayText = notExistsCountryCode
            };

            UnitOfWork.ReferenceCountries.Add(newReferenceCountry);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceCountries.Get(notExistsCountryCode);

            Assert.That(result, Is.Not.Null);
            AssertHelper.AreObjectsEqual(newReferenceCountry, result);
        }

        [Test]
        public void Add_ValidCountryExists_ThrowsInvalidOperationException()
        {
            var randomCountryCode = GetRandomCountryCode();
            Assert.That(() => UnitOfWork.ReferenceCountries.Add(
                new ReferenceCountry()
                {
                    Code = randomCountryCode
                }),
                Throws.InvalidOperationException);
        }

        [Test]
        public void Add_InvalidCountry_ThrowsDbUpdateException()
        {
            UnitOfWork.ReferenceCountries.Add(new ReferenceCountry());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void AddRange_TwoValidCountries_CountIncreasedByTwo()
        {
        Start:
            var notExistsCountryCode1 = GetNotExistsCountryCode();
            var notExistsCountryCode2 = GetNotExistsCountryCode();
            if (notExistsCountryCode1 == notExistsCountryCode2)
                goto Start;

            var newCountries = new Collection<ReferenceCountry>
            {
                new ReferenceCountry() { Code = notExistsCountryCode1, DisplayText = "" },
                new ReferenceCountry() { Code = notExistsCountryCode2, DisplayText = "" }
            };
            UnitOfWork.ReferenceCountries.AddRange(newCountries);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceCountries.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceCountries.Length + newCountries.Count));
        }

        [Test]
        public void AddRange_TwoValidCountriesDuplicated_ThrowsInvalidOperationException()
        {
            var notExistsCountryCode = GetNotExistsCountryCode();
            var newCountries = new Collection<ReferenceCountry>
            {
                new ReferenceCountry() { Code = notExistsCountryCode, DisplayText = "" },
                new ReferenceCountry() { Code = notExistsCountryCode, DisplayText = "" }
            };

            Assert.That(() => UnitOfWork.ReferenceCountries.AddRange(newCountries),
                Throws.InvalidOperationException);
        }

        [Test]
        public void AddRange_TwoMalformedCountries_ThrowsDbUpdateException()
        {
            var newCountries = new Collection<ReferenceCountry>
            {
                new ReferenceCountry(),
                new ReferenceCountry()
            };
            UnitOfWork.ReferenceCountries.AddRange(newCountries);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void Remove_ValidCountryNotExists_ThrowsDbUpdateConcurrencyException()
        {
            var notExistsCountryCode = GetNotExistsCountryCode();
            UnitOfWork.ReferenceCountries.Remove(
                new ReferenceCountry()
                {
                    Code = notExistsCountryCode
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidCountryExists_CountryCannotBeFetched()
        {
            var randomCountryCode = GetRandomCountryCode();
            var removeReferenceCountry = UnitOfWork.ReferenceCountries.Get(randomCountryCode);
            UnitOfWork.ReferenceCountries.Remove(removeReferenceCountry);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceCountries.Get(randomCountryCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidCountry_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceCountries.Remove(new ReferenceCountry());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceCountries = UnitOfWork.ReferenceCountries.GetAll().ToList();
            var removeReferenceCountries = new Collection<ReferenceCountry>();
            var removeCount = new Random().Next(1, referenceCountries.Count);

            for (int i = 0; i < removeCount; i++)
            {
                removeReferenceCountries.Add(referenceCountries.ElementAt(i));
            }

            UnitOfWork.ReferenceCountries.RemoveRange(removeReferenceCountries);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceCountries.GetAll();

            Assert.That(result.Count, Is.EqualTo(referenceCountries.Count - removeCount));
        }

        [Test]
        public void RemoveRange_TwoValidCountriesDuplicated_ThrowsInvalidOperationException()
        {
            var randomCountryCode = GetRandomCountryCode();
            var newCountries = new Collection<ReferenceCountry>
            {
                new ReferenceCountry() { Code = randomCountryCode },
                new ReferenceCountry() { Code = randomCountryCode }
            };

            Assert.That(() => UnitOfWork.ReferenceCountries.RemoveRange(newCountries),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedCountries_DbUpdateConcurrencyException()
        {
            var removeReferenceCountries = new Collection<ReferenceCountry>
            {
                new ReferenceCountry(),
                new ReferenceCountry()
            };

            UnitOfWork.ReferenceCountries.RemoveRange(removeReferenceCountries);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }
    }
}
