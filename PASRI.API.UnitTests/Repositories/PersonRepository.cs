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
    public class PersonRepositoryTests : BaseUnitTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.Persons.GetAll();

            Assert.That(result.Count, Is.EqualTo(PreDefinedData.Persons.Length));
        }

        [Test]
        public void Get_ValidPersonId_ReturnsSinglePerson()
        {
            var randomPersonId = PreDefinedData.GetRandomPersonId();

            var result = UnitOfWork.Persons.Get(randomPersonId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(randomPersonId));
        }

        [Test]
        public void GetEagerLoaded_ValidPersonId_ReturnsSinglePersonWithBirth()
        {
            var testPersonId = 1;

            var result = UnitOfWork.Persons.GetEagerLoadedPerson(testPersonId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(testPersonId));
            Assert.That(result.Birth.Date, Is.EqualTo(PreDefinedData.Births[testPersonId-1].Date));
        }

        [Test]
        [TestCase(null)]
        [TestCase(Int32.MaxValue)]
        public void Get_InvalidPersonId_ReturnsNull(Int32 invalidPersonId)
        {
            var result = UnitOfWork.Persons.Get(invalidPersonId);

            Assert.That(result, Is.Null);
        }

        [Test]
        [TestCase(null)]
        [TestCase(Int32.MaxValue)]
        public void GetEagerLoaded_InvalidPersonId_ReturnsNull(Int32 invalidPersonId)
        {
            var result = UnitOfWork.Persons.GetEagerLoadedPerson(invalidPersonId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Find_PredicateUsedToFindOnePerson_ReturnsCollection()
        {
            var randomPersonId = PreDefinedData.GetRandomPersonId();

            var result = UnitOfWork.Persons.Find(p => p.Id == randomPersonId);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.ElementAt(0).Id, Is.EqualTo(randomPersonId));
        }

        [Test]
        public void Find_PredicateUsedToFindMoreThanOnePerson_ReturnsCollection()
        {
            var result = UnitOfWork.Persons.Find(p => p.Id != Int32.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test]
        public void Find_PredicateUsedToFindNoPersons_ReturnsEmptyCollection()
        {
            var result = UnitOfWork.Persons.Find(p => p.Id == Int32.MaxValue);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindOnePerson_ReturnsOnePerson()
        {
            var randomPersonId = PreDefinedData.GetRandomPersonId();

            var result = UnitOfWork.Persons.SingleOrDefault(p => p.Id == randomPersonId);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedToFindMoreOnePerson_ThrowsInvalidOperationException()
        {
            var randomPersonId = PreDefinedData.GetRandomPersonId();

            Assert.That(() =>
                UnitOfWork.Persons.SingleOrDefault(p => p.Id != randomPersonId),
                Throws.InvalidOperationException);
        }

        [Test]
        public void SingleOrDefault_PredicateUsedOnToFindNoPersons_ReturnsNull()
        {
            var result = UnitOfWork.Persons.SingleOrDefault(p => p.Id == Int32.MaxValue);

            Assert.That(result, Is.Null);
        }
    }
}
