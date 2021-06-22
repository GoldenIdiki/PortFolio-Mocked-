using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoldPortFolio.ViewModels
{
    public class RegisterViewModel
    {
        // Profile
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [StringLength(11)]
        public string PhoneNumber { get; set; }
        public string Picture { get; set; }
        [Required]
        public string Profession { get; set; }
        [Required]
        public string Qualification { get; set; }
        public string LinkedInUrl { get; set; }
        public string GitHubUrl { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
        public string ConfirmPassword { get; set; }

        // Address
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        // Work Experience
        [DataType(DataType.Date)]
        public DateTime YearStarted { get; set; }
        [DataType(DataType.Date)]
        public DateTime YearEnded { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
    }
}
