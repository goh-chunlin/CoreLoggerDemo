using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLoggerDemo.Models.DataEntryViewModels
{
    public class DataEntryViewModel
    {
        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string CreatedBy { get; set; }
    }
}
