using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonthlyPremium.Data;
using MonthlyPremiumModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonthlyPremiumUtilities;

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
                OccupationRating = "Professional",
                DateOfBirth = new DateTime(1975, 10, 1),
                CoverAmount = 10000
            };

            var premium = new PremiumCore().CalculatePremium(mpm);

            Assert.AreNotEqual(0, premium);
        }

        [TestMethod]
        public async Task SavePremiumTest()
        {
            var test = "123456";

            await new PremiumCore().SavePremium(test);

            var fileContent = await File.ReadAllTextAsync(new PersistenceTests().DataFile);

            Assert.IsTrue(fileContent.Contains(test));
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

    [TestClass]
    public class PersistenceTests
    {
        public readonly string DataFile;

        public PersistenceTests()
        {
            DataFile = new ConfigMgr().DataFile();
        }

        [TestMethod]
        public void PrepareDataFileTest()
        {
            new Persistence().PrepareDataFile();

            Assert.AreEqual(true, File.Exists(DataFile));

            var fileSize = new FileInfo(DataFile).Length;

            Assert.AreEqual(0, fileSize);
        }

        [TestMethod]
        public async Task WriteDataToFileTestAsync()
        {
            var test = "ABCXYZ";

            await new Persistence().WriteDataToFile(test);

            var fileContent = await File.ReadAllTextAsync(DataFile);

            Assert.IsTrue(fileContent.Contains(test));
        }
    }
}