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
    public class ReferenceCountryRepositoryTests : BaseUnitTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceCountries.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceCountries.Length));
        }

        [Test]
        public void Get_ValidCountryCode_ReturnsSingleCountry()
        {
            var randomCountryId = PreDefinedData.GetRandomCountryId();

            var result = UnitOfWork.ReferenceCountries.Get(randomCountryId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(randomCountryId));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidCountryCode_ReturnsNull(string invalidCountryCode)
        {
            var result = UnitOfWork.ReferenceCountries.Find(p => p.Code == invalidCountryCode);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Find_PredicateUsedToFindOneCountry_ReturnsCollection()
        {
            var randomCountryId = PreDefinedData.GetRandomCountryId();

            var result = UnitOfWork.ReferenceCountries.Find(p => p.Id == randomCountryId);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id == randomCountryId);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneCountry_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceCountries.Find(p => p.Id != int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Find_PredicateUsedToFindNoCountries_ReturnsEmptyCollection()
        {
            var result = UnitOfWork.ReferenceCountries.Find(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneCountry_ReturnsOneCountry()
        {
            var randomCountryId = PreDefinedData.GetRandomCountryId();

            var result = UnitOfWork.ReferenceCountries.SingleOrDefault(p => p.Id == randomCountryId);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneCountry_ThrowsInvalidOperationException()
        {
            var randomCountryId = PreDefinedData.GetRandomCountryId();

            Assert.That(() =>
                UnitOfWork.ReferenceCountries.SingleOrDefault(p => p.Id != randomCountryId),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoCountries_ReturnsNull()
        {
            var result = UnitOfWork.ReferenceCountries.SingleOrDefault(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidCountryNotExists_FetchNewCountry()
        {
            var notExistsCountryCode = PreDefinedData.GetNotExistsCountryCode();
            var newReferenceCountry = new ReferenceCountry
            {
                Code = notExistsCountryCode,
                LongName = notExistsCountryCode
            };

            UnitOfWork.ReferenceCountries.Add(newReferenceCountry);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceCountries.Get(newReferenceCountry.Id);

            Assert.That(result, Is.Not.Null);

            AssertHelper.AreObjectsEqual(newReferenceCountry, result);
        }

        [Test]
        public void Add_ValidCountryExists_ThrowsInvalidOperationException()
        {
            var randomCountryId = PreDefinedData.GetRandomCountryId();
            var randomCountry = UnitOfWork.ReferenceCountries.Get(randomCountryId);

            Assert.That(() => UnitOfWork.ReferenceCountries.Add(
                new ReferenceCountry
                {
                    Id = randomCountry.Id,
                    Code = randomCountry.Code
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
            var notExistsCountryCode1 = PreDefinedData.GetNotExistsCountryCode();
            var notExistsCountryCode2 = PreDefinedData.GetNotExistsCountryCode();
            if (notExistsCountryCode1 == notExistsCountryCode2)
                goto Start;

            var newCountries = new Collection<ReferenceCountry>
            {
                new ReferenceCountry { Code = notExistsCountryCode1, LongName = "" },
                new ReferenceCountry { Code = notExistsCountryCode2, LongName = "" }
            };
            UnitOfWork.ReferenceCountries.AddRange(newCountries);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceCountries.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceCountries.Length + newCountries.Count));
        }

        [Test]
        public void AddRange_TwoValidCountriesDuplicated_ThrowsInvalidOperationException()
        {
            var notExistsCountryCode = PreDefinedData.GetNotExistsCountryCode();
            var newCountries = new Collection<ReferenceCountry>
            {
                new ReferenceCountry { Id = int.MaxValue, Code = notExistsCountryCode, LongName = "" },
                new ReferenceCountry { Id = int.MaxValue, Code = notExistsCountryCode, LongName = "" }
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
            UnitOfWork.ReferenceCountries.Remove(
                new ReferenceCountry
                {
                    Id = int.MaxValue
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidCountryExists_CountryCannotBeFetched()
        {
            var randomCountryId = PreDefinedData.GetRandomCountryId();
            var removeReferenceCountry = UnitOfWork.ReferenceCountries.Get(randomCountryId);
            UnitOfWork.ReferenceCountries.Remove(removeReferenceCountry);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceCountries.Get(removeReferenceCountry.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidCountry_ThrowsInvalidOperationException()
        {
            Assert.That(() => UnitOfWork.ReferenceCountries.Remove(new ReferenceCountry()), 
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceCountries = UnitOfWork.ReferenceCountries.GetAll().ToList();
            var removeReferenceCountries = new Collection<ReferenceCountry>();
            var removeCount = new Random().Next(1, referenceCountries.Count);

            for (var i = 0; i < removeCount; i++)
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
            var randomCountryId = PreDefinedData.GetRandomCountryId();
            var randomCountry = UnitOfWork.ReferenceCountries.Get(randomCountryId);

            var existingCountries = new Collection<ReferenceCountry>
            {
                new ReferenceCountry { Id = randomCountry.Id, Code = randomCountry.Code },
                new ReferenceCountry { Id = randomCountry.Id, Code = randomCountry.Code }
            };

            Assert.That(() => UnitOfWork.ReferenceCountries.RemoveRange(existingCountries),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedCountries_InvalidOperationException()
        {
            var removeReferenceCountries = new Collection<ReferenceCountry>
            {
                new ReferenceCountry(),
                new ReferenceCountry()
            };

            Assert.That(() => UnitOfWork.ReferenceCountries.RemoveRange(removeReferenceCountries), 
                Throws.InvalidOperationException);
        }
    }
}
