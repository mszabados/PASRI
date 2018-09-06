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
    public class ReferenceSuffixNameRepositoryTests : BaseUnitTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceSuffixNames.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceSuffixNames.Length));
        }

        [Test]
        public void Get_ValidSuffixNameCode_ReturnsSingleSuffixName()
        {
            var randomSuffixNameCode = PreDefinedData.GetRandomSuffixNameCode();
            var result = UnitOfWork.ReferenceSuffixNames.Get(randomSuffixNameCode);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Code, Is.EqualTo(randomSuffixNameCode));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidSuffixNameCode_ReturnsNull(string invalidSuffixNameCode)
        {
            var result = UnitOfWork.ReferenceSuffixNames.Get(invalidSuffixNameCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Find_PredicateUsedToFindOneSuffixName_ReturnsCollection()
        {
            var randomSuffixNameCode = PreDefinedData.GetRandomSuffixNameCode();
            Expression<Func<ReferenceSuffixName, bool>> predicate =
                (p => p.Code == randomSuffixNameCode);
            var result = UnitOfWork.ReferenceSuffixNames.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToList()[0].Code == randomSuffixNameCode);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneSuffixName_ReturnsCollection()
        {
            var randomSuffixNameCode = PreDefinedData.GetRandomSuffixNameCode();
            Expression<Func<ReferenceSuffixName, bool>> predicate =
                (p => p.Code != randomSuffixNameCode);
            var result = UnitOfWork.ReferenceSuffixNames.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(
                PreDefinedData.ReferenceSuffixNames.Length - 1));
        }

        [Test]
        public void Find_PredicateUsedToFindNoSuffixNames_ReturnsEmptyCollection()
        {
            var notExistsSuffixNameCode = PreDefinedData.GetNotExistsSuffixNameCode();
            Expression<Func<ReferenceSuffixName, bool>> predicate =
                (p => p.Code == notExistsSuffixNameCode);
            var result = UnitOfWork.ReferenceSuffixNames.Find(predicate);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneSuffixName_ReturnsOneSuffixName()
        {
            var randomSuffixNameCode = PreDefinedData.GetRandomSuffixNameCode();
            Expression<Func<ReferenceSuffixName, bool>> predicate =
                (p => p.Code == randomSuffixNameCode);
            var result = UnitOfWork.ReferenceSuffixNames.SingleOrDefault(predicate);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneSuffixName_ThrowsInvalidOperationException()
        {
            var randomSuffixNameCode = PreDefinedData.GetRandomSuffixNameCode();
            Expression<Func<ReferenceSuffixName, bool>> predicate =
                (p => p.Code != randomSuffixNameCode);

            Assert.That(() =>
                UnitOfWork.ReferenceSuffixNames.SingleOrDefault(predicate),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoSuffixNames_ReturnsNull()
        {
            var notExistsSuffixNameCode = PreDefinedData.GetNotExistsSuffixNameCode();
            Expression<Func<ReferenceSuffixName, bool>> predicate =
                (p => p.Code == notExistsSuffixNameCode);
            var result = UnitOfWork.ReferenceSuffixNames.SingleOrDefault(predicate);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidSuffixNameNotExists_FetchNewSuffixName()
        {
            var notExistsSuffixNameCode = PreDefinedData.GetNotExistsSuffixNameCode();
            var newReferenceSuffixName = new ReferenceSuffixName()
            {
                Code = notExistsSuffixNameCode,
                DisplayText = notExistsSuffixNameCode
            };

            UnitOfWork.ReferenceSuffixNames.Add(newReferenceSuffixName);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceSuffixNames.Get(notExistsSuffixNameCode);

            Assert.That(result, Is.Not.Null);
            AssertHelper.AreObjectsEqual(newReferenceSuffixName, result);
        }

        [Test]
        public void Add_ValidSuffixNameExists_ThrowsInvalidOperationException()
        {
            var randomSuffixNameCode = PreDefinedData.GetRandomSuffixNameCode();
            Assert.That(() => UnitOfWork.ReferenceSuffixNames.Add(
                new ReferenceSuffixName()
                {
                    Code = randomSuffixNameCode
                }),
                Throws.InvalidOperationException);
        }

        [Test]
        public void Add_InvalidSuffixName_ThrowsDbUpdateException()
        {
            UnitOfWork.ReferenceSuffixNames.Add(new ReferenceSuffixName());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void AddRange_TwoValidSuffixNames_CountIncreasedByTwo()
        {
        Start:
            var notExistsSuffixNameCode1 = PreDefinedData.GetNotExistsSuffixNameCode();
            var notExistsSuffixNameCode2 = PreDefinedData.GetNotExistsSuffixNameCode();
            if (notExistsSuffixNameCode1 == notExistsSuffixNameCode2)
                goto Start;

            var newSuffixNames = new Collection<ReferenceSuffixName>
            {
                new ReferenceSuffixName() { Code = notExistsSuffixNameCode1, DisplayText = "" },
                new ReferenceSuffixName() { Code = notExistsSuffixNameCode2, DisplayText = "" }
            };
            UnitOfWork.ReferenceSuffixNames.AddRange(newSuffixNames);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceSuffixNames.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceSuffixNames.Length + newSuffixNames.Count));
        }

        [Test]
        public void AddRange_TwoValidSuffixNamesDuplicated_ThrowsInvalidOperationException()
        {
            var notExistsSuffixNameCode = PreDefinedData.GetNotExistsSuffixNameCode();
            var newSuffixNames = new Collection<ReferenceSuffixName>
            {
                new ReferenceSuffixName() { Code = notExistsSuffixNameCode, DisplayText = "" },
                new ReferenceSuffixName() { Code = notExistsSuffixNameCode, DisplayText = "" }
            };

            Assert.That(() => UnitOfWork.ReferenceSuffixNames.AddRange(newSuffixNames),
                Throws.InvalidOperationException);
        }

        [Test]
        public void AddRange_TwoMalformedSuffixNames_ThrowsDbUpdateException()
        {
            var newSuffixNames = new Collection<ReferenceSuffixName>
            {
                new ReferenceSuffixName(),
                new ReferenceSuffixName()
            };
            UnitOfWork.ReferenceSuffixNames.AddRange(newSuffixNames);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void Remove_ValidSuffixNameNotExists_ThrowsDbUpdateConcurrencyException()
        {
            var notExistsSuffixNameCode = PreDefinedData.GetNotExistsSuffixNameCode();
            UnitOfWork.ReferenceSuffixNames.Remove(
                new ReferenceSuffixName()
                {
                    Code = notExistsSuffixNameCode
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidSuffixNameExists_SuffixNameCannotBeFetched()
        {
            var randomSuffixNameCode = PreDefinedData.GetRandomSuffixNameCode();
            var removeReferenceSuffixName = UnitOfWork.ReferenceSuffixNames.Get(randomSuffixNameCode);
            UnitOfWork.ReferenceSuffixNames.Remove(removeReferenceSuffixName);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceSuffixNames.Get(randomSuffixNameCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidSuffixName_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceSuffixNames.Remove(new ReferenceSuffixName());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceSuffixNames = UnitOfWork.ReferenceSuffixNames.GetAll().ToList();
            var removeReferenceSuffixNames = new Collection<ReferenceSuffixName>();
            var removeCount = new Random().Next(1, referenceSuffixNames.Count);

            for (int i = 0; i < removeCount; i++)
            {
                removeReferenceSuffixNames.Add(referenceSuffixNames.ElementAt(i));
            }

            UnitOfWork.ReferenceSuffixNames.RemoveRange(removeReferenceSuffixNames);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceSuffixNames.GetAll();

            Assert.That(result.Count, Is.EqualTo(referenceSuffixNames.Count - removeCount));
        }

        [Test]
        public void RemoveRange_TwoValidSuffixNamesDuplicated_ThrowsInvalidOperationException()
        {
            var randomSuffixNameCode = PreDefinedData.GetRandomSuffixNameCode();
            var newSuffixNames = new Collection<ReferenceSuffixName>
            {
                new ReferenceSuffixName() { Code = randomSuffixNameCode },
                new ReferenceSuffixName() { Code = randomSuffixNameCode }
            };

            Assert.That(() => UnitOfWork.ReferenceSuffixNames.RemoveRange(newSuffixNames),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedSuffixNames_DbUpdateConcurrencyException()
        {
            var removeReferenceSuffixNames = new Collection<ReferenceSuffixName>
            {
                new ReferenceSuffixName(),
                new ReferenceSuffixName()
            };

            UnitOfWork.ReferenceSuffixNames.RemoveRange(removeReferenceSuffixNames);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }
    }
}
