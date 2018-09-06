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
    public class ReferenceStateRepositoryTests : BaseUnitTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceStates.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceStates.Length));
        }

        [Test]
        public void Get_ValidStateCode_ReturnsSingleState()
        {
            var randomStateCode = PreDefinedData.GetRandomStateCode();
            var result = UnitOfWork.ReferenceStates.Get(randomStateCode);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Code, Is.EqualTo(randomStateCode));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidStateCode_ReturnsNull(string invalidStateCode)
        {
            var result = UnitOfWork.ReferenceStates.Get(invalidStateCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Find_PredicateUsedToFindOneState_ReturnsCollection()
        {
            var randomStateCode = PreDefinedData.GetRandomStateCode();
            Expression<Func<ReferenceState, bool>> predicate =
                (p => p.Code == randomStateCode);
            var result = UnitOfWork.ReferenceStates.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToList()[0].Code == randomStateCode);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneState_ReturnsCollection()
        {
            var randomStateCode = PreDefinedData.GetRandomStateCode();
            Expression<Func<ReferenceState, bool>> predicate =
                (p => p.Code != randomStateCode);
            var result = UnitOfWork.ReferenceStates.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(
                PreDefinedData.ReferenceStates.Length - 1));
        }

        [Test]
        public void Find_PredicateUsedToFindNoStates_ReturnsEmptyCollection()
        {
            var notExistsStateCode = PreDefinedData.GetNotExistsStateCode();
            Expression<Func<ReferenceState, bool>> predicate =
                (p => p.Code == notExistsStateCode);
            var result = UnitOfWork.ReferenceStates.Find(predicate);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneState_ReturnsOneState()
        {
            var randomStateCode = PreDefinedData.GetRandomStateCode();
            Expression<Func<ReferenceState, bool>> predicate =
                (p => p.Code == randomStateCode);
            var result = UnitOfWork.ReferenceStates.SingleOrDefault(predicate);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneState_ThrowsInvalidOperationException()
        {
            var randomStateCode = PreDefinedData.GetRandomStateCode();
            Expression<Func<ReferenceState, bool>> predicate =
                (p => p.Code != randomStateCode);

            Assert.That(() =>
                UnitOfWork.ReferenceStates.SingleOrDefault(predicate),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoStates_ReturnsNull()
        {
            var notExistsStateCode = PreDefinedData.GetNotExistsStateCode();
            Expression<Func<ReferenceState, bool>> predicate =
                (p => p.Code == notExistsStateCode);
            var result = UnitOfWork.ReferenceStates.SingleOrDefault(predicate);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidStateNotExists_FetchNewState()
        {
            var notExistsStateCode = PreDefinedData.GetNotExistsStateCode();
            var newReferenceState = new ReferenceState()
            {
                Code = notExistsStateCode,
                DisplayText = notExistsStateCode
            };

            UnitOfWork.ReferenceStates.Add(newReferenceState);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceStates.Get(notExistsStateCode);

            Assert.That(result, Is.Not.Null);
            AssertHelper.AreObjectsEqual(newReferenceState, result);
        }

        [Test]
        public void Add_ValidStateExists_ThrowsInvalidOperationException()
        {
            var randomStateCode = PreDefinedData.GetRandomStateCode();
            Assert.That(() => UnitOfWork.ReferenceStates.Add(
                new ReferenceState()
                {
                    Code = randomStateCode
                }),
                Throws.InvalidOperationException);
        }

        [Test]
        public void Add_InvalidState_ThrowsDbUpdateException()
        {
            UnitOfWork.ReferenceStates.Add(new ReferenceState());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void AddRange_TwoValidStates_CountIncreasedByTwo()
        {
        Start:
            var notExistsStateCode1 = PreDefinedData.GetNotExistsStateCode();
            var notExistsStateCode2 = PreDefinedData.GetNotExistsStateCode();
            if (notExistsStateCode1 == notExistsStateCode2)
                goto Start;

            var newStates = new Collection<ReferenceState>
            {
                new ReferenceState() { Code = notExistsStateCode1, DisplayText = "" },
                new ReferenceState() { Code = notExistsStateCode2, DisplayText = "" }
            };
            UnitOfWork.ReferenceStates.AddRange(newStates);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceStates.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceStates.Length + newStates.Count));
        }

        [Test]
        public void AddRange_TwoValidStatesDuplicated_ThrowsInvalidOperationException()
        {
            var notExistsStateCode = PreDefinedData.GetNotExistsStateCode();
            var newStates = new Collection<ReferenceState>
            {
                new ReferenceState() { Code = notExistsStateCode, DisplayText = "" },
                new ReferenceState() { Code = notExistsStateCode, DisplayText = "" }
            };

            Assert.That(() => UnitOfWork.ReferenceStates.AddRange(newStates),
                Throws.InvalidOperationException);
        }

        [Test]
        public void AddRange_TwoMalformedStates_ThrowsDbUpdateException()
        {
            var newStates = new Collection<ReferenceState>
            {
                new ReferenceState(),
                new ReferenceState()
            };
            UnitOfWork.ReferenceStates.AddRange(newStates);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void Remove_ValidStateNotExists_ThrowsDbUpdateConcurrencyException()
        {
            var notExistsStateCode = PreDefinedData.GetNotExistsStateCode();
            UnitOfWork.ReferenceStates.Remove(
                new ReferenceState()
                {
                    Code = notExistsStateCode
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidStateExists_StateCannotBeFetched()
        {
            var randomStateCode = PreDefinedData.GetRandomStateCode();
            var removeReferenceState = UnitOfWork.ReferenceStates.Get(randomStateCode);
            UnitOfWork.ReferenceStates.Remove(removeReferenceState);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceStates.Get(randomStateCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidState_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceStates.Remove(new ReferenceState());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceStates = UnitOfWork.ReferenceStates.GetAll().ToList();
            var removeReferenceStates = new Collection<ReferenceState>();
            var removeCount = new Random().Next(1, referenceStates.Count);

            for (int i = 0; i < removeCount; i++)
            {
                removeReferenceStates.Add(referenceStates.ElementAt(i));
            }

            UnitOfWork.ReferenceStates.RemoveRange(removeReferenceStates);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceStates.GetAll();

            Assert.That(result.Count, Is.EqualTo(referenceStates.Count - removeCount));
        }

        [Test]
        public void RemoveRange_TwoValidStatesDuplicated_ThrowsInvalidOperationException()
        {
            var randomStateCode = PreDefinedData.GetRandomStateCode();
            var newStates = new Collection<ReferenceState>
            {
                new ReferenceState() { Code = randomStateCode },
                new ReferenceState() { Code = randomStateCode }
            };

            Assert.That(() => UnitOfWork.ReferenceStates.RemoveRange(newStates),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedStates_DbUpdateConcurrencyException()
        {
            var removeReferenceStates = new Collection<ReferenceState>
            {
                new ReferenceState(),
                new ReferenceState()
            };

            UnitOfWork.ReferenceStates.RemoveRange(removeReferenceStates);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }
    }
}
