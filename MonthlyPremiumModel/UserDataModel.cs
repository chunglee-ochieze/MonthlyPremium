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
    /// <summary>
    /// Object model of the User data. Age gets calculated from Date of birth, upon object instantiation.
    /// </summary>
    public class UserDataModel
    {
        [Required(ErrorMessage = "Name is required."), DataType(DataType.Text), RegularExpression("^([a-zA-Z]{2,}\\s[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\\s?([a-zA-Z]{1,})?)", ErrorMessage = "First and Last Names required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date Of Birth is required."), DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        [ValidDate]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Occupation is required."), DataType(DataType.Text), RegularExpression("White Collar|Professional|Light Manual|Heavy Manual", ErrorMessage = "Incorrect Occupation.")]
        [Display(Name = "Occupation")]
        public string OccupationRating { get; set; }

        [Required(ErrorMessage = "Sum Insured is required."), DataType(DataType.Currency)]
        [Display(Name = "Sum Insured")]
        [ValidAmount]
        public double CoverAmount { get; set; }

        public double Age => DateTime.Now.Subtract(DateOfBirth).TotalDays / 365.25;
        public double MonthlyPremium { get; set; }
    }
}
