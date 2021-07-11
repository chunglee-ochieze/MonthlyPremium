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
        /// <summary>
        /// Testing to confirm that CalculatePremium function does return a non-zero response, indicating that calculation was done.
        /// </summary>
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

        /// <summary>
        /// Testing to confirm that SavePremium function does save data.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task SavePremiumTest()
        {
            var test = "123456";

            await new PremiumCore().SavePremium(test);

            var fileContent = await File.ReadAllTextAsync(new PersistenceTests().DataFile);

            Assert.IsTrue(fileContent.Contains(test));
        }

        /// <summary>
        /// Testing to confirm that ViewPremiums function does return data.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ViewPremiumsTest()
        {
            var premiums = await new PremiumCore().ViewPremiums();

            Assert.AreEqual("ChungLee Ochieze", premiums.FirstOrDefault()?.Name);

            Assert.AreEqual(3, premiums.Count);
        }
    }

    [TestClass]
    public class UserDataModelTests
    {
        /// <summary>
        /// Testing to confirm that UserDataModel class does calculate Age appropriately, based on Date of Birth property
        /// </summary>
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

        /// <summary>
        /// Testing to confirm that prepared data file is empty and not holding data from previous sessions.
        /// </summary>
        [TestMethod]
        public void PrepareDataFileTest()
        {
            new Persistence().PrepareDataFile();

            Assert.AreEqual(true, File.Exists(DataFile));

            var fileSize = new FileInfo(DataFile).Length;

            Assert.AreEqual(0, fileSize);
        }

        /// <summary>
        /// Testing to confirm that data written into data file is retrievable and intact.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteDataToFileTestAsync()
        {
            var test = "ABCXYZ";

            await new Persistence().WriteDataToFile(test);

            var fileContent = await File.ReadAllTextAsync(DataFile);

            Assert.IsTrue(fileContent.Contains(test));
        }

        /// <summary>
        /// Testing to confirm that data can be retrieved from data file in desired format.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ReadDataFromFileTestAsync()
        {
            var lines = await new Persistence().ReadDataFromFile();

            Assert.AreEqual(3, lines.Count);
        }
    }
}