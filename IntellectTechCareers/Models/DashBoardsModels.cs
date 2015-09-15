using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IntellectTechCareers.Models
{
    public class ManagerDashBoardModels
    {
        [Display(Name = "Job posted in total")]
        public string TotalJobs { get; set; }

        [Display(Name = "Job scheduled for interview in total")]
        public string TotalScheduledInterview { get; set; }

        [Display(Name = "Job results released in total")]
        public string TotalResults { get; set; }

        [Display(Name = "Job yet to be scheduled for interview")]
        public string UnscheduledInterviewJobs { get; set; }

        [Display(Name = "Job results yet to be released")]
        public string ResultsNotReleased { get; set; }
    }
}