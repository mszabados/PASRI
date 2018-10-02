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
    public class BirthRepositoryTests : BaseUnitTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.Births.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.Births.Length));
        }

        [Test]
        public void Get_ValidBirthById_ReturnsSingleBirth()
        {
            var randomBirthId = PreDefinedData.GetRandomBirthId();

            var result = UnitOfWork.Births.Get(randomBirthId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(randomBirthId));
        }

        [Test]
        [TestCase(null)]
        [TestCase(Int32.MaxValue)]
        public void Get_InvalidBirthById_ReturnsNull(Int32 invalidBirthId)
        {
            var result = UnitOfWork.Births.Get(invalidBirthId);

            Assert.That(result, Is.Null);
        }

        [Test]
        [TestCase(null)]
        [TestCase(Int32.MaxValue)]
        public void GetEagerLoaded_InvalidBirthById_ReturnsNull(Int32 invalidBirthId)
        {
            var result = UnitOfWork.Births.GetEagerLoadedBirthById(invalidBirthId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Get_ValidBirthByPersonId_ReturnsSingleBirth()
        {
            var randomPersonId = PreDefinedData.GetRandomPersonId();

            var result = UnitOfWork.Births.Find(b => b.PersonId == randomPersonId);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ElementAt(0).PersonId, Is.EqualTo(randomPersonId));
        }

        [Test]
        public void GetEagerLoaded_ValidBirthByPersonId_ReturnsSingleBirthWithBirth()
        {
            var testPersonId = 1;

            var result = UnitOfWork.Births.GetEagerLoadedBirthByPersonId(testPersonId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(testPersonId));
            Assert.That(result.Date, Is.EqualTo(PreDefinedData.Births[testPersonId - 1].Date));
        }

        [Test]
        [TestCase(null)]
        [TestCase(Int32.MaxValue)]
        public void Get_InvalidBirthByPersonId_ReturnsNull(Int32 invalidBirthId)
        {
            var result = UnitOfWork.Births.Get(invalidBirthId);

            Assert.That(result, Is.Null);
        }

        [Test]
        [TestCase(null)]
        [TestCase(Int32.MaxValue)]
        public void GetEagerLoaded_InvalidBirthId_ReturnsNull(Int32 invalidBirthId)
        {
            var result = UnitOfWork.Births.GetEagerLoadedBirthByPersonId(invalidBirthId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Find_PredicateUsedToFindOneBirth_ReturnsCollection()
        {
            var randomBirthId = PreDefinedData.GetRandomBirthId();

            var result = UnitOfWork.Births.Find(p => p.Id == randomBirthId);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ElementAt(0).Id == randomBirthId);
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOneBirth_ReturnsCollection()
        {
            var result = UnitOfWork.Births.Find(p => p.Id != Int32.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Find_PredicateUsedToFindNoBirths_ReturnsEmptyCollection()
        {
            var result = UnitOfWork.Births.Find(p => p.Id == Int32.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOneBirth_ReturnsOneBirth()
        {
            var randomBirthId = PreDefinedData.GetRandomBirthId();

            var result = UnitOfWork.Births.SingleOrDefault(p => p.Id == randomBirthId);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOneBirth_ThrowsInvalidOperationException()
        {
            var randomBirthId = PreDefinedData.GetRandomBirthId();

            Assert.That(() =>
                UnitOfWork.Births.SingleOrDefault(p => p.Id != randomBirthId),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoBirths_ReturnsNull()
        {
            var result = UnitOfWork.Births.SingleOrDefault(p => p.Id == Int32.MaxValue);

            Assert.That(result, Is.Null);
        }
    }
}
