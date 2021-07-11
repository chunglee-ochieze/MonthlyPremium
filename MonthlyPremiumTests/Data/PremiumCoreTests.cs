using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonthlyPremium.Data;
using MonthlyPremiumModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyPremium.Data.Tests
{
    [TestClass]
    public class PremiumCoreTests
    {
        [TestMethod]
        public void DeathPremiumTest()
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
}