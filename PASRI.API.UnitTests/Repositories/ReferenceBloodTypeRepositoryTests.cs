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
            var randomBloodTypeCode = PreDefinedData.GetRandomBloodTypeCode();
            var result = UnitOfWork.ReferenceBloodTypes.Get(randomBloodTypeCode);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Code, Is.EqualTo(randomBloodTypeCode));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidBloodTypeCode_ReturnsNull(string invalidBloodTypeCode)
        {
            var result = UnitOfWork.ReferenceBloodTypes.Get(invalidBloodTypeCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Find_PredicateUsedToFindOneBloodType_ReturnsCollection()
        {
            var randomBloodTypeCode = PreDefinedData.GetRandomBloodTypeCode();
            Expression<Func<ReferenceBloodType, bool>> predicate =
                (p => p.Code == randomBloodTypeCode);
            var result = UnitOfWork.ReferenceBloodTypes.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToList()[0].Code == randomBloodTypeCode);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneBloodType_ReturnsCollection()
        {
            var randomBloodTypeCode = PreDefinedData.GetRandomBloodTypeCode();
            Expression<Func<ReferenceBloodType, bool>> predicate =
                (p => p.Code != randomBloodTypeCode);
            var result = UnitOfWork.ReferenceBloodTypes.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(
                PreDefinedData.ReferenceBloodTypes.Length - 1));
        }

        [Test]
        public void Find_PredicateUsedToFindNoBloodTypes_ReturnsEmptyCollection()
        {
            var notExistsBloodTypeCode = PreDefinedData.GetNotExistsBloodTypeCode();
            Expression<Func<ReferenceBloodType, bool>> predicate =
                (p => p.Code == notExistsBloodTypeCode);
            var result = UnitOfWork.ReferenceBloodTypes.Find(predicate);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneBloodType_ReturnsOneBloodType()
        {
            var randomBloodTypeCode = PreDefinedData.GetRandomBloodTypeCode();
            Expression<Func<ReferenceBloodType, bool>> predicate =
                (p => p.Code == randomBloodTypeCode);
            var result = UnitOfWork.ReferenceBloodTypes.SingleOrDefault(predicate);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneBloodType_ThrowsInvalidOperationException()
        {
            var randomBloodTypeCode = PreDefinedData.GetRandomBloodTypeCode();
            Expression<Func<ReferenceBloodType, bool>> predicate =
                (p => p.Code != randomBloodTypeCode);

            Assert.That(() =>
                UnitOfWork.ReferenceBloodTypes.SingleOrDefault(predicate),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoBloodTypes_ReturnsNull()
        {
            var notExistsBloodTypeCode = PreDefinedData.GetNotExistsBloodTypeCode();
            Expression<Func<ReferenceBloodType, bool>> predicate =
                (p => p.Code == notExistsBloodTypeCode);
            var result = UnitOfWork.ReferenceBloodTypes.SingleOrDefault(predicate);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidBloodTypeNotExists_FetchNewBloodType()
        {
            var notExistsBloodTypeCode = PreDefinedData.GetNotExistsBloodTypeCode();
            var newReferenceBloodType = new ReferenceBloodType()
            {
                Code = notExistsBloodTypeCode,
                DisplayText = notExistsBloodTypeCode
            };

            UnitOfWork.ReferenceBloodTypes.Add(newReferenceBloodType);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceBloodTypes.Get(notExistsBloodTypeCode);

            Assert.That(result, Is.Not.Null);
            AssertHelper.AreObjectsEqual(newReferenceBloodType, result);
        }

        [Test]
        public void Add_ValidBloodTypeExists_ThrowsInvalidOperationException()
        {
            var randomBloodTypeCode = PreDefinedData.GetRandomBloodTypeCode();
            Assert.That(() => UnitOfWork.ReferenceBloodTypes.Add(
                new ReferenceBloodType()
                {
                    Code = randomBloodTypeCode
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
                new ReferenceBloodType() { Code = notExistsBloodTypeCode1, DisplayText = "" },
                new ReferenceBloodType() { Code = notExistsBloodTypeCode2, DisplayText = "" }
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
                new ReferenceBloodType() { Code = notExistsBloodTypeCode, DisplayText = "" },
                new ReferenceBloodType() { Code = notExistsBloodTypeCode, DisplayText = "" }
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
            var notExistsBloodTypeCode = PreDefinedData.GetNotExistsBloodTypeCode();
            UnitOfWork.ReferenceBloodTypes.Remove(
                new ReferenceBloodType()
                {
                    Code = notExistsBloodTypeCode
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidBloodTypeExists_BloodTypeCannotBeFetched()
        {
            var randomBloodTypeCode = PreDefinedData.GetRandomBloodTypeCode();
            var removeReferenceBloodType = UnitOfWork.ReferenceBloodTypes.Get(randomBloodTypeCode);
            UnitOfWork.ReferenceBloodTypes.Remove(removeReferenceBloodType);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceBloodTypes.Get(randomBloodTypeCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidBloodType_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceBloodTypes.Remove(new ReferenceBloodType());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceBloodTypes = UnitOfWork.ReferenceBloodTypes.GetAll().ToList();
            var removeReferenceBloodTypes = new Collection<ReferenceBloodType>();
            var removeCount = new Random().Next(1, referenceBloodTypes.Count);

            for (int i = 0; i < removeCount; i++)
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
            var randomBloodTypeCode = PreDefinedData.GetRandomBloodTypeCode();
            var newBloodTypes = new Collection<ReferenceBloodType>
            {
                new ReferenceBloodType() { Code = randomBloodTypeCode },
                new ReferenceBloodType() { Code = randomBloodTypeCode }
            };

            Assert.That(() => UnitOfWork.ReferenceBloodTypes.RemoveRange(newBloodTypes),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedBloodTypes_DbUpdateConcurrencyException()
        {
            var removeReferenceBloodTypes = new Collection<ReferenceBloodType>
            {
                new ReferenceBloodType(),
                new ReferenceBloodType()
            };

            UnitOfWork.ReferenceBloodTypes.RemoveRange(removeReferenceBloodTypes);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }
    }
}
