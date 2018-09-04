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
    public class ReferenceBloodTypeRepositoryTests : BaseUnitTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceBloodTypes.GetAll();

            Assert.That(result.Count, Is.EqualTo(4));
        }

        [Test]
        public void Get_ValidBloodTypeCode_ReturnsSingleBloodType()
        {
            var validBloodTypeCode = "O";
            var result = UnitOfWork.ReferenceBloodTypes.Get(validBloodTypeCode);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Code, Is.EqualTo(validBloodTypeCode));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("Z")]
        public void Get_InvalidBloodTypeCode_ReturnsNull(string invalidBloodTypeCode)
        {
            var result = UnitOfWork.ReferenceBloodTypes.Get(invalidBloodTypeCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Find_PredicateUsedToFindOneBloodType_ReturnsCollection()
        {
            Expression<Func<ReferenceBloodType, bool>> predicate =
                (p => p.Code == "O");
            var result = UnitOfWork.ReferenceBloodTypes.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneBloodType_ReturnsCollection()
        {
            Expression<Func<ReferenceBloodType, bool>> predicate =
                (p => p.Code != "O");
            var result = UnitOfWork.ReferenceBloodTypes.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void Find_PredicateUsedToFindNoBloodTypes_ReturnsEmptyCollection()
        {
            Expression<Func<ReferenceBloodType, bool>> predicate =
                (p => p.Code == "Z");
            var result = UnitOfWork.ReferenceBloodTypes.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneBloodType_ReturnsOneBloodType()
        {
            Expression<Func<ReferenceBloodType, bool>> predicate =
                (p => p.Code == "O");
            var result = UnitOfWork.ReferenceBloodTypes.SingleOrDefault(predicate);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneBloodType_ThrowsInvalidOperationException()
        {
            Expression<Func<ReferenceBloodType, bool>> predicate =
                (p => p.Code != "O");

            Assert.That(() =>
                UnitOfWork.ReferenceBloodTypes.SingleOrDefault(predicate),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoBloodTypes_ReturnsNull()
        {
            Expression<Func<ReferenceBloodType, bool>> predicate =
                (p => p.Code == "Z");
            var result = UnitOfWork.ReferenceBloodTypes.SingleOrDefault(predicate);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidBloodTypeNotExists_FetchNewBloodType()
        {
            string testBloodTypeCode = "Z";
            var newReferenceBloodType = new ReferenceBloodType()
            {
                Code = testBloodTypeCode,
                DisplayText = testBloodTypeCode
            };

            UnitOfWork.ReferenceBloodTypes.Add(newReferenceBloodType);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceBloodTypes.Get(testBloodTypeCode);

            Assert.That(result, Is.Not.Null);
            AssertPropertyValuesAreEqual(newReferenceBloodType, result);
        }

        [Test]
        public void Add_ValidBloodTypeExists_ThrowsInvalidOperationException()
        {
            Assert.That(() => UnitOfWork.ReferenceBloodTypes.Add(
                new ReferenceBloodType()
                {
                    Code = "O",
                    DisplayText = ""
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
        public void AddRange_ValidBloodTypes_CountIncreasedByTwo()
        {
            var newBloodTypes = new Collection<ReferenceBloodType>
            {
                new ReferenceBloodType() { Code = "Z", DisplayText = "" },
                new ReferenceBloodType() { Code = "Y", DisplayText = "" }
            };
            UnitOfWork.ReferenceBloodTypes.AddRange(newBloodTypes);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceBloodTypes.GetAll();

            Assert.That(result.Count, Is.EqualTo(6));
        }

        [Test]
        public void AddRange_ValidBloodTypesDuplicated_ThrowsInvalidOperationException()
        {
            var newBloodTypes = new Collection<ReferenceBloodType>
            {
                new ReferenceBloodType() { Code = "Z", DisplayText = "" },
                new ReferenceBloodType() { Code = "Z", DisplayText = "" }
            };

            Assert.That(() => UnitOfWork.ReferenceBloodTypes.AddRange(newBloodTypes),
                Throws.InvalidOperationException);
        }

        [Test]
        public void AddRange_InvalidBloodTypes_ThrowsDbUpdateException()
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
            string testBloodTypeCode = "Z";
            UnitOfWork.ReferenceBloodTypes.Remove(
                new ReferenceBloodType()
                {
                    Code = testBloodTypeCode,
                    DisplayText = testBloodTypeCode
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidBloodTypeExists_BloodTypeCannotBeFetched()
        {
            string validBloodTypeCode = "O";
            var removeReferenceBloodType = UnitOfWork.ReferenceBloodTypes.Get(validBloodTypeCode);
            UnitOfWork.ReferenceBloodTypes.Remove(removeReferenceBloodType);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceBloodTypes.Get(validBloodTypeCode);

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
        public void RemoveRange_TwoValidBloodTypes_OneBloodTypeRemains()
        {
            var referenceBloodTypes = UnitOfWork.ReferenceBloodTypes.GetAll().ToList();
            var removeReferenceBloodTypes = new Collection<ReferenceBloodType>();
            var removeCount = 2;
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
        public void RemoveRange_ValidBloodTypesDuplicated_ThrowsInvalidOperationException()
        {
            var newBloodTypes = new Collection<ReferenceBloodType>
            {
                new ReferenceBloodType() { Code = "O", DisplayText = "" },
                new ReferenceBloodType() { Code = "O", DisplayText = "" }
            };

            Assert.That(() => UnitOfWork.ReferenceBloodTypes.RemoveRange(newBloodTypes),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_InvalidBloodTypes_DbUpdateConcurrencyException()
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
