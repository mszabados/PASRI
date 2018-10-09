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
            var randomHairColorId = PreDefinedData.GetRandomHairColorId();

            var result = UnitOfWork.ReferenceHairColors.Get(randomHairColorId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(randomHairColorId));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidHairColorCode_ReturnsNull(string invalidHairColorCode)
        {
            var result = UnitOfWork.ReferenceHairColors.Find(p => p.Code == invalidHairColorCode);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Find_PredicateUsedToFindOneHairColor_ReturnsCollection()
        {
            var randomHairColorId = PreDefinedData.GetRandomHairColorId();

            var result = UnitOfWork.ReferenceHairColors.Find(p => p.Id == randomHairColorId);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id == randomHairColorId);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneHairColor_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceHairColors.Find(p => p.Id != int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Find_PredicateUsedToFindNoHairColors_ReturnsEmptyCollection()
        {
            var result = UnitOfWork.ReferenceHairColors.Find(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneHairColor_ReturnsOneHairColor()
        {
            var randomHairColorId = PreDefinedData.GetRandomHairColorId();

            var result = UnitOfWork.ReferenceHairColors.SingleOrDefault(p => p.Id == randomHairColorId);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneHairColor_ThrowsInvalidOperationException()
        {
            var randomHairColorId = PreDefinedData.GetRandomHairColorId();

            Assert.That(() =>
                UnitOfWork.ReferenceHairColors.SingleOrDefault(p => p.Id != randomHairColorId),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoHairColors_ReturnsNull()
        {
            var result = UnitOfWork.ReferenceHairColors.SingleOrDefault(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidHairColorNotExists_FetchNewHairColor()
        {
            var notExistsHairColorCode = PreDefinedData.GetNotExistsHairColorCode();
            var newReferenceHairColor = new ReferenceHairColor
            {
                Code = notExistsHairColorCode,
                LongName = notExistsHairColorCode
            };

            UnitOfWork.ReferenceHairColors.Add(newReferenceHairColor);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceHairColors.Get(newReferenceHairColor.Id);

            Assert.That(result, Is.Not.Null);

            AssertHelper.AreObjectsEqual(newReferenceHairColor, result);
        }

        [Test]
        public void Add_ValidHairColorExists_ThrowsInvalidOperationException()
        {
            var randomHairColorId = PreDefinedData.GetRandomHairColorId();
            var randomHairColor = UnitOfWork.ReferenceHairColors.Get(randomHairColorId);

            Assert.That(() => UnitOfWork.ReferenceHairColors.Add(
                new ReferenceHairColor
                {
                    Id = randomHairColor.Id,
                    Code = randomHairColor.Code
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
                new ReferenceHairColor { Code = notExistsHairColorCode1, LongName = "" },
                new ReferenceHairColor { Code = notExistsHairColorCode2, LongName = "" }
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
                new ReferenceHairColor { Id = int.MaxValue, Code = notExistsHairColorCode, LongName = "" },
                new ReferenceHairColor { Id = int.MaxValue, Code = notExistsHairColorCode, LongName = "" }
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
            UnitOfWork.ReferenceHairColors.Remove(
                new ReferenceHairColor
                {
                    Id = int.MaxValue
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidHairColorExists_HairColorCannotBeFetched()
        {
            var randomHairColorId = PreDefinedData.GetRandomHairColorId();
            var removeReferenceHairColor = UnitOfWork.ReferenceHairColors.Get(randomHairColorId);
            UnitOfWork.ReferenceHairColors.Remove(removeReferenceHairColor);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceHairColors.Get(removeReferenceHairColor.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidHairColor_ThrowsInvalidOperationException()
        {
            Assert.That(() => UnitOfWork.ReferenceHairColors.Remove(new ReferenceHairColor()),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceHairColors = UnitOfWork.ReferenceHairColors.GetAll().ToList();
            var removeReferenceHairColors = new Collection<ReferenceHairColor>();
            var removeCount = new Random().Next(1, referenceHairColors.Count);

            for (var i = 0; i < removeCount; i++)
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
            var randomHairColorId = PreDefinedData.GetRandomHairColorId();
            var randomHairColor = UnitOfWork.ReferenceHairColors.Get(randomHairColorId);

            var existingHairColors = new Collection<ReferenceHairColor>
            {
                new ReferenceHairColor { Id = randomHairColor.Id, Code = randomHairColor.Code },
                new ReferenceHairColor { Id = randomHairColor.Id, Code = randomHairColor.Code }
            };

            Assert.That(() => UnitOfWork.ReferenceHairColors.RemoveRange(existingHairColors),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedHairColors_InvalidOperationException()
        {
            var removeReferenceHairColors = new Collection<ReferenceHairColor>
            {
                new ReferenceHairColor(),
                new ReferenceHairColor()
            };

            Assert.That(() => UnitOfWork.ReferenceHairColors.RemoveRange(removeReferenceHairColors),
                Throws.InvalidOperationException);
        }
    }
}
