using MonthlyPremium.Interface;
using MonthlyPremiumModel;
using MonthlyPremiumUtilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MonthlyPremium.Data
{
    public class PremiumCore : IPremiumCore
    {
        private readonly Logger _logger;
        private readonly ConfigMgr _configMgr;

        public PremiumCore()
        {
            _logger = new Logger();
            _configMgr = new ConfigMgr();
        }

        /// <summary>
        /// A function that calculates monthly premium based on rating factors and formula.
        /// </summary>
        /// <param name="user">An object parameter containing user details</param>
        /// <returns>Result of the calculation</returns>
        public double CalculatePremium(UserDataModel user)
        {
            var premium = 0D;

            try
            {
                var ratingFactors = _configMgr.RatingFactors();

                var ratingFactor = ratingFactors.FirstOrDefault(r => r.Key.ToLower().Equals(user.OccupationRating.ToLower())).Value;

                premium = (user.CoverAmount * ratingFactor * user.Age) / 1000 * 12;

                _logger.Information($"Monthly Premium successfully calculated: {premium}");

                user.MonthlyPremium = premium;
                user.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.Name.ToLower());

                _ = SavePremium(JsonConvert.SerializeObject(user));
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception error occurred: {ex.Message}", ex);
            }

            return premium;
        }

        /// <summary>
        /// A function that persists a user data.
        /// </summary>
        /// <param name="data">A string parameter containing user details</param>
        /// <returns></returns>
        public async Task SavePremium(string data)
        {
            try
            {
                await new Persistence().WriteDataToFile(data);
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception error occurred: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// A function that retrieves details of all previously-calculated premiums.
        /// </summary>
        /// <returns>List of user details</returns>
        public async Task<List<UserDataModel>> ViewPremiums()
        {
            List<UserDataModel> userData;

            try
            {
                var fromFile = await new Persistence().ReadDataFromFile();

                userData = fromFile.Select(r => JsonConvert.DeserializeObject<UserDataModel>(r)).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception error occurred: {ex.Message}", ex);
                userData = new List<UserDataModel>();
            }

            return userData;
        }
    }
}