using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PASRI.API.Core.Domain;
using PASRI.API.TestHelper;

namespace PASRI.API.UnitTests.Repositories
{
    [TestFixture]
    public class ReferenceCountryRepositoryTests : BaseUnitTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceCountries.GetAll();
            
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void Get_ValidCountryCode_ReturnsSingleCountry()
        {
            var validCountryCode = "US";
            var result = UnitOfWork.ReferenceCountries.Get(validCountryCode);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Code, Is.EqualTo(validCountryCode));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("ZZ")]
        public void Get_InvalidCountryCode_ReturnsNull(string invalidCountryCode)
        {
            var result = UnitOfWork.ReferenceCountries.Get(invalidCountryCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Find_PredicateUsedToFindOneCountry_ReturnsCollection()
        {
            Expression<Func<ReferenceCountry, bool>> predicate =
                (p => p.StartDate <= new DateTime(1800, 1, 1));
            var result = UnitOfWork.ReferenceCountries.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneCountry_ReturnsCollection()
        {
            Expression<Func<ReferenceCountry, bool>> predicate =
                (p => p.StartDate == null);
            var result = UnitOfWork.ReferenceCountries.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void Find_PredicateUsedToFindNoCountries_ReturnsEmptyCollection()
        {
            Expression<Func<ReferenceCountry, bool>> predicate =
                (p => p.StartDate > new DateTime(1800, 1, 1));
            var result = UnitOfWork.ReferenceCountries.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneCountry_ReturnsOneCountry()
        {
            Expression<Func<ReferenceCountry, bool>> predicate =
                (p => p.StartDate <= new DateTime(1800, 1, 1));
            var result = UnitOfWork.ReferenceCountries.SingleOrDefault(predicate);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneCountry_ThrowsInvalidOperationException()
        {
            Expression<Func<ReferenceCountry, bool>> predicate =
                (p => p.StartDate == null);

            Assert.That(() => 
                UnitOfWork.ReferenceCountries.SingleOrDefault(predicate), 
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoCountries_ReturnsNull()
        {
            Expression<Func<ReferenceCountry, bool>> predicate =
                (p => p.StartDate > new DateTime(1800, 1, 1));
            var result = UnitOfWork.ReferenceCountries.SingleOrDefault(predicate);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidCountryNotExists_FetchNewCountry()
        {
            string testCountryCode = "ZZ";
            var newReferenceCountry = new ReferenceCountry()
            {
                Code = testCountryCode,
                DisplayText = testCountryCode
            };

            UnitOfWork.ReferenceCountries.Add(newReferenceCountry);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceCountries.Get(testCountryCode);

            Assert.That(result, Is.Not.Null);
            AssertPropertyValuesAreEqual(newReferenceCountry, result);
        }

        [Test]
        public void Add_ValidCountryExists_ThrowsInvalidOperationException()
        {
            Assert.That(() => UnitOfWork.ReferenceCountries.Add(
                new ReferenceCountry()
                {
                    Code = "US",
                    DisplayText = ""
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
        public void AddRange_ValidCountries_CountIncreasedByTwo()
        {
            var newCountries = new Collection<ReferenceCountry>
            {
                new ReferenceCountry() { Code = "ZZ", DisplayText = "" },
                new ReferenceCountry() { Code = "ZY", DisplayText = "" }
            };
            UnitOfWork.ReferenceCountries.AddRange(newCountries);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceCountries.GetAll();

            Assert.That(result.Count, Is.EqualTo(5));
        }

        [Test]
        public void AddRange_ValidCountriesDuplicated_ThrowsInvalidOperationException()
        {
            var newCountries = new Collection<ReferenceCountry>
            {
                new ReferenceCountry() { Code = "ZZ", DisplayText = "" },
                new ReferenceCountry() { Code = "ZZ", DisplayText = "" }
            };

            Assert.That(() => UnitOfWork.ReferenceCountries.AddRange(newCountries),
                Throws.InvalidOperationException);
        }

        [Test]
        public void AddRange_InvalidCountries_ThrowsDbUpdateException()
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
            string testCountryCode = "ZZ";
            UnitOfWork.ReferenceCountries.Remove(
                new ReferenceCountry() {
                    Code = testCountryCode,
                    DisplayText = testCountryCode
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidCountryExists_CountryCannotBeFetched()
        {
            string validCountryCode = "US";
            var removeReferenceCountry = UnitOfWork.ReferenceCountries.Get(validCountryCode);
            UnitOfWork.ReferenceCountries.Remove(removeReferenceCountry);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceCountries.Get(validCountryCode);

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
        public void RemoveRange_TwoValidCountries_OneCountryRemains()
        {
            var referenceCountries = UnitOfWork.ReferenceCountries.GetAll().ToList();
            var removeReferenceCountries = new Collection<ReferenceCountry>();
            var removeCount = 2;
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
        public void RemoveRange_ValidCountriesDuplicated_ThrowsInvalidOperationException()
        {
            var newCountries = new Collection<ReferenceCountry>
            {
                new ReferenceCountry() { Code = "US", DisplayText = "" },
                new ReferenceCountry() { Code = "US", DisplayText = "" }
            };

            Assert.That(() => UnitOfWork.ReferenceCountries.RemoveRange(newCountries),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_InvalidCountries_DbUpdateConcurrencyException()
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
