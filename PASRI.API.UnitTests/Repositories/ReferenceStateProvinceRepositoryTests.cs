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
    public class ReferenceStateProvinceRepositoryTests : BaseUnitTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceStateProvinces.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceStateProvinces.Length));
        }

        [Test]
        public void Get_ValidStateProvinceCode_ReturnsSingleStateProvince()
        {
            var randomStateProvinceId = PreDefinedData.GetRandomStateProvinceId();

            var result = UnitOfWork.ReferenceStateProvinces.Get(randomStateProvinceId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(randomStateProvinceId));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(AssertHelper.Alphabet)]
        public void Get_InvalidStateProvinceCode_ReturnsNull(string invalidStateProvinceCode)
        {
            var result = UnitOfWork.ReferenceStateProvinces.Find(p => p.Code == invalidStateProvinceCode);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Find_PredicateUsedToFindOneStateProvince_ReturnsCollection()
        {
            var randomStateProvinceId = PreDefinedData.GetRandomStateProvinceId();

            var result = UnitOfWork.ReferenceStateProvinces.Find(p => p.Id == randomStateProvinceId);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id == randomStateProvinceId);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneStateProvince_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceStateProvinces.Find(p => p.Id != int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Find_PredicateUsedToFindNoStateProvinces_ReturnsEmptyCollection()
        {
            var result = UnitOfWork.ReferenceStateProvinces.Find(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneStateProvince_ReturnsOneStateProvince()
        {
            var randomStateProvinceId = PreDefinedData.GetRandomStateProvinceId();

            var result = UnitOfWork.ReferenceStateProvinces.SingleOrDefault(p => p.Id == randomStateProvinceId);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneStateProvince_ThrowsInvalidOperationException()
        {
            var randomStateProvinceId = PreDefinedData.GetRandomStateProvinceId();

            Assert.That(() =>
                UnitOfWork.ReferenceStateProvinces.SingleOrDefault(p => p.Id != randomStateProvinceId),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoStateProvinces_ReturnsNull()
        {
            var result = UnitOfWork.ReferenceStateProvinces.SingleOrDefault(p => p.Id == int.MaxValue);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_ValidStateProvinceNotExists_FetchNewStateProvince()
        {
            var notExistsStateProvinceCode = PreDefinedData.GetNotExistsStateProvinceCode();
            var newReferenceStateProvince = new ReferenceStateProvince
            {
                Code = notExistsStateProvinceCode,
                LongName = notExistsStateProvinceCode
            };

            UnitOfWork.ReferenceStateProvinces.Add(newReferenceStateProvince);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceStateProvinces.Get(newReferenceStateProvince.Id);

            Assert.That(result, Is.Not.Null);

            AssertHelper.AreObjectsEqual(newReferenceStateProvince, result);
        }

        [Test]
        public void Add_ValidStateProvinceExists_ThrowsInvalidOperationException()
        {
            var randomStateProvinceId = PreDefinedData.GetRandomStateProvinceId();
            var randomStateProvince = UnitOfWork.ReferenceStateProvinces.Get(randomStateProvinceId);

            Assert.That(() => UnitOfWork.ReferenceStateProvinces.Add(
                new ReferenceStateProvince
                {
                    Id = randomStateProvince.Id,
                    Code = randomStateProvince.Code
                }),
                Throws.InvalidOperationException);
        }

        [Test]
        public void Add_InvalidStateProvince_ThrowsDbUpdateException()
        {
            UnitOfWork.ReferenceStateProvinces.Add(new ReferenceStateProvince());

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void AddRange_TwoValidStateProvinces_CountIncreasedByTwo()
        {
        Start:
            var notExistsStateProvinceCode1 = PreDefinedData.GetNotExistsStateProvinceCode();
            var notExistsStateProvinceCode2 = PreDefinedData.GetNotExistsStateProvinceCode();
            if (notExistsStateProvinceCode1 == notExistsStateProvinceCode2)
                goto Start;

            var newStateProvinces = new Collection<ReferenceStateProvince>
            {
                new ReferenceStateProvince { Code = notExistsStateProvinceCode1, LongName = "" },
                new ReferenceStateProvince { Code = notExistsStateProvinceCode2, LongName = "" }
            };
            UnitOfWork.ReferenceStateProvinces.AddRange(newStateProvinces);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceStateProvinces.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.ReferenceStateProvinces.Length + newStateProvinces.Count));
        }

        [Test]
        public void AddRange_TwoValidStateProvincesDuplicated_ThrowsInvalidOperationException()
        {
            var notExistsStateProvinceCode = PreDefinedData.GetNotExistsStateProvinceCode();
            var newStateProvinces = new Collection<ReferenceStateProvince>
            {
                new ReferenceStateProvince { Id = int.MaxValue, Code = notExistsStateProvinceCode, LongName = "" },
                new ReferenceStateProvince { Id = int.MaxValue, Code = notExistsStateProvinceCode, LongName = "" }
            };

            Assert.That(() => UnitOfWork.ReferenceStateProvinces.AddRange(newStateProvinces),
                Throws.InvalidOperationException);
        }

        [Test]
        public void AddRange_TwoMalformedStateProvinces_ThrowsDbUpdateException()
        {
            var newStateProvinces = new Collection<ReferenceStateProvince>
            {
                new ReferenceStateProvince(),
                new ReferenceStateProvince()
            };
            UnitOfWork.ReferenceStateProvinces.AddRange(newStateProvinces);

            Assert.That(() =>
                UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateException>());
        }

        [Test]
        public void Remove_ValidStateProvinceNotExists_ThrowsDbUpdateConcurrencyException()
        {
            UnitOfWork.ReferenceStateProvinces.Remove(
                new ReferenceStateProvince
                {
                    Id = int.MaxValue
                });

            Assert.That(() => UnitOfWork.Complete(),
                Throws.TypeOf<DbUpdateConcurrencyException>());
        }

        [Test]
        public void Remove_ValidStateProvinceExists_StateProvinceCannotBeFetched()
        {
            var randomStateProvinceId = PreDefinedData.GetRandomStateProvinceId();
            var removeReferenceStateProvince = UnitOfWork.ReferenceStateProvinces.Get(randomStateProvinceId);
            UnitOfWork.ReferenceStateProvinces.Remove(removeReferenceStateProvince);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceStateProvinces.Get(removeReferenceStateProvince.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Remove_InvalidStateProvince_ThrowsInvalidOperationException()
        {
            Assert.That(() => UnitOfWork.ReferenceStateProvinces.Remove(new ReferenceStateProvince()),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_RandomCount_CalculatedCountRemains()
        {
            var referenceStateProvinces = UnitOfWork.ReferenceStateProvinces.GetAll().ToList();
            var removeReferenceStateProvinces = new Collection<ReferenceStateProvince>();
            var removeCount = new Random().Next(1, referenceStateProvinces.Count);

            for (var i = 0; i < removeCount; i++)
            {
                removeReferenceStateProvinces.Add(referenceStateProvinces.ElementAt(i));
            }

            UnitOfWork.ReferenceStateProvinces.RemoveRange(removeReferenceStateProvinces);
            UnitOfWork.Complete();

            var result = UnitOfWork.ReferenceStateProvinces.GetAll();

            Assert.That(result.Count, Is.EqualTo(referenceStateProvinces.Count - removeCount));
        }

        [Test]
        public void RemoveRange_TwoValidStateProvincesDuplicated_ThrowsInvalidOperationException()
        {
            var randomStateProvinceId = PreDefinedData.GetRandomStateProvinceId();
            var randomStateProvince = UnitOfWork.ReferenceStateProvinces.Get(randomStateProvinceId);

            var existingStateProvinces = new Collection<ReferenceStateProvince>
            {
                new ReferenceStateProvince { Id = randomStateProvince.Id, Code = randomStateProvince.Code },
                new ReferenceStateProvince { Id = randomStateProvince.Id, Code = randomStateProvince.Code }
            };

            Assert.That(() => UnitOfWork.ReferenceStateProvinces.RemoveRange(existingStateProvinces),
                Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveRange_TwoMalformedStateProvinces_InvalidOperationException()
        {
            var removeReferenceStateProvinces = new Collection<ReferenceStateProvince>
            {
                new ReferenceStateProvince(),
                new ReferenceStateProvince()
            };

            Assert.That(() => UnitOfWork.ReferenceStateProvinces.RemoveRange(removeReferenceStateProvinces),
                Throws.InvalidOperationException);
        }
    }
}
