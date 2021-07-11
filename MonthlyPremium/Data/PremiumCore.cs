using MonthlyPremium.Interface;
using MonthlyPremiumModel;
using MonthlyPremiumUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public double CalculateSavePremium(UserDataModel user)
        {
            var premium = 0D;

            try
            {
                var ratingFactors = _configMgr.RatingFactors();

                var ratingFactor = ratingFactors.FirstOrDefault(r => r.Key.ToLower().Equals(user.Occupation.ToLower())).Value;

                premium = ((user.CoverAmount * ratingFactor * user.Age) / 1000) * 12;

                _logger.Information($"Monthly Premium successfully calculated: {premium}");
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception error occurred: {ex.Message}", ex);
            }

            return premium;
        }
    }
}