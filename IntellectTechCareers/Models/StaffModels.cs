using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntellectTechCareers.Models
{
    public class Job
    {
        [Display(Name = "Job Description")]
        public string JobDesc { get; set; }

        [Display(Name = "Job Role")]
        public string JobRole { get; set; }
        public IEnumerable<JobRole> JobRoles { get; set; }

        [Display(Name = "Skills")]
        public List<Skill> Skills { get; set; }

        [Display(Name = "Vacancies")]
        public int Vacancies { get; set; }

        [Display(Name = "Min Experience")]
        public int MinExperience { get; set; }

        [Display(Name = "Max Experience")]
        public int MaxExperience { get; set; }

        [Display(Name = "Age Limit")]
        public int AgeLimit { get; set; }
    }

    public class Staff
    {
        [Display(Name = "Staff ID")]
        public int StaffId { get; set; }

        [Display(Name = "Username")]
        [Required]
        public string StaffUserName { get; set; }

        [Display(Name = "Staff Name")]
        [Required]
        public string StaffName { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression("(?=.*\\d)(?=.*[a-zA-Z]).{1,}", ErrorMessage = "Password must have at least 1 digit and 1 alphabet")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        //TODO : Must have one digit and one alphabet
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Right to schedule")]
        public bool RightToSchedule { get; set; }

        [Display(Name = "Right to publish")]
        public bool RightToPublish { get; set; }
        
        [Display(Name = "Right to post")]
        public bool RightToPost { get; set; }
    }
}