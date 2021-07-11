using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonthlyPremium.Data;
using MonthlyPremiumModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionTests
{
    [TestClass]
    public class PremiumCoreTests
    {
        [TestMethod]
        public void CalculateSavePremiumTest()
        {
            var mpm = new UserDataModel
            {
                Occupation = "Professional",
                DateOfBirth = new DateTime(1975, 10, 1),
                CoverAmount = 10000
            };
            var premium = new PremiumCore().CalculateSavePremium(mpm);

            Assert.AreNotEqual(0, premium);
        }
    }

    [TestClass]
    public class UserDataModelTests
    {
        [TestMethod]
        public void AgeCalculationTest()
        {
            var user = new UserDataModel
            {
                DateOfBirth = new DateTime(2020, 07, 11) //1 year ago
            };

            Assert.AreEqual(1, Math.Round(user.Age, MidpointRounding.AwayFromZero));
        }
    }
}