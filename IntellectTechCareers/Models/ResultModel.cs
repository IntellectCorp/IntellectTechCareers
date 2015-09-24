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

        [Display(Name = "Job Description")]
        public string JobDesc { get; set; }

        [Display(Name = "Declaration Date")]
        public DateTime DeclrationDate { get; set; }

        [Display(Name = "Number of Candidates Selected")]
        public int numOfCandidatesSelected { get; set; }

        [Display(Name = "Candidates")]
        public List<CandidateResult> Candidates { get; set; }

        [Display(Name = "Vacancies")]
        public int Vacancies { get; set; }
    }

    public class CandidateResult
    {
        [Display(Name = "User ID")]
        public int UserID { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Is Selected")]
        public bool IsSelected { get; set; }

    }
}