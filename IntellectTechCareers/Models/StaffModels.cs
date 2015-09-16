using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntellectTechCareers.Models
{
    public class JobModel
    {
        [Display(Name = "Job Description")]
        public string JobDesc { get; set; }

        [Display(Name = "Job Role")]
        public string JobRole { get; set; }

        [Display(Name = "Skills")]
        public List<string> Skills { get; set; }

        [Display(Name = "Min Experience")]
        public string MinExperience { get; set; }

        [Display(Name = "Max Experience")]
        public string MaxExperience { get; set; }

        [Display(Name = "Age Limit")]
        public bool IsAgeLimit { get; set; }

        [Display(Name = "Enter the Age Limit")]
        public string AgeLimit { get; set; }
    }
}