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
    public class ReferenceEyeColorRepositoryTests : BaseUnitTestProvider
    {
        /// <summary>
        /// Helper method to retrieve a eye color code, which is the primary key
        /// of the <see cref="ReferenceEyeColor"/> that does not exist in the
        /// <see cref="PreDefinedData.ReferenceEyeColors"/> test collection
        /// </summary>
        private static string GetNotExistsEyeColorCode() =>
            AssertHelper.GetValueNotInArray(PreDefinedData.ReferenceEyeColors,
                "Code", 2);

        /// <summary>
        /// Helper method to retrieve a eye color code, which is the primary key
        /// of the <see cref="ReferenceEyeColor"/> that exists in the
        /// <see cref="PreDefinedData.ReferenceEyeColors"/> test collection
        /// </summary>
        private static string GetRandomEyeColorCode() =>
            PreDefinedData.ReferenceEyeColors[
                new Random().Next(0, PreDefinedData.ReferenceEyeColors.Length)
            ].Code;

        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceEyeColors.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceEyeColors.Length));
        }

        [Test]
        public void Get_ValidEyeColorCode_ReturnsSingleEyeColor()
        {
            var randomEyeColorCode = GetRandomEyeColorCode();
            var result = UnitOfWork.ReferenceEyeColors.Get(randomEyeColorCode);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Code, Is.EqualTo(randomEyeColorCode));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidEyeColorCode_ReturnsNull(string invalidEyeColorCode)
        {
            var result = UnitOfWork.ReferenceEyeColors.Get(invalidEyeColorCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Find_PredicateUsedToFindOneEyeColor_ReturnsCollection()
        {
            var randomEyeColorCode = GetRandomEyeColorCode();
            Expression<Func<ReferenceEyeColor, bool>> predicate =
                (p => p.Code == randomEyeColorCode);
            var result = UnitOfWork.ReferenceEyeColors.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ToList()[0].Code == randomEyeColorCode);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneEyeColor_ReturnsCollection()
        {
            var randomEyeColorCode = GetRandomEyeColorCode();
            Expression<Func<ReferenceEyeColor, bool>> predicate =
                (p => p.Code != randomEyeColorCode);
            var result = UnitOfWork.ReferenceEyeColors.Find(predicate);

            Assert.That(result.Count, Is.EqualTo(
                PreDefinedData.ReferenceEyeColors.Length - 1));
        }

        [Test]
        public void Find_PredicateUsedToFindNoEyeColors_ReturnsEmptyCollection()
        {
            var notExistsEyeColorCode = GetNotExistsEyeColorCode();
            Expression<Func<ReferenceEyeColor, bool>> predicate =
                (p => p.Code == notExistsEyeColorCode);
            var result = UnitOfWork.ReferenceEyeColors.Find(predicate);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneEyeColor_ReturnsOneEyeColor()
        {
            var randomEyeColorCode = GetRandomEyeColorCode();
            Expression<Func<ReferenceEyeColor, bool>> predicate =
                (p => p.Code == randomEyeColorCode);
            var result = UnitOfWork.ReferenceEyeColors.SingleOrDefault(predicate);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneEyeColor_ThrowsInvalidOperationException()
        {
            var randomEyeColorCode = GetRandomEyeColorCode();
            Expression<Func<ReferenceEyeColor, bool>> predicate =
                (p => p.Code != randomEyeColorCode);

            Assert.That(() =>
                UnitOfWork.ReferenceEyeColors.SingleOrDefault(predicate),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoEyeColors_ReturnsNull()
        {
            var notExistsEyeColorCode = GetNotExistsEyeColorCode();
            Expression<Func<ReferenceEyeColor, bool>> predicate =
                (p => p.Code == notExistsEyeColorCode);
            var result = UnitOfWork.ReferenceEyeColors.SingleOrDefault(predicate);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidEyeColorNotExists_FetchNewEyeColor()
        {
            var notExistsEyeColorCode = GetNotExistsEyeColorCode();
            var newReferenceEyeColor = new ReferenceEyeColor()
            {
                Code = notExistsEyeColorCode,
                DisplayText = notExistsEyeColorCode
            };

            UnitOfWork.ReferenceEyeColors.Add(newReferenceEyeColor);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceEyeColors.Get(notExistsEyeColorCode);

            Assert.That(result, Is.Not.Null);
            AssertHelper.AreObjectsEqual(newReferenceEyeColor, result);
        }

        [Test]
        public void Add_ValidEyeColorExists_ThrowsInvalidOperationException()
        {
            var randomEyeColorCode = GetRandomEyeColorCode();
            Assert.That(() => UnitOfWork.ReferenceEyeColors.Add(
                new ReferenceEyeColor()
                {
                    Code = randomEyeColorCode
                }),
                Throws.InvalidOperationException);
        }

        [Test]
        public void Add_InvalidEyeColor_ThrowsDbUpdateException()
        {
            UnitOfWork.ReferenceEyeColors.Add(new ReferenceEyeColor());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void AddRange_TwoValidEyeColors_CountIncreasedByTwo()
        {
        Start:
            var notExistsEyeColorCode1 = GetNotExistsEyeColorCode();
            var notExistsEyeColorCode2 = GetNotExistsEyeColorCode();
            if (notExistsEyeColorCode1 == notExistsEyeColorCode2)
                goto Start;

            var newEyeColors = new Collection<ReferenceEyeColor>
            {
                new ReferenceEyeColor() { Code = notExistsEyeColorCode1, DisplayText = "" },
                new ReferenceEyeColor() { Code = notExistsEyeColorCode2, DisplayText = "" }
            };
            UnitOfWork.ReferenceEyeColors.AddRange(newEyeColors);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceEyeColors.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceEyeColors.Length + newEyeColors.Count));
        }

        [Test]
        public void AddRange_TwoValidEyeColorsDuplicated_ThrowsInvalidOperationException()
        {
            var notExistsEyeColorCode = GetNotExistsEyeColorCode();
            var newEyeColors = new Collection<ReferenceEyeColor>
            {
                new ReferenceEyeColor() { Code = notExistsEyeColorCode, DisplayText = "" },
                new ReferenceEyeColor() { Code = notExistsEyeColorCode, DisplayText = "" }
            };

            Assert.That(() => UnitOfWork.ReferenceEyeColors.AddRange(newEyeColors),
                Throws.InvalidOperationException);
        }

        [Test]
        public void AddRange_TwoMalformedEyeColors_ThrowsDbUpdateException()
        {
            var newEyeColors = new Collection<ReferenceEyeColor>
            {
                new ReferenceEyeColor(),
                new ReferenceEyeColor()
            };
            UnitOfWork.ReferenceEyeColors.AddRange(newEyeColors);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void Remove_ValidEyeColorNotExists_ThrowsDbUpdateConcurrencyException()
        {
            var notExistsEyeColorCode = GetNotExistsEyeColorCode();
            UnitOfWork.ReferenceEyeColors.Remove(
                new ReferenceEyeColor()
                {
                    Code = notExistsEyeColorCode
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidEyeColorExists_EyeColorCannotBeFetched()
        {
            var randomEyeColorCode = GetRandomEyeColorCode();
            var removeReferenceEyeColor = UnitOfWork.ReferenceEyeColors.Get(randomEyeColorCode);
            UnitOfWork.ReferenceEyeColors.Remove(removeReferenceEyeColor);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceEyeColors.Get(randomEyeColorCode);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidEyeColor_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceEyeColors.Remove(new ReferenceEyeColor());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceEyeColors = UnitOfWork.ReferenceEyeColors.GetAll().ToList();
            var removeReferenceEyeColors = new Collection<ReferenceEyeColor>();
            var removeCount = new Random().Next(1, referenceEyeColors.Count);

            for (int i = 0; i < removeCount; i++)
            {
                removeReferenceEyeColors.Add(referenceEyeColors.ElementAt(i));
            }

            UnitOfWork.ReferenceEyeColors.RemoveRange(removeReferenceEyeColors);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceEyeColors.GetAll();

            Assert.That(result.Count, Is.EqualTo(referenceEyeColors.Count - removeCount));
        }

        [Test]
        public void RemoveRange_TwoValidEyeColorsDuplicated_ThrowsInvalidOperationException()
        {
            var randomEyeColorCode = GetRandomEyeColorCode();
            var newEyeColors = new Collection<ReferenceEyeColor>
            {
                new ReferenceEyeColor() { Code = randomEyeColorCode },
                new ReferenceEyeColor() { Code = randomEyeColorCode }
            };

            Assert.That(() => UnitOfWork.ReferenceEyeColors.RemoveRange(newEyeColors),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedEyeColors_DbUpdateConcurrencyException()
        {
            var removeReferenceEyeColors = new Collection<ReferenceEyeColor>
            {
                new ReferenceEyeColor(),
                new ReferenceEyeColor()
            };

            UnitOfWork.ReferenceEyeColors.RemoveRange(removeReferenceEyeColors);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }
    }
}
