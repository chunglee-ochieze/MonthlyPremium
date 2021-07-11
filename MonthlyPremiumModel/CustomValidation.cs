using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyPremiumModel
{
    public class ValidDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                return Convert.ToDateTime(value) switch
                {
                    var n when n <= DateTime.Now.Date => ValidationResult.Success,
                    _ => new ValidationResult($"Incorrect {validationContext.DisplayName}.")
                };
            }
            catch (Exception)
            {
                return new ValidationResult($"Incorrect {validationContext.DisplayName}.");
            }
        }
    }

    public class ValidAmountAttribute : ValidDateAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                return Convert.ToDouble(value) switch
                {
                    > 0 => ValidationResult.Success,
                    _ => new ValidationResult($"Incorrect {validationContext.DisplayName}.")
                };
            }
            catch (Exception)
            {
                return new ValidationResult($"Incorrect {validationContext.DisplayName}.");
            }
        }
    }
}
