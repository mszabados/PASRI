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
    public class ReferenceEyeColorRepositoryTests : BaseUnitTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceEyeColors.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceEyeColors.Length));
        }

        [Test]
        public void Get_ValidEyeColorCode_ReturnsSingleEyeColor()
        {
            var randomEyeColorId = PreDefinedData.GetRandomEyeColorId();

            var result = UnitOfWork.ReferenceEyeColors.Get(randomEyeColorId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(randomEyeColorId));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidEyeColorCode_ReturnsNull(string invalidEyeColorCode)
        {
            var result = UnitOfWork.ReferenceEyeColors.Find(p => p.Code == invalidEyeColorCode);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Find_PredicateUsedToFindOneEyeColor_ReturnsCollection()
        {
            var randomEyeColorId = PreDefinedData.GetRandomEyeColorId();

            var result = UnitOfWork.ReferenceEyeColors.Find(p => p.Id == randomEyeColorId);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id == randomEyeColorId);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneEyeColor_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceEyeColors.Find(p => p.Id != int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Find_PredicateUsedToFindNoEyeColors_ReturnsEmptyCollection()
        {
            var result = UnitOfWork.ReferenceEyeColors.Find(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneEyeColor_ReturnsOneEyeColor()
        {
            var randomEyeColorId = PreDefinedData.GetRandomEyeColorId();

            var result = UnitOfWork.ReferenceEyeColors.SingleOrDefault(p => p.Id == randomEyeColorId);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneEyeColor_ThrowsInvalidOperationException()
        {
            var randomEyeColorId = PreDefinedData.GetRandomEyeColorId();

            Assert.That(() =>
                UnitOfWork.ReferenceEyeColors.SingleOrDefault(p => p.Id != randomEyeColorId),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoEyeColors_ReturnsNull()
        {
            var result = UnitOfWork.ReferenceEyeColors.SingleOrDefault(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidEyeColorNotExists_FetchNewEyeColor()
        {
            var notExistsEyeColorCode = PreDefinedData.GetNotExistsEyeColorCode();
            var newReferenceEyeColor = new ReferenceEyeColor
            {
                Code = notExistsEyeColorCode,
                LongName = notExistsEyeColorCode
            };

            UnitOfWork.ReferenceEyeColors.Add(newReferenceEyeColor);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceEyeColors.Get(newReferenceEyeColor.Id);

            Assert.That(result, Is.Not.Null);

            AssertHelper.AreObjectsEqual(newReferenceEyeColor, result);
        }

        [Test]
        public void Add_ValidEyeColorExists_ThrowsInvalidOperationException()
        {
            var randomEyeColorId = PreDefinedData.GetRandomEyeColorId();
            var randomEyeColor = UnitOfWork.ReferenceEyeColors.Get(randomEyeColorId);

            Assert.That(() => UnitOfWork.ReferenceEyeColors.Add(
                new ReferenceEyeColor
                {
                    Id = randomEyeColor.Id,
                    Code = randomEyeColor.Code
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
            var notExistsEyeColorCode1 = PreDefinedData.GetNotExistsEyeColorCode();
            var notExistsEyeColorCode2 = PreDefinedData.GetNotExistsEyeColorCode();
            if (notExistsEyeColorCode1 == notExistsEyeColorCode2)
                goto Start;

            var newEyeColors = new Collection<ReferenceEyeColor>
            {
                new ReferenceEyeColor { Code = notExistsEyeColorCode1, LongName = "" },
                new ReferenceEyeColor { Code = notExistsEyeColorCode2, LongName = "" }
            };
            UnitOfWork.ReferenceEyeColors.AddRange(newEyeColors);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceEyeColors.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceEyeColors.Length + newEyeColors.Count));
        }

        [Test]
        public void AddRange_TwoValidEyeColorsDuplicated_ThrowsInvalidOperationException()
        {
            var notExistsEyeColorCode = PreDefinedData.GetNotExistsEyeColorCode();
            var newEyeColors = new Collection<ReferenceEyeColor>
            {
                new ReferenceEyeColor { Id = int.MaxValue, Code = notExistsEyeColorCode, LongName = "" },
                new ReferenceEyeColor { Id = int.MaxValue, Code = notExistsEyeColorCode, LongName = "" }
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
            UnitOfWork.ReferenceEyeColors.Remove(
                new ReferenceEyeColor
                {
                    Id = int.MaxValue
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidEyeColorExists_EyeColorCannotBeFetched()
        {
            var randomEyeColorId = PreDefinedData.GetRandomEyeColorId();
            var removeReferenceEyeColor = UnitOfWork.ReferenceEyeColors.Get(randomEyeColorId);
            UnitOfWork.ReferenceEyeColors.Remove(removeReferenceEyeColor);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceEyeColors.Get(removeReferenceEyeColor.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidEyeColor_ThrowsInvalidOperationException()
        {
            Assert.That(() => UnitOfWork.ReferenceEyeColors.Remove(new ReferenceEyeColor()),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceEyeColors = UnitOfWork.ReferenceEyeColors.GetAll().ToList();
            var removeReferenceEyeColors = new Collection<ReferenceEyeColor>();
            var removeCount = new Random().Next(1, referenceEyeColors.Count);

            for (var i = 0; i < removeCount; i++)
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
            var randomEyeColorId = PreDefinedData.GetRandomEyeColorId();
            var randomEyeColor = UnitOfWork.ReferenceEyeColors.Get(randomEyeColorId);

            var existingEyeColors = new Collection<ReferenceEyeColor>
            {
                new ReferenceEyeColor { Id = randomEyeColor.Id, Code = randomEyeColor.Code },
                new ReferenceEyeColor { Id = randomEyeColor.Id, Code = randomEyeColor.Code }
            };

            Assert.That(() => UnitOfWork.ReferenceEyeColors.RemoveRange(existingEyeColors),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedEyeColors_InvalidOperationException()
        {
            var removeReferenceEyeColors = new Collection<ReferenceEyeColor>
            {
                new ReferenceEyeColor(),
                new ReferenceEyeColor()
            };

            Assert.That(() => UnitOfWork.ReferenceEyeColors.RemoveRange(removeReferenceEyeColors),
                Throws.InvalidOperationException);
        }
    }
}
