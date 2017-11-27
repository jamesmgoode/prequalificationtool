using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrequalificationTool.Models
{
    public class CardApplicationViewModel
    {
        [Required]
        [Display(Name = "First name")]
        [StringLength(50, ErrorMessage = "Maximum of 50 characters")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "DOB")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }
        [Required]
        [Display(Name = "Annual income")]
        [Range(0, 1000000000000000)]
        public int AnnualIncome { get; set; }
    }
}
