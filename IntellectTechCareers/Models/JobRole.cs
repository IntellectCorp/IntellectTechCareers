using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntellectTechCareers.Models
{
    public class JobRole
    {
        public int Id { get; set; }
        
        [Display(Name = "Job Role")]
        public string Name { get; set; }
    }
}