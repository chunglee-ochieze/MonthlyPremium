using System;
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

        public RatingFactorModel RatingFactor() => new()
        {
            Professional = _conf.GetValue<decimal>("RatingFactor:Professional"),
            WhiteCollar = _conf.GetValue<decimal>("RatingFactor:WhiteCollar"),
            LightManual = _conf.GetValue<decimal>("RatingFactor:LightManual"),
            HeavyManual = _conf.GetValue<decimal>("RatingFactor:HeavyManual")
        };
    }
}