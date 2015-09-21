using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntellectTechCareers.Models
{
    public class ApplicationModel
    {
        [Display(Name = "Application ID")]
        public int AppId { get; set; }

        [Display(Name = "Candidate ID")]
        public int CandidateId { get; set; }

        [Display(Name = "Job ID")]
        public int JobId { get; set; }

        [Display(Name = "Status Code")]
        public string StatusCode { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Application Date")]
        public DateTime Date { get; set; }
    }

    public class ApplicantListModel
    {
        [Display(Name = "Candidate")]
        public int CandidateId { get; set; }
        public List<int> CandidateIdList { get; set; }
    }
}