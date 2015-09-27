using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntellectTechCareers.Models
{
    public class InterviewerModel
    {
        [Required]
        [StringLength(15, ErrorMessage = "The {0} should be between 6 and 15 characters long.", MinimumLength = 6)]
        [RegularExpression("^[a-zA-Z_]*$", ErrorMessage = "User name can only have Alphabets and Underscore")]
        [Display(Name = "User name*")]
        public string UserName { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression("(?=.*\\d)(?=.*[a-zA-Z]).{1,}", ErrorMessage = "Password must have at least 1 digit and 1 alphabet")]
        [DataType(DataType.Password)]
        [Display(Name = "Password*")]
        //TODO : Must have one digit and one alphabet
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Name cannot be greater than 30 characters. ")]
        [Display(Name = "Name*")]
        public string Name { get; set; }

        [Display(Name = "Jobs*")]
        public List<JobSelectModel> Jobs { get; set; }
    }
}