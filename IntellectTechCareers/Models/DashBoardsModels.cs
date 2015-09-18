using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IntellectTechCareers.Models
{
    public class ManagerDashBoardModel
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

    public class StaffDashBoardModel
    {
        [Display(Name = "Right to Post Job ")]
        public string RightToPost { get; set; }

        [Display(Name = "Right to Schedule Interview ")]
        public string RightToScheduleInterview { get; set; }

        [Display(Name = "Right to Release Results ")]
        public string RightToReleaseResults { get; set; }

        [Display(Name = "Job Posted by You")]
        public int JobPosted { get; set; }

        [Display(Name = "Job Scheduled for Interview by You")]
        public int InterviewsScheduled { get; set; }

        [Display(Name = "Job Results Released by You")]
        public int ResultsReleased { get; set; }
    }
}