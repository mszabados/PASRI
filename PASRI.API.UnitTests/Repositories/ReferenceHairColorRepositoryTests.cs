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
    public class ReferenceHairColorRepositoryTests : BaseUnitTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceHairColors.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceHairColors.Length));
        }

        [Test]
        public void Get_ValidHairColorCode_ReturnsSingleHairColor()
        {
            var randomHairColorCode = PreDefinedData.GetRandomHairColorCode();
            var result = UnitOfWork.ReferenceHairColors.Get(randomHairColorCode);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Code, Is.EqualTo(randomHairColorCode));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidHairColorCode_ReturnsNull(string invalidHairColorCode)
        {
            var result = UnitOfWork.ReferenceHairColors.Get(invalidHairColorCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Find_PredicateUsedToFindOneHairColor_ReturnsCollection()
        {
            var randomHairColorCode = PreDefinedData.GetRandomHairColorCode();
            Expression<Func<ReferenceHairColor, bool>> predicate =
                (p => p.Code == randomHairColorCode);
            var result = UnitOfWork.ReferenceHairColors.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToList()[0].Code == randomHairColorCode);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneHairColor_ReturnsCollection()
        {
            var randomHairColorCode = PreDefinedData.GetRandomHairColorCode();
            Expression<Func<ReferenceHairColor, bool>> predicate =
                (p => p.Code != randomHairColorCode);
            var result = UnitOfWork.ReferenceHairColors.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(
                PreDefinedData.ReferenceHairColors.Length - 1));
        }

        [Test]
        public void Find_PredicateUsedToFindNoHairColors_ReturnsEmptyCollection()
        {
            var notExistsHairColorCode = PreDefinedData.GetNotExistsHairColorCode();
            Expression<Func<ReferenceHairColor, bool>> predicate =
                (p => p.Code == notExistsHairColorCode);
            var result = UnitOfWork.ReferenceHairColors.Find(predicate);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneHairColor_ReturnsOneHairColor()
        {
            var randomHairColorCode = PreDefinedData.GetRandomHairColorCode();
            Expression<Func<ReferenceHairColor, bool>> predicate =
                (p => p.Code == randomHairColorCode);
            var result = UnitOfWork.ReferenceHairColors.SingleOrDefault(predicate);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneHairColor_ThrowsInvalidOperationException()
        {
            var randomHairColorCode = PreDefinedData.GetRandomHairColorCode();
            Expression<Func<ReferenceHairColor, bool>> predicate =
                (p => p.Code != randomHairColorCode);

            Assert.That(() =>
                UnitOfWork.ReferenceHairColors.SingleOrDefault(predicate),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoHairColors_ReturnsNull()
        {
            var notExistsHairColorCode = PreDefinedData.GetNotExistsHairColorCode();
            Expression<Func<ReferenceHairColor, bool>> predicate =
                (p => p.Code == notExistsHairColorCode);
            var result = UnitOfWork.ReferenceHairColors.SingleOrDefault(predicate);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidHairColorNotExists_FetchNewHairColor()
        {
            var notExistsHairColorCode = PreDefinedData.GetNotExistsHairColorCode();
            var newReferenceHairColor = new ReferenceHairColor()
            {
                Code = notExistsHairColorCode,
                DisplayText = notExistsHairColorCode
            };

            UnitOfWork.ReferenceHairColors.Add(newReferenceHairColor);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceHairColors.Get(notExistsHairColorCode);

            Assert.That(result, Is.Not.Null);
            AssertHelper.AreObjectsEqual(newReferenceHairColor, result);
        }

        [Test]
        public void Add_ValidHairColorExists_ThrowsInvalidOperationException()
        {
            var randomHairColorCode = PreDefinedData.GetRandomHairColorCode();
            Assert.That(() => UnitOfWork.ReferenceHairColors.Add(
                new ReferenceHairColor()
                {
                    Code = randomHairColorCode
                }),
                Throws.InvalidOperationException);
        }

        [Test]
        public void Add_InvalidHairColor_ThrowsDbUpdateException()
        {
            UnitOfWork.ReferenceHairColors.Add(new ReferenceHairColor());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void AddRange_TwoValidHairColors_CountIncreasedByTwo()
        {
        Start:
            var notExistsHairColorCode1 = PreDefinedData.GetNotExistsHairColorCode();
            var notExistsHairColorCode2 = PreDefinedData.GetNotExistsHairColorCode();
            if (notExistsHairColorCode1 == notExistsHairColorCode2)
                goto Start;

            var newHairColors = new Collection<ReferenceHairColor>
            {
                new ReferenceHairColor() { Code = notExistsHairColorCode1, DisplayText = "" },
                new ReferenceHairColor() { Code = notExistsHairColorCode2, DisplayText = "" }
            };
            UnitOfWork.ReferenceHairColors.AddRange(newHairColors);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceHairColors.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceHairColors.Length + newHairColors.Count));
        }

        [Test]
        public void AddRange_TwoValidHairColorsDuplicated_ThrowsInvalidOperationException()
        {
            var notExistsHairColorCode = PreDefinedData.GetNotExistsHairColorCode();
            var newHairColors = new Collection<ReferenceHairColor>
            {
                new ReferenceHairColor() { Code = notExistsHairColorCode, DisplayText = "" },
                new ReferenceHairColor() { Code = notExistsHairColorCode, DisplayText = "" }
            };

            Assert.That(() => UnitOfWork.ReferenceHairColors.AddRange(newHairColors),
                Throws.InvalidOperationException);
        }

        [Test]
        public void AddRange_TwoMalformedHairColors_ThrowsDbUpdateException()
        {
            var newHairColors = new Collection<ReferenceHairColor>
            {
                new ReferenceHairColor(),
                new ReferenceHairColor()
            };
            UnitOfWork.ReferenceHairColors.AddRange(newHairColors);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void Remove_ValidHairColorNotExists_ThrowsDbUpdateConcurrencyException()
        {
            var notExistsHairColorCode = PreDefinedData.GetNotExistsHairColorCode();
            UnitOfWork.ReferenceHairColors.Remove(
                new ReferenceHairColor()
                {
                    Code = notExistsHairColorCode
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidHairColorExists_HairColorCannotBeFetched()
        {
            var randomHairColorCode = PreDefinedData.GetRandomHairColorCode();
            var removeReferenceHairColor = UnitOfWork.ReferenceHairColors.Get(randomHairColorCode);
            UnitOfWork.ReferenceHairColors.Remove(removeReferenceHairColor);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceHairColors.Get(randomHairColorCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidHairColor_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceHairColors.Remove(new ReferenceHairColor());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceHairColors = UnitOfWork.ReferenceHairColors.GetAll().ToList();
            var removeReferenceHairColors = new Collection<ReferenceHairColor>();
            var removeCount = new Random().Next(1, referenceHairColors.Count);

            for (int i = 0; i < removeCount; i++)
            {
                removeReferenceHairColors.Add(referenceHairColors.ElementAt(i));
            }

            UnitOfWork.ReferenceHairColors.RemoveRange(removeReferenceHairColors);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceHairColors.GetAll();

            Assert.That(result.Count, Is.EqualTo(referenceHairColors.Count - removeCount));
        }

        [Test]
        public void RemoveRange_TwoValidHairColorsDuplicated_ThrowsInvalidOperationException()
        {
            var randomHairColorCode = PreDefinedData.GetRandomHairColorCode();
            var newHairColors = new Collection<ReferenceHairColor>
            {
                new ReferenceHairColor() { Code = randomHairColorCode },
                new ReferenceHairColor() { Code = randomHairColorCode }
            };

            Assert.That(() => UnitOfWork.ReferenceHairColors.RemoveRange(newHairColors),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedHairColors_DbUpdateConcurrencyException()
        {
            var removeReferenceHairColors = new Collection<ReferenceHairColor>
            {
                new ReferenceHairColor(),
                new ReferenceHairColor()
            };

            UnitOfWork.ReferenceHairColors.RemoveRange(removeReferenceHairColors);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }
    }
}
