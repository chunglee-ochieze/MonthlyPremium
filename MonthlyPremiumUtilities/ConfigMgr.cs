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

        public LoggerModel Logger() => new()
        {
            LogPath = _conf["Logger:Path"],
            LogSize = Convert.ToInt64(_conf["Logger:Size"]) * 1048576
        };

        public Dictionary<string, double> RatingFactors() => new()
        {
            { "Professional", _conf.GetValue<double>("RatingFactors:Professional") },
            { "WhiteCollar", _conf.GetValue<double>("RatingFactors:WhiteCollar") },
            { "LightManual", _conf.GetValue<double>("RatingFactors:LightManual") },
            { "HeavyManual", _conf.GetValue<double>("RatingFactors:HeavyManual") }
        };
    }
}