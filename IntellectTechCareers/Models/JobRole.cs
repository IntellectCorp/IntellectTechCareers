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
        [Required(ErrorMessage="Required Field")]
        [StringLength(25, ErrorMessage = "Maximum Length Allowed is 25.")]
        public string Name { get; set; }
    }
}