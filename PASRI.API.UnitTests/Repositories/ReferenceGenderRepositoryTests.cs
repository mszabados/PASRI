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
    public class ReferenceGenderRepositoryTests : BaseUnitTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceGenders.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceGenders.Length));
        }

        [Test]
        public void Get_ValidGenderCode_ReturnsSingleGender()
        {
            var randomGenderId = PreDefinedData.GetRandomGenderId();

            var result = UnitOfWork.ReferenceGenders.Get(randomGenderId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(randomGenderId));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidGenderCode_ReturnsNull(string invalidGenderCode)
        {
            var result = UnitOfWork.ReferenceGenders.Find(p => p.Code == invalidGenderCode);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Find_PredicateUsedToFindOneGender_ReturnsCollection()
        {
            var randomGenderId = PreDefinedData.GetRandomGenderId();

            var result = UnitOfWork.ReferenceGenders.Find(p => p.Id == randomGenderId);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id == randomGenderId);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneGender_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceGenders.Find(p => p.Id != int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Find_PredicateUsedToFindNoGenders_ReturnsEmptyCollection()
        {
            var result = UnitOfWork.ReferenceGenders.Find(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneGender_ReturnsOneGender()
        {
            var randomGenderId = PreDefinedData.GetRandomGenderId();

            var result = UnitOfWork.ReferenceGenders.SingleOrDefault(p => p.Id == randomGenderId);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneGender_ThrowsInvalidOperationException()
        {
            var randomGenderId = PreDefinedData.GetRandomGenderId();

            Assert.That(() =>
                UnitOfWork.ReferenceGenders.SingleOrDefault(p => p.Id != randomGenderId),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoGenders_ReturnsNull()
        {
            var result = UnitOfWork.ReferenceGenders.SingleOrDefault(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidGenderNotExists_FetchNewGender()
        {
            var notExistsGenderCode = PreDefinedData.GetNotExistsGenderCode();
            var newReferenceGender = new ReferenceGender
            {
                Code = notExistsGenderCode,
                LongName = notExistsGenderCode
            };

            UnitOfWork.ReferenceGenders.Add(newReferenceGender);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceGenders.Get(newReferenceGender.Id);

            Assert.That(result, Is.Not.Null);

            AssertHelper.AreObjectsEqual(newReferenceGender, result);
        }

        [Test]
        public void Add_ValidGenderExists_ThrowsInvalidOperationException()
        {
            var randomGenderId = PreDefinedData.GetRandomGenderId();
            var randomGender = UnitOfWork.ReferenceGenders.Get(randomGenderId);

            Assert.That(() => UnitOfWork.ReferenceGenders.Add(
                new ReferenceGender
                {
                    Id = randomGender.Id,
                    Code = randomGender.Code
                }),
                Throws.InvalidOperationException);
        }

        [Test]
        public void Add_InvalidGender_ThrowsDbUpdateException()
        {
            UnitOfWork.ReferenceGenders.Add(new ReferenceGender());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void AddRange_TwoValidGenders_CountIncreasedByTwo()
        {
        Start:
            var notExistsGenderCode1 = PreDefinedData.GetNotExistsGenderCode();
            var notExistsGenderCode2 = PreDefinedData.GetNotExistsGenderCode();
            if (notExistsGenderCode1 == notExistsGenderCode2)
                goto Start;

            var newGenders = new Collection<ReferenceGender>
            {
                new ReferenceGender { Code = notExistsGenderCode1, LongName = "" },
                new ReferenceGender { Code = notExistsGenderCode2, LongName = "" }
            };
            UnitOfWork.ReferenceGenders.AddRange(newGenders);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceGenders.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceGenders.Length + newGenders.Count));
        }

        [Test]
        public void AddRange_TwoValidGendersDuplicated_ThrowsInvalidOperationException()
        {
            var notExistsGenderCode = PreDefinedData.GetNotExistsGenderCode();
            var newGenders = new Collection<ReferenceGender>
            {
                new ReferenceGender { Id = int.MaxValue, Code = notExistsGenderCode, LongName = "" },
                new ReferenceGender { Id = int.MaxValue, Code = notExistsGenderCode, LongName = "" }
            };

            Assert.That(() => UnitOfWork.ReferenceGenders.AddRange(newGenders),
                Throws.InvalidOperationException);
        }

        [Test]
        public void AddRange_TwoMalformedGenders_ThrowsDbUpdateException()
        {
            var newGenders = new Collection<ReferenceGender>
            {
                new ReferenceGender(),
                new ReferenceGender()
            };
            UnitOfWork.ReferenceGenders.AddRange(newGenders);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void Remove_ValidGenderNotExists_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceGenders.Remove(
                new ReferenceGender
                {
                    Id = int.MaxValue
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidGenderExists_GenderCannotBeFetched()
        {
            var randomGenderId = PreDefinedData.GetRandomGenderId();
            var removeReferenceGender = UnitOfWork.ReferenceGenders.Get(randomGenderId);
            UnitOfWork.ReferenceGenders.Remove(removeReferenceGender);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceGenders.Get(removeReferenceGender.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidGender_ThrowsInvalidOperationException()
        {
            Assert.That(() => UnitOfWork.ReferenceGenders.Remove(new ReferenceGender()),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceGenders = UnitOfWork.ReferenceGenders.GetAll().ToList();
            var removeReferenceGenders = new Collection<ReferenceGender>();
            var removeCount = new Random().Next(1, referenceGenders.Count);

            for (var i = 0; i < removeCount; i++)
            {
                removeReferenceGenders.Add(referenceGenders.ElementAt(i));
            }

            UnitOfWork.ReferenceGenders.RemoveRange(removeReferenceGenders);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceGenders.GetAll();

            Assert.That(result.Count, Is.EqualTo(referenceGenders.Count - removeCount));
        }

        [Test]
        public void RemoveRange_TwoValidGendersDuplicated_ThrowsInvalidOperationException()
        {
            var randomGenderId = PreDefinedData.GetRandomGenderId();
            var randomGender = UnitOfWork.ReferenceGenders.Get(randomGenderId);

            var existingGenders = new Collection<ReferenceGender>
            {
                new ReferenceGender { Id = randomGender.Id, Code = randomGender.Code },
                new ReferenceGender { Id = randomGender.Id, Code = randomGender.Code }
            };

            Assert.That(() => UnitOfWork.ReferenceGenders.RemoveRange(existingGenders),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedGenders_InvalidOperationException()
        {
            var removeReferenceGenders = new Collection<ReferenceGender>
            {
                new ReferenceGender(),
                new ReferenceGender()
            };

            Assert.That(() => UnitOfWork.ReferenceGenders.RemoveRange(removeReferenceGenders),
                Throws.InvalidOperationException);
        }
    }
}
