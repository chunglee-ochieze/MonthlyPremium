using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;
using MonthlyPremiumModel;

namespace MonthlyPremiumUtilities
{
    public class ConfigMgr
    {
        private readonly IConfigurationRoot _conf;
        public ConfigMgr()
        {
            _conf = new ConfigurationBuilder().AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json")).Build();
        }

        /// <summary>
        /// For retrieving config details for logging.
        /// </summary>
        /// <returns>An object of the logger parameters.</returns>
        public LoggerModel Logger() => new()
        {
            LogPath = _conf["Logger:Path"],
            LogSize = Convert.ToInt64(_conf["Logger:Size"]) * 1048576
        };

        /// <summary>
        /// For retrieving config details for Occupation Rating.
        /// </summary>
        /// <returns>A Dictionary of rating factors.</returns>
        public Dictionary<string, double> RatingFactors() => new()
        {
            { "Professional", _conf.GetValue<double>("RatingFactors:Professional") },
            { "White Collar", _conf.GetValue<double>("RatingFactors:WhiteCollar") },
            { "Light Manual", _conf.GetValue<double>("RatingFactors:LightManual") },
            { "Heavy Manual", _conf.GetValue<double>("RatingFactors:HeavyManual") }
        };

        /// <summary>
        /// For retrieving config detail of data file.
        /// </summary>
        /// <returns>A string of data file path.</returns>
        public string DataFile() => _conf["DataFile"];
    }
}