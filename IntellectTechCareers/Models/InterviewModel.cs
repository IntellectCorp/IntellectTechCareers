using IntellectTechCareers.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntellectTechCareers.Models
{
    public class InterviewModel
    {
        [Display(Name = "Job ID")]
        public int JobId { get; set; }

        [Display(Name = "Job Description")]
        public string JobDesc { get; set; }

        [Display(Name = "Date*")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Display(Name = "Time")]
        public Time24hrFormat Time { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Venue*")]
        public string Venue { get; set; }

    }
}