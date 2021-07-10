using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonthlyPremium.Data;
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
            var premium = new PremiumCore().DeathPremium("Professional", 40, 10000);

            Assert.AreNotEqual(0, premium);
        }
    }
}