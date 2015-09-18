using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntellectTechCareers.Models
{
    public class JobRoleModel
    {
        [Display(Name = "Job Description")]
        public string JobDesc { get; set; }

        [Display(Name = "Job Role")]
        public string JobRole { get; set; }
        public IEnumerable<JobRole> JobRoles { get; set; }

        [Display(Name = "Skills")]
        public List<string> Skills { get; set; }

        [Display(Name = "Min Experience")]
        public int MinExperience { get; set; }

        [Display(Name = "Max Experience")]
        public int MaxExperience { get; set; }

        [Display(Name = "Age Limit")]
        public int AgeLimit { get; set; }
    }
}