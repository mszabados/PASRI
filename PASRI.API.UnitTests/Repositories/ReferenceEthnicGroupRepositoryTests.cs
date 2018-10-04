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
    public class ReferenceEthnicGroupRepositoryTests : BaseUnitTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceEthnicGroups.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceEthnicGroups.Length));
        }

        [Test]
        public void Get_ValidEthnicGroupCode_ReturnsSingleEthnicGroup()
        {
            var randomEthnicGroupId = PreDefinedData.GetRandomEthnicGroupId();

            var result = UnitOfWork.ReferenceEthnicGroups.Get(randomEthnicGroupId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(randomEthnicGroupId));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidEthnicGroupCode_ReturnsNull(string invalidEthnicGroupCode)
        {
            var result = UnitOfWork.ReferenceEthnicGroups.Find(p => p.Code == invalidEthnicGroupCode);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Find_PredicateUsedToFindOneEthnicGroup_ReturnsCollection()
        {
            var randomEthnicGroupId = PreDefinedData.GetRandomEthnicGroupId();

            var result = UnitOfWork.ReferenceEthnicGroups.Find(p => p.Id == randomEthnicGroupId);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id == randomEthnicGroupId);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneEthnicGroup_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceEthnicGroups.Find(p => p.Id != int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Find_PredicateUsedToFindNoEthnicGroups_ReturnsEmptyCollection()
        {
            var result = UnitOfWork.ReferenceEthnicGroups.Find(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneEthnicGroup_ReturnsOneEthnicGroup()
        {
            var randomEthnicGroupId = PreDefinedData.GetRandomEthnicGroupId();

            var result = UnitOfWork.ReferenceEthnicGroups.SingleOrDefault(p => p.Id == randomEthnicGroupId);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneEthnicGroup_ThrowsInvalidOperationException()
        {
            var randomEthnicGroupId = PreDefinedData.GetRandomEthnicGroupId();

            Assert.That(() =>
                UnitOfWork.ReferenceEthnicGroups.SingleOrDefault(p => p.Id != randomEthnicGroupId),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoEthnicGroups_ReturnsNull()
        {
            var result = UnitOfWork.ReferenceEthnicGroups.SingleOrDefault(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidEthnicGroupNotExists_FetchNewEthnicGroup()
        {
            var notExistsEthnicGroupCode = PreDefinedData.GetNotExistsEthnicGroupCode();
            var newReferenceEthnicGroup = new ReferenceEthnicGroup
            {
                Code = notExistsEthnicGroupCode,
                LongName = notExistsEthnicGroupCode
            };

            UnitOfWork.ReferenceEthnicGroups.Add(newReferenceEthnicGroup);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceEthnicGroups.Get(newReferenceEthnicGroup.Id);

            Assert.That(result, Is.Not.Null);

            AssertHelper.AreObjectsEqual(newReferenceEthnicGroup, result);
        }

        [Test]
        public void Add_ValidEthnicGroupExists_ThrowsInvalidOperationException()
        {
            var randomEthnicGroupId = PreDefinedData.GetRandomEthnicGroupId();
            var randomEthnicGroup = UnitOfWork.ReferenceEthnicGroups.Get(randomEthnicGroupId);

            Assert.That(() => UnitOfWork.ReferenceEthnicGroups.Add(
                new ReferenceEthnicGroup
                {
                    Id = randomEthnicGroup.Id,
                    Code = randomEthnicGroup.Code
                }),
                Throws.InvalidOperationException);
        }

        [Test]
        public void Add_InvalidEthnicGroup_ThrowsDbUpdateException()
        {
            UnitOfWork.ReferenceEthnicGroups.Add(new ReferenceEthnicGroup());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void AddRange_TwoValidEthnicGroups_CountIncreasedByTwo()
        {
        Start:
            var notExistsEthnicGroupCode1 = PreDefinedData.GetNotExistsEthnicGroupCode();
            var notExistsEthnicGroupCode2 = PreDefinedData.GetNotExistsEthnicGroupCode();
            if (notExistsEthnicGroupCode1 == notExistsEthnicGroupCode2)
                goto Start;

            var newEthnicGroups = new Collection<ReferenceEthnicGroup>
            {
                new ReferenceEthnicGroup { Code = notExistsEthnicGroupCode1, LongName = "" },
                new ReferenceEthnicGroup { Code = notExistsEthnicGroupCode2, LongName = "" }
            };
            UnitOfWork.ReferenceEthnicGroups.AddRange(newEthnicGroups);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceEthnicGroups.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceEthnicGroups.Length + newEthnicGroups.Count));
        }

        [Test]
        public void AddRange_TwoValidEthnicGroupsDuplicated_ThrowsInvalidOperationException()
        {
            var notExistsEthnicGroupCode = PreDefinedData.GetNotExistsEthnicGroupCode();
            var newEthnicGroups = new Collection<ReferenceEthnicGroup>
            {
                new ReferenceEthnicGroup { Id = int.MaxValue, Code = notExistsEthnicGroupCode, LongName = "" },
                new ReferenceEthnicGroup { Id = int.MaxValue, Code = notExistsEthnicGroupCode, LongName = "" }
            };

            Assert.That(() => UnitOfWork.ReferenceEthnicGroups.AddRange(newEthnicGroups),
                Throws.InvalidOperationException);
        }

        [Test]
        public void AddRange_TwoMalformedEthnicGroups_ThrowsDbUpdateException()
        {
            var newEthnicGroups = new Collection<ReferenceEthnicGroup>
            {
                new ReferenceEthnicGroup(),
                new ReferenceEthnicGroup()
            };
            UnitOfWork.ReferenceEthnicGroups.AddRange(newEthnicGroups);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void Remove_ValidEthnicGroupNotExists_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceEthnicGroups.Remove(
                new ReferenceEthnicGroup
                {
                    Id = int.MaxValue
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidEthnicGroupExists_EthnicGroupCannotBeFetched()
        {
            var randomEthnicGroupId = PreDefinedData.GetRandomEthnicGroupId();
            var removeReferenceEthnicGroup = UnitOfWork.ReferenceEthnicGroups.Get(randomEthnicGroupId);
            UnitOfWork.ReferenceEthnicGroups.Remove(removeReferenceEthnicGroup);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceEthnicGroups.Get(removeReferenceEthnicGroup.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidEthnicGroup_ThrowsInvalidOperationException()
        {
            Assert.That(() => UnitOfWork.ReferenceEthnicGroups.Remove(new ReferenceEthnicGroup()),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceEthnicGroups = UnitOfWork.ReferenceEthnicGroups.GetAll().ToList();
            var removeReferenceEthnicGroups = new Collection<ReferenceEthnicGroup>();
            var removeCount = new Random().Next(1, referenceEthnicGroups.Count);

            for (var i = 0; i < removeCount; i++)
            {
                removeReferenceEthnicGroups.Add(referenceEthnicGroups.ElementAt(i));
            }

            UnitOfWork.ReferenceEthnicGroups.RemoveRange(removeReferenceEthnicGroups);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceEthnicGroups.GetAll();

            Assert.That(result.Count, Is.EqualTo(referenceEthnicGroups.Count - removeCount));
        }

        [Test]
        public void RemoveRange_TwoValidEthnicGroupsDuplicated_ThrowsInvalidOperationException()
        {
            var randomEthnicGroupId = PreDefinedData.GetRandomEthnicGroupId();
            var randomEthnicGroup = UnitOfWork.ReferenceEthnicGroups.Get(randomEthnicGroupId);

            var existingEthnicGroups = new Collection<ReferenceEthnicGroup>
            {
                new ReferenceEthnicGroup { Id = randomEthnicGroup.Id, Code = randomEthnicGroup.Code },
                new ReferenceEthnicGroup { Id = randomEthnicGroup.Id, Code = randomEthnicGroup.Code }
            };

            Assert.That(() => UnitOfWork.ReferenceEthnicGroups.RemoveRange(existingEthnicGroups),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedEthnicGroups_InvalidOperationException()
        {
            var removeReferenceEthnicGroups = new Collection<ReferenceEthnicGroup>
            {
                new ReferenceEthnicGroup(),
                new ReferenceEthnicGroup()
            };

            Assert.That(() => UnitOfWork.ReferenceEthnicGroups.RemoveRange(removeReferenceEthnicGroups),
                Throws.InvalidOperationException);
        }
    }
}
