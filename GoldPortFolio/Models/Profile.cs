using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoldPortFolio.Models
{
    public class Profile : IdentityUser
    {
        public Profile()
        {
            Address = new List<Address>();
            WorkExperience = new List<WorkExperience>();
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Qualification { get; set; }
        [Required]
        public string Profession { get; set; }
        public string Picture { get; set; }
        public string GitHubUrl { get; set; }
        public string LinkInUrl { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
        public string ConfirmPassword { get; set; }
        public List<WorkExperience> WorkExperience { get; set; }
        public List<Address> Address { get; set; }
    }
}
