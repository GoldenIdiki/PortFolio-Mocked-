using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoldPortFolio.Models
{
    public class ContactPage
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Please input your name")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Please this field is required")]
        public string Message { get; set; }
    }
}
