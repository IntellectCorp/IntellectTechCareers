using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IntellectTechCareers.Models
{
    public class ManagerDashBoardModel
    {
        [Display(Name = "Jobs posted in total")]
        public int TotalJobs { get; set; }

        [Display(Name = "Jobs scheduled for interview in total")]
        public int TotalScheduledInterview { get; set; }

        [Display(Name = "Job results released in total")]
        public int TotalResults { get; set; }

        [Display(Name = "Jobs yet to be scheduled for interview")]
        public int UnscheduledInterviewJobs { get; set; }

        [Display(Name = "Job results yet to be released")]
        public int ResultsNotReleased { get; set; }
    }

    public class StaffDashBoardModel
    {
        [Display(Name = "Rights to Post Job ")]
        public string RightToPost { get; set; }

        [Display(Name = "Rights to Schedule Interview ")]
        public string RightToScheduleInterview { get; set; }

        [Display(Name = "Rights to Release Results ")]
        public string RightToReleaseResults { get; set; }

        [Display(Name = "Jobs Posted by You")]
        public int JobPosted { get; set; }

        [Display(Name = "Jobs Scheduled for Interview by You")]
        public int InterviewsScheduled { get; set; }

        [Display(Name = "Job Results Released by You")]
        public int ResultsReleased { get; set; }
    }
}