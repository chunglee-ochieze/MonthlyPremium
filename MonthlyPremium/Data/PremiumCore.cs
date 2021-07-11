using MonthlyPremium.Interface;
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

        public double DeathPremium(string occupationRating, double age, double coverAmount)
        {
            var premium = 0D;

            try
            {
                var ratingFactors = _configMgr.RatingFactor();

                var ratingFactor = occupationRating.ToLower() switch
                {
                    "professional" => ratingFactors.Professional,
                    "whitecollar" => ratingFactors.WhiteCollar,
                    "lightmanual" => ratingFactors.LightManual,
                    "heavymanual" => ratingFactors.HeavyManual,
                    _ => throw new NotImplementedException($"OccupationRating {occupationRating} not implemented.")
                };

                premium = ((coverAmount * ratingFactor * age) / 1000) * 12;

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