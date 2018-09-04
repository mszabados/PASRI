using System;
using System.Linq;
using NUnit.Framework;
using PASRI.API.Core.Domain;

namespace PASRI.API.UnitTests
{
    [TestFixture]
    public class ReferenceCountryRepositoryTests : BaseTestProvider
    {
        [Test]
        public void GetAll_WhenCalled_ReturnsCollection()
        {
            var result = UnitOfWork.ReferenceCountries.GetAll();
            
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void Get_ValidCountryCode_ReturnsSingleCountry()
        {
            var validCountryCode = "US";
            var result = UnitOfWork.ReferenceCountries.Get(validCountryCode);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Code, Is.EqualTo(validCountryCode));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("ZZ")]
        public void Get_InvalidCountryCode_ReturnsNull(string invalidCountryCode)
        {
            var result = UnitOfWork.ReferenceCountries.Get(invalidCountryCode);

            Assert.That(result, Is.Null);
        }

        protected override void LoadTestData()
        {
            PasriDbContext.ReferenceCountries.AddRange(
                new ReferenceCountry { Code = "US", DisplayText = "United States of America", StartDate = DateTime.Parse("7/4/1776") }, 
                new ReferenceCountry { Code = "CA", DisplayText = "Canada" }, 
                new ReferenceCountry { Code = "MX", DisplayText = "Mexico" }
                );
        }
    }
}
