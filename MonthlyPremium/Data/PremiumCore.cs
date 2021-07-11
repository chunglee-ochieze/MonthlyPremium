using MonthlyPremium.Interface;
using MonthlyPremiumModel;
using MonthlyPremiumUtilities;
using System;
using System.Collections.Generic;
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

        public double CalculatePremium(UserDataModel user)
        {
            var premium = 0D;

            try
            {
                var ratingFactors = _configMgr.RatingFactors();

                var ratingFactor = ratingFactors.FirstOrDefault(r => r.Key.ToLower().Equals(user.OccupationRating.ToLower())).Value;

                premium = ((user.CoverAmount * ratingFactor * user.Age) / 1000) * 12;

                _logger.Information($"Monthly Premium successfully calculated: {premium}");

                user.MonthlyPremium = premium;

                _ = SavePremium(JsonConvert.SerializeObject(user));
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception error occurred: {ex.Message}", ex);
            }

            return premium;
        }

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
    }
}