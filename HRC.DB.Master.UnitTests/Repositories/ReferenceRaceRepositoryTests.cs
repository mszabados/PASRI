using HRC.DB.Master.Core.Domain;
using HRC.DB.Master.Test;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using HRC.Helper.Test;

// ReSharper disable PossibleMultipleEnumeration

namespace HRC.DB.Master.UnitTests.Repositories
{
    [TestFixture]
    public class ReferenceRaceRepositoryTests : BaseUnitTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceRaces.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceRaces.Length));
        }

        [Test]
        public void Get_ValidRaceCode_ReturnsSingleRace()
        {
            var randomRaceId = PreDefinedData.GetRandomRaceId();

            var result = UnitOfWork.ReferenceRaces.Get(randomRaceId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(randomRaceId));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidRaceCode_ReturnsNull(string invalidRaceCode)
        {
            var result = UnitOfWork.ReferenceRaces.Find(p => p.Code == invalidRaceCode);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Find_PredicateUsedToFindOneRace_ReturnsCollection()
        {
            var randomRaceId = PreDefinedData.GetRandomRaceId();

            var result = UnitOfWork.ReferenceRaces.Find(p => p.Id == randomRaceId);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id == randomRaceId);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneRace_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceRaces.Find(p => p.Id != int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Find_PredicateUsedToFindNoRaces_ReturnsEmptyCollection()
        {
            var result = UnitOfWork.ReferenceRaces.Find(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneRace_ReturnsOneRace()
        {
            var randomRaceId = PreDefinedData.GetRandomRaceId();

            var result = UnitOfWork.ReferenceRaces.SingleOrDefault(p => p.Id == randomRaceId);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneRace_ThrowsInvalidOperationException()
        {
            var randomRaceId = PreDefinedData.GetRandomRaceId();

            Assert.That(() =>
                UnitOfWork.ReferenceRaces.SingleOrDefault(p => p.Id != randomRaceId),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoRaces_ReturnsNull()
        {
            var result = UnitOfWork.ReferenceRaces.SingleOrDefault(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidRaceNotExists_FetchNewRace()
        {
            var notExistsRaceCode = PreDefinedData.GetNotExistsRaceCode();
            var newReferenceRace = new ReferenceRace
            {
                Code = notExistsRaceCode,
                LongName = notExistsRaceCode
            };

            UnitOfWork.ReferenceRaces.Add(newReferenceRace);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceRaces.Get(newReferenceRace.Id);

            Assert.That(result, Is.Not.Null);

            AssertHelper.AreObjectsEqual(newReferenceRace, result);
        }

        [Test]
        public void Add_ValidRaceExists_ThrowsInvalidOperationException()
        {
            var randomRaceId = PreDefinedData.GetRandomRaceId();
            var randomRace = UnitOfWork.ReferenceRaces.Get(randomRaceId);

            Assert.That(() => UnitOfWork.ReferenceRaces.Add(
                new ReferenceRace
                {
                    Id = randomRace.Id,
                    Code = randomRace.Code
                }),
                Throws.InvalidOperationException);
        }

        [Test]
        public void Add_InvalidRace_ThrowsDbUpdateException()
        {
            UnitOfWork.ReferenceRaces.Add(new ReferenceRace());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void AddRange_TwoValidRaces_CountIncreasedByTwo()
        {
        Start:
            var notExistsRaceCode1 = PreDefinedData.GetNotExistsRaceCode();
            var notExistsRaceCode2 = PreDefinedData.GetNotExistsRaceCode();
            if (notExistsRaceCode1 == notExistsRaceCode2)
                goto Start;

            var newRaces = new Collection<ReferenceRace>
            {
                new ReferenceRace { Code = notExistsRaceCode1, LongName = "" },
                new ReferenceRace { Code = notExistsRaceCode2, LongName = "" }
            };
            UnitOfWork.ReferenceRaces.AddRange(newRaces);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceRaces.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceRaces.Length + newRaces.Count));
        }

        [Test]
        public void AddRange_TwoValidRacesDuplicated_ThrowsInvalidOperationException()
        {
            var notExistsRaceCode = PreDefinedData.GetNotExistsRaceCode();
            var newRaces = new Collection<ReferenceRace>
            {
                new ReferenceRace { Id = int.MaxValue, Code = notExistsRaceCode, LongName = "" },
                new ReferenceRace { Id = int.MaxValue, Code = notExistsRaceCode, LongName = "" }
            };

            Assert.That(() => UnitOfWork.ReferenceRaces.AddRange(newRaces),
                Throws.InvalidOperationException);
        }

        [Test]
        public void AddRange_TwoMalformedRaces_ThrowsDbUpdateException()
        {
            var newRaces = new Collection<ReferenceRace>
            {
                new ReferenceRace(),
                new ReferenceRace()
            };
            UnitOfWork.ReferenceRaces.AddRange(newRaces);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void Remove_ValidRaceNotExists_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceRaces.Remove(
                new ReferenceRace
                {
                    Id = int.MaxValue
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidRaceExists_RaceCannotBeFetched()
        {
            var randomRaceId = PreDefinedData.GetRandomRaceId();
            var removeReferenceRace = UnitOfWork.ReferenceRaces.Get(randomRaceId);
            UnitOfWork.ReferenceRaces.Remove(removeReferenceRace);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceRaces.Get(removeReferenceRace.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidRace_ThrowsInvalidOperationException()
        {
            Assert.That(() => UnitOfWork.ReferenceRaces.Remove(new ReferenceRace()),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceRaces = UnitOfWork.ReferenceRaces.GetAll().ToList();
            var removeReferenceRaces = new Collection<ReferenceRace>();
            var removeCount = new Random().Next(1, referenceRaces.Count);

            for (var i = 0; i < removeCount; i++)
            {
                removeReferenceRaces.Add(referenceRaces.ElementAt(i));
            }

            UnitOfWork.ReferenceRaces.RemoveRange(removeReferenceRaces);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceRaces.GetAll();

            Assert.That(result.Count, Is.EqualTo(referenceRaces.Count - removeCount));
        }

        [Test]
        public void RemoveRange_TwoValidRacesDuplicated_ThrowsInvalidOperationException()
        {
            var randomRaceId = PreDefinedData.GetRandomRaceId();
            var randomRace = UnitOfWork.ReferenceRaces.Get(randomRaceId);

            var existingRaces = new Collection<ReferenceRace>
            {
                new ReferenceRace { Id = randomRace.Id, Code = randomRace.Code },
                new ReferenceRace { Id = randomRace.Id, Code = randomRace.Code }
            };

            Assert.That(() => UnitOfWork.ReferenceRaces.RemoveRange(existingRaces),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedRaces_InvalidOperationException()
        {
            var removeReferenceRaces = new Collection<ReferenceRace>
            {
                new ReferenceRace(),
                new ReferenceRace()
            };

            Assert.That(() => UnitOfWork.ReferenceRaces.RemoveRange(removeReferenceRaces),
                Throws.InvalidOperationException);
        }
    }
}
