using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MonthlyPremiumModel
{
    public class MonthlyPremiumModel
    {
        [Required(ErrorMessage = "Name is required."), DataType(DataType.Text), RegularExpression("^([a-zA-Z]{2,}\\s[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\\s?([a-zA-Z]{1,})?)", ErrorMessage = "First and Last Names required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date Of Birth is required."), DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        [ValidDate]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Occupation is required."), DataType(DataType.Text), RegularExpression("whitecollar|professional|lightmanual|heavymanual", ErrorMessage = "Incorrect Occupation.")]
        public string Occupation { get; set; }

        [Required(ErrorMessage = "Sum Insured is required."), DataType(DataType.Currency)]
        [Display(Name = "Sum Insured")]
        [ValidAmount]
        public double CoverAmount { get; set; }

        public double Age { get; set; }

        public MonthlyPremiumModel()
        {
            Age = DateTime.Now.Subtract(DateOfBirth).TotalDays / 365.25;
        }
    }
}
