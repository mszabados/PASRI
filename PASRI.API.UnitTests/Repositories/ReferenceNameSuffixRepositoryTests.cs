using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PASRI.API.Core.Domain;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using PASRI.API.TestHelper;

namespace PASRI.API.UnitTests.Repositories
{
    [TestFixture]
    public class ReferenceNameSuffixRepositoryTests : BaseUnitTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceNameSuffixes.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceNameSuffixes.Length));
        }

        [Test]
        public void Get_ValidNameSuffixCode_ReturnsSingleNameSuffix()
        {
            var randomNameSuffixId = PreDefinedData.GetRandomNameSuffixId();

            var result = UnitOfWork.ReferenceNameSuffixes.Get(randomNameSuffixId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(randomNameSuffixId));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidNameSuffixCode_ReturnsNull(string invalidNameSuffixCode)
        {
            var result = UnitOfWork.ReferenceNameSuffixes.Find(p => p.Code == invalidNameSuffixCode);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Find_PredicateUsedToFindOneNameSuffix_ReturnsCollection()
        {
            var randomNameSuffixId = PreDefinedData.GetRandomNameSuffixId();

            var result = UnitOfWork.ReferenceNameSuffixes.Find(p => p.Id == randomNameSuffixId);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ElementAt(0).Id == randomNameSuffixId);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneNameSuffix_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceNameSuffixes.Find(p => p.Id != Int32.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Find_PredicateUsedToFindNoNameSuffixes_ReturnsEmptyCollection()
        {
            var result = UnitOfWork.ReferenceNameSuffixes.Find(p => p.Id == Int32.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneNameSuffix_ReturnsOneNameSuffix()
        {
            var randomNameSuffixId = PreDefinedData.GetRandomNameSuffixId();

            var result = UnitOfWork.ReferenceNameSuffixes.SingleOrDefault(p => p.Id == randomNameSuffixId);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneNameSuffix_ThrowsInvalidOperationException()
        {
            var randomNameSuffixId = PreDefinedData.GetRandomNameSuffixId();

            Assert.That(() =>
                UnitOfWork.ReferenceNameSuffixes.SingleOrDefault(p => p.Id != randomNameSuffixId),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoNameSuffixes_ReturnsNull()
        {
            var result = UnitOfWork.ReferenceNameSuffixes.SingleOrDefault(p => p.Id == Int32.MaxValue);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidNameSuffixNotExists_FetchNewNameSuffix()
        {
            var notExistsNameSuffixCode = PreDefinedData.GetNotExistsNameSuffixCode();
            var newReferenceNameSuffix = new ReferenceNameSuffix()
            {
                Code = notExistsNameSuffixCode,
                LongName = notExistsNameSuffixCode
            };

            UnitOfWork.ReferenceNameSuffixes.Add(newReferenceNameSuffix);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceNameSuffixes.Get(newReferenceNameSuffix.Id);

            Assert.That(result, Is.Not.Null);

            AssertHelper.AreObjectsEqual(newReferenceNameSuffix, result);
        }

        [Test]
        public void Add_ValidNameSuffixExists_ThrowsInvalidOperationException()
        {
            var randomNameSuffixId = PreDefinedData.GetRandomNameSuffixId();
            var randomNameSuffix = UnitOfWork.ReferenceNameSuffixes.Get(randomNameSuffixId);

            Assert.That(() => UnitOfWork.ReferenceNameSuffixes.Add(
                new ReferenceNameSuffix()
                {
                    Id = randomNameSuffix.Id,
                    Code = randomNameSuffix.Code
                }),
                Throws.InvalidOperationException);
        }

        [Test]
        public void Add_InvalidNameSuffix_ThrowsDbUpdateException()
        {
            UnitOfWork.ReferenceNameSuffixes.Add(new ReferenceNameSuffix());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void AddRange_TwoValidNameSuffixes_CountIncreasedByTwo()
        {
        Start:
            var notExistsNameSuffixCode1 = PreDefinedData.GetNotExistsNameSuffixCode();
            var notExistsNameSuffixCode2 = PreDefinedData.GetNotExistsNameSuffixCode();
            if (notExistsNameSuffixCode1 == notExistsNameSuffixCode2)
                goto Start;

            var newNameSuffixes = new Collection<ReferenceNameSuffix>
            {
                new ReferenceNameSuffix() { Code = notExistsNameSuffixCode1, LongName = "" },
                new ReferenceNameSuffix() { Code = notExistsNameSuffixCode2, LongName = "" }
            };
            UnitOfWork.ReferenceNameSuffixes.AddRange(newNameSuffixes);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceNameSuffixes.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceNameSuffixes.Length + newNameSuffixes.Count));
        }

        [Test]
        public void AddRange_TwoValidNameSuffixesDuplicated_ThrowsInvalidOperationException()
        {
            var notExistsNameSuffixCode = PreDefinedData.GetNotExistsNameSuffixCode();
            var newNameSuffixes = new Collection<ReferenceNameSuffix>
            {
                new ReferenceNameSuffix() { Id = Int32.MaxValue, Code = notExistsNameSuffixCode, LongName = "" },
                new ReferenceNameSuffix() { Id = Int32.MaxValue, Code = notExistsNameSuffixCode, LongName = "" }
            };

            Assert.That(() => UnitOfWork.ReferenceNameSuffixes.AddRange(newNameSuffixes),
                Throws.InvalidOperationException);
        }

        [Test]
        public void AddRange_TwoMalformedNameSuffixes_ThrowsDbUpdateException()
        {
            var newNameSuffixes = new Collection<ReferenceNameSuffix>
            {
                new ReferenceNameSuffix(),
                new ReferenceNameSuffix()
            };
            UnitOfWork.ReferenceNameSuffixes.AddRange(newNameSuffixes);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void Remove_ValidNameSuffixNotExists_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceNameSuffixes.Remove(
                new ReferenceNameSuffix()
                {
                    Id = Int32.MaxValue
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidNameSuffixExists_NameSuffixCannotBeFetched()
        {
            var randomNameSuffixId = PreDefinedData.GetRandomNameSuffixId();
            var removeReferenceNameSuffix = UnitOfWork.ReferenceNameSuffixes.Get(randomNameSuffixId);
            UnitOfWork.ReferenceNameSuffixes.Remove(removeReferenceNameSuffix);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceNameSuffixes.Get(removeReferenceNameSuffix.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidNameSuffix_ThrowsInvalidOperationException()
        {
            Assert.That(() => UnitOfWork.ReferenceNameSuffixes.Remove(new ReferenceNameSuffix()),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceNameSuffixes = UnitOfWork.ReferenceNameSuffixes.GetAll().ToList();
            var removeReferenceNameSuffixes = new Collection<ReferenceNameSuffix>();
            var removeCount = new Random().Next(1, referenceNameSuffixes.Count);

            for (int i = 0; i < removeCount; i++)
            {
                removeReferenceNameSuffixes.Add(referenceNameSuffixes.ElementAt(i));
            }

            UnitOfWork.ReferenceNameSuffixes.RemoveRange(removeReferenceNameSuffixes);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceNameSuffixes.GetAll();

            Assert.That(result.Count, Is.EqualTo(referenceNameSuffixes.Count - removeCount));
        }

        [Test]
        public void RemoveRange_TwoValidNameSuffixesDuplicated_ThrowsInvalidOperationException()
        {
            var randomNameSuffixId = PreDefinedData.GetRandomNameSuffixId();
            var randomNameSuffix = UnitOfWork.ReferenceNameSuffixes.Get(randomNameSuffixId);

            var existingNameSuffixes = new Collection<ReferenceNameSuffix>
            {
                new ReferenceNameSuffix() { Id = randomNameSuffix.Id, Code = randomNameSuffix.Code },
                new ReferenceNameSuffix() { Id = randomNameSuffix.Id, Code = randomNameSuffix.Code }
            };

            Assert.That(() => UnitOfWork.ReferenceNameSuffixes.RemoveRange(existingNameSuffixes),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedNameSuffixes_InvalidOperationException()
        {
            var removeReferenceNameSuffixes = new Collection<ReferenceNameSuffix>
            {
                new ReferenceNameSuffix(),
                new ReferenceNameSuffix()
            };

            Assert.That(() => UnitOfWork.ReferenceNameSuffixes.RemoveRange(removeReferenceNameSuffixes),
                Throws.InvalidOperationException);
        }
    }
}
