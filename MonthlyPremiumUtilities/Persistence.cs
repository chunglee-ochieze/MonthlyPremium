using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyPremiumUtilities
{
    public class Persistence
    {
        private readonly Logger _logger;
        private readonly string _dataFile;

        public Persistence()
        {
            _logger = new Logger();
            _dataFile = new ConfigMgr().DataFile();
        }

        /// <summary>
        /// A function that prepares the data file for persisting user details.
        /// </summary>
        public void PrepareDataFile()
        {
            try
            {
                using var fs = File.Create(_dataFile);
                fs.Close();
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception error occurred: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// A function that commits user details into the data file.
        /// </summary>
        /// <param name="data">A string parameter containing user details</param>
        /// <returns></returns>
        public async Task WriteDataToFile(string data)
        {
            try
            {
                await using var sw = File.AppendText(_dataFile);
                await sw.WriteLineAsync(data);
                await sw.FlushAsync();
                sw.Close();
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception error occurred: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// A function that reads string data from data file.
        /// </summary>
        /// <returns>A list of strings</returns>
        public async Task<List<string>> ReadDataFromFile()
        {
            List<string> lines;

            try
            {
                using var fr = File.ReadAllLinesAsync(_dataFile);
                lines = (await fr).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception error occurred: {ex.Message}", ex);
                lines = null;
            }

            return lines;
        }
    }
}
