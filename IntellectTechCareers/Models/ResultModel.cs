using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntellectTechCareers.Models
{
    public class ResultModel
    {
        [Display(Name = "Job ID")]
        public int JobId { get; set; }

        [Display(Name = "Declaration Date")]
        public DateTime DeclrationDate { get; set; }

        [Display(Name = "Number of Candidates")]
        public int numOfCandidates { get; set; }

        [Display(Name = "Candidates Selected")]
        public List<int> CandidatesSelected { get; set; }

        [Display(Name = "Released By")]
        public int ReleasedBy { get; set; }
    }
}