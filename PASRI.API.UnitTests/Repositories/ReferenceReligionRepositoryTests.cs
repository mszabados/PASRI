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
    public class ReferenceReligionRepositoryTests : BaseUnitTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceReligions.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceReligions.Length));
        }

        [Test]
        public void Get_ValidReligionCode_ReturnsSingleReligion()
        {
            var randomReligionId = PreDefinedData.GetRandomReligionId();

            var result = UnitOfWork.ReferenceReligions.Get(randomReligionId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(randomReligionId));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidReligionCode_ReturnsNull(string invalidReligionCode)
        {
            var result = UnitOfWork.ReferenceReligions.Find(p => p.Code == invalidReligionCode);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Find_PredicateUsedToFindOneReligion_ReturnsCollection()
        {
            var randomReligionId = PreDefinedData.GetRandomReligionId();

            var result = UnitOfWork.ReferenceReligions.Find(p => p.Id == randomReligionId);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id == randomReligionId);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneReligion_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceReligions.Find(p => p.Id != int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Find_PredicateUsedToFindNoReligions_ReturnsEmptyCollection()
        {
            var result = UnitOfWork.ReferenceReligions.Find(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneReligion_ReturnsOneReligion()
        {
            var randomReligionId = PreDefinedData.GetRandomReligionId();

            var result = UnitOfWork.ReferenceReligions.SingleOrDefault(p => p.Id == randomReligionId);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneReligion_ThrowsInvalidOperationException()
        {
            var randomReligionId = PreDefinedData.GetRandomReligionId();

            Assert.That(() =>
                UnitOfWork.ReferenceReligions.SingleOrDefault(p => p.Id != randomReligionId),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoReligions_ReturnsNull()
        {
            var result = UnitOfWork.ReferenceReligions.SingleOrDefault(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidReligionNotExists_FetchNewReligion()
        {
            var notExistsReligionCode = PreDefinedData.GetNotExistsReligionCode();
            var newReferenceReligion = new ReferenceReligion
            {
                Code = notExistsReligionCode,
                LongName = notExistsReligionCode
            };

            UnitOfWork.ReferenceReligions.Add(newReferenceReligion);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceReligions.Get(newReferenceReligion.Id);

            Assert.That(result, Is.Not.Null);

            AssertHelper.AreObjectsEqual(newReferenceReligion, result);
        }

        [Test]
        public void Add_ValidReligionExists_ThrowsInvalidOperationException()
        {
            var randomReligionId = PreDefinedData.GetRandomReligionId();
            var randomReligion = UnitOfWork.ReferenceReligions.Get(randomReligionId);

            Assert.That(() => UnitOfWork.ReferenceReligions.Add(
                new ReferenceReligion
                {
                    Id = randomReligion.Id,
                    Code = randomReligion.Code
                }),
                Throws.InvalidOperationException);
        }

        [Test]
        public void Add_InvalidReligion_ThrowsDbUpdateException()
        {
            UnitOfWork.ReferenceReligions.Add(new ReferenceReligion());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void AddRange_TwoValidReligions_CountIncreasedByTwo()
        {
        Start:
            var notExistsReligionCode1 = PreDefinedData.GetNotExistsReligionCode();
            var notExistsReligionCode2 = PreDefinedData.GetNotExistsReligionCode();
            if (notExistsReligionCode1 == notExistsReligionCode2)
                goto Start;

            var newReligions = new Collection<ReferenceReligion>
            {
                new ReferenceReligion { Code = notExistsReligionCode1, LongName = "" },
                new ReferenceReligion { Code = notExistsReligionCode2, LongName = "" }
            };
            UnitOfWork.ReferenceReligions.AddRange(newReligions);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceReligions.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceReligions.Length + newReligions.Count));
        }

        [Test]
        public void AddRange_TwoValidReligionsDuplicated_ThrowsInvalidOperationException()
        {
            var notExistsReligionCode = PreDefinedData.GetNotExistsReligionCode();
            var newReligions = new Collection<ReferenceReligion>
            {
                new ReferenceReligion { Id = int.MaxValue, Code = notExistsReligionCode, LongName = "" },
                new ReferenceReligion { Id = int.MaxValue, Code = notExistsReligionCode, LongName = "" }
            };

            Assert.That(() => UnitOfWork.ReferenceReligions.AddRange(newReligions),
                Throws.InvalidOperationException);
        }

        [Test]
        public void AddRange_TwoMalformedReligions_ThrowsDbUpdateException()
        {
            var newReligions = new Collection<ReferenceReligion>
            {
                new ReferenceReligion(),
                new ReferenceReligion()
            };
            UnitOfWork.ReferenceReligions.AddRange(newReligions);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void Remove_ValidReligionNotExists_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceReligions.Remove(
                new ReferenceReligion
                {
                    Id = int.MaxValue
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidReligionExists_ReligionCannotBeFetched()
        {
            var randomReligionId = PreDefinedData.GetRandomReligionId();
            var removeReferenceReligion = UnitOfWork.ReferenceReligions.Get(randomReligionId);
            UnitOfWork.ReferenceReligions.Remove(removeReferenceReligion);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceReligions.Get(removeReferenceReligion.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidReligion_ThrowsInvalidOperationException()
        {
            Assert.That(() => UnitOfWork.ReferenceReligions.Remove(new ReferenceReligion()),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceReligions = UnitOfWork.ReferenceReligions.GetAll().ToList();
            var removeReferenceReligions = new Collection<ReferenceReligion>();
            var removeCount = new Random().Next(1, referenceReligions.Count);

            for (var i = 0; i < removeCount; i++)
            {
                removeReferenceReligions.Add(referenceReligions.ElementAt(i));
            }

            UnitOfWork.ReferenceReligions.RemoveRange(removeReferenceReligions);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceReligions.GetAll();

            Assert.That(result.Count, Is.EqualTo(referenceReligions.Count - removeCount));
        }

        [Test]
        public void RemoveRange_TwoValidReligionsDuplicated_ThrowsInvalidOperationException()
        {
            var randomReligionId = PreDefinedData.GetRandomReligionId();
            var randomReligion = UnitOfWork.ReferenceReligions.Get(randomReligionId);

            var existingReligions = new Collection<ReferenceReligion>
            {
                new ReferenceReligion { Id = randomReligion.Id, Code = randomReligion.Code },
                new ReferenceReligion { Id = randomReligion.Id, Code = randomReligion.Code }
            };

            Assert.That(() => UnitOfWork.ReferenceReligions.RemoveRange(existingReligions),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedReligions_InvalidOperationException()
        {
            var removeReferenceReligions = new Collection<ReferenceReligion>
            {
                new ReferenceReligion(),
                new ReferenceReligion()
            };

            Assert.That(() => UnitOfWork.ReferenceReligions.RemoveRange(removeReferenceReligions),
                Throws.InvalidOperationException);
        }
    }
}
