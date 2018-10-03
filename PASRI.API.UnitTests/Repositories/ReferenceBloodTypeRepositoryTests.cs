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
    public class ReferenceBloodTypeRepositoryTests : BaseUnitTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceBloodTypes.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceBloodTypes.Length));
        }

        [Test]
        public void Get_ValidBloodTypeCode_ReturnsSingleBloodType()
        {
            var randomBloodTypeId = PreDefinedData.GetRandomBloodTypeId();

            var result = UnitOfWork.ReferenceBloodTypes.Get(randomBloodTypeId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(randomBloodTypeId));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidBloodTypeCode_ReturnsNull(string invalidBloodTypeCode)
        {
            var result = UnitOfWork.ReferenceBloodTypes.Find(p => p.Code == invalidBloodTypeCode);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Find_PredicateUsedToFindOneBloodType_ReturnsCollection()
        {
            var randomBloodTypeId = PreDefinedData.GetRandomBloodTypeId();

            var result = UnitOfWork.ReferenceBloodTypes.Find(p => p.Id == randomBloodTypeId);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ElementAt(0).Id == randomBloodTypeId);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneBloodType_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceBloodTypes.Find(p => p.Id != int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Find_PredicateUsedToFindNoBloodTypes_ReturnsEmptyCollection()
        {
            var result = UnitOfWork.ReferenceBloodTypes.Find(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneBloodType_ReturnsOneBloodType()
        {
            var randomBloodTypeId = PreDefinedData.GetRandomBloodTypeId();

            var result = UnitOfWork.ReferenceBloodTypes.SingleOrDefault(p => p.Id == randomBloodTypeId);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneBloodType_ThrowsInvalidOperationException()
        {
            var randomBloodTypeId = PreDefinedData.GetRandomBloodTypeId();

            Assert.That(() =>
                UnitOfWork.ReferenceBloodTypes.SingleOrDefault(p => p.Id != randomBloodTypeId),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoBloodTypes_ReturnsNull()
        {
            var result = UnitOfWork.ReferenceBloodTypes.SingleOrDefault(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidBloodTypeNotExists_FetchNewBloodType()
        {
            var notExistsBloodTypeCode = PreDefinedData.GetNotExistsBloodTypeCode();
            var newReferenceBloodType = new ReferenceBloodType
            {
                Code = notExistsBloodTypeCode,
                LongName = notExistsBloodTypeCode
            };

            UnitOfWork.ReferenceBloodTypes.Add(newReferenceBloodType);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceBloodTypes.Get(newReferenceBloodType.Id);

            Assert.That(result, Is.Not.Null);

            AssertHelper.AreObjectsEqual(newReferenceBloodType, result);
        }

        [Test]
        public void Add_ValidBloodTypeExists_ThrowsInvalidOperationException()
        {
            var randomBloodTypeId = PreDefinedData.GetRandomBloodTypeId();
            var randomBloodType = UnitOfWork.ReferenceBloodTypes.Get(randomBloodTypeId);

            Assert.That(() => UnitOfWork.ReferenceBloodTypes.Add(
                new ReferenceBloodType
                {
                    Id = randomBloodType.Id,
                    Code = randomBloodType.Code
                }),
                Throws.InvalidOperationException);
        }

        [Test]
        public void Add_InvalidBloodType_ThrowsDbUpdateException()
        {
            UnitOfWork.ReferenceBloodTypes.Add(new ReferenceBloodType());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void AddRange_TwoValidBloodTypes_CountIncreasedByTwo()
        {
        Start:
            var notExistsBloodTypeCode1 = PreDefinedData.GetNotExistsBloodTypeCode();
            var notExistsBloodTypeCode2 = PreDefinedData.GetNotExistsBloodTypeCode();
            if (notExistsBloodTypeCode1 == notExistsBloodTypeCode2)
                goto Start;

            var newBloodTypes = new Collection<ReferenceBloodType>
            {
                new ReferenceBloodType { Code = notExistsBloodTypeCode1, LongName = "" },
                new ReferenceBloodType { Code = notExistsBloodTypeCode2, LongName = "" }
            };
            UnitOfWork.ReferenceBloodTypes.AddRange(newBloodTypes);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceBloodTypes.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceBloodTypes.Length + newBloodTypes.Count));
        }

        [Test]
        public void AddRange_TwoValidBloodTypesDuplicated_ThrowsInvalidOperationException()
        {
            var notExistsBloodTypeCode = PreDefinedData.GetNotExistsBloodTypeCode();
            var newBloodTypes = new Collection<ReferenceBloodType>
            {
                new ReferenceBloodType { Id = int.MaxValue, Code = notExistsBloodTypeCode, LongName = "" },
                new ReferenceBloodType { Id = int.MaxValue, Code = notExistsBloodTypeCode, LongName = "" }
            };

            Assert.That(() => UnitOfWork.ReferenceBloodTypes.AddRange(newBloodTypes),
                Throws.InvalidOperationException);
        }

        [Test]
        public void AddRange_TwoMalformedBloodTypes_ThrowsDbUpdateException()
        {
            var newBloodTypes = new Collection<ReferenceBloodType>
            {
                new ReferenceBloodType(),
                new ReferenceBloodType()
            };
            UnitOfWork.ReferenceBloodTypes.AddRange(newBloodTypes);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void Remove_ValidBloodTypeNotExists_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceBloodTypes.Remove(
                new ReferenceBloodType
                {
                    Id = int.MaxValue
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidBloodTypeExists_BloodTypeCannotBeFetched()
        {
            var randomBloodTypeId = PreDefinedData.GetRandomBloodTypeId();
            var removeReferenceBloodType = UnitOfWork.ReferenceBloodTypes.Get(randomBloodTypeId);
            UnitOfWork.ReferenceBloodTypes.Remove(removeReferenceBloodType);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceBloodTypes.Get(removeReferenceBloodType.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidBloodType_ThrowsInvalidOperationException()
        {
            Assert.That(() => UnitOfWork.ReferenceBloodTypes.Remove(new ReferenceBloodType()),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceBloodTypes = UnitOfWork.ReferenceBloodTypes.GetAll().ToList();
            var removeReferenceBloodTypes = new Collection<ReferenceBloodType>();
            var removeCount = new Random().Next(1, referenceBloodTypes.Count);

            for (var i = 0; i < removeCount; i++)
            {
                removeReferenceBloodTypes.Add(referenceBloodTypes.ElementAt(i));
            }

            UnitOfWork.ReferenceBloodTypes.RemoveRange(removeReferenceBloodTypes);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceBloodTypes.GetAll();

            Assert.That(result.Count, Is.EqualTo(referenceBloodTypes.Count - removeCount));
        }

        [Test]
        public void RemoveRange_TwoValidBloodTypesDuplicated_ThrowsInvalidOperationException()
        {
            var randomBloodTypeId = PreDefinedData.GetRandomBloodTypeId();
            var randomBloodType = UnitOfWork.ReferenceBloodTypes.Get(randomBloodTypeId);

            var existingBloodTypes = new Collection<ReferenceBloodType>
            {
                new ReferenceBloodType { Id = randomBloodType.Id, Code = randomBloodType.Code },
                new ReferenceBloodType { Id = randomBloodType.Id, Code = randomBloodType.Code }
            };

            Assert.That(() => UnitOfWork.ReferenceBloodTypes.RemoveRange(existingBloodTypes),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedBloodTypes_InvalidOperationException()
        {
            var removeReferenceBloodTypes = new Collection<ReferenceBloodType>
            {
                new ReferenceBloodType(),
                new ReferenceBloodType()
            };

            Assert.That(() => UnitOfWork.ReferenceBloodTypes.RemoveRange(removeReferenceBloodTypes),
                Throws.InvalidOperationException);
        }
    }
}
