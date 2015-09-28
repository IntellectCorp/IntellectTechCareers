using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntellectTechCareers.Models
{
    public class JobModel
    {
        [Display(Name = "Job ID")]
        public int JobId { get; set; }

        [Required]
        [Display(Name = "Job Description")]
        public string JobDesc { get; set; }

        [Display(Name = "Job Role")]
        public int JobRole { get; set; }
        public IEnumerable<JobRole> JobRoles { get; set; }

        [Display(Name = "Skills")]
        public List<string> Skills { get; set; }

        [Display(Name = "Vacancies")]
        public int Vacancies { get; set; }

        [Display(Name = "Min Experience")]
        public int MinExperience { get; set; }

        [Display(Name = "Max Experience")]
        public int MaxExperience { get; set; }

        [Display(Name = "Age Limit")]
        public int AgeLimit { get; set; }

        [Display(Name = "Posted by")]
        public int PostedBy { get; set; }
        
        [Display(Name = "Posted On")]
        public DateTime PostedOn { get; set; }

        [Display(Name = "Status")]
        public String Status { get; set; }
    
    }

    public class JobViewModel
    {
        public List<JobModel> jobs  { get; set; }
        public Dictionary<string, string> qualifications { get; set; }
        public List<string> candidateUgQualification { get; set; }
        public List<string> candidatePgQualification { get; set; }
        public int totalExperience { get; set; }
        public string selectedJobs { get; set; }
        public int jobsAlreadyApplied { get; set; }
        public List<int> appliedJobs { get; set; }
    }

    public class JobWithApplicantsModel : JobModel
    {
        public JobWithApplicantsModel()
        {
        }

        public JobWithApplicantsModel(JobModel job)
        {
            this.JobId = job.JobId;
            this.JobDesc = job.JobDesc;
            this.JobRole = job.JobRole;
            this.MinExperience = job.MinExperience;
            this.MaxExperience = job.MaxExperience;
            this.PostedBy = job.PostedBy;
            this.PostedOn = job.PostedOn;
            this.Skills = job.Skills;
            this.Status = job.Status;
            this.Vacancies = job.Vacancies;
            this.AgeLimit = job.AgeLimit;
        }

        [Display(Name = "No. of Application")]
        public int ApplicantCount { get; set; }
    }

    public class JobSelectModel : JobModel
    {
        public JobSelectModel()
        {
        }

        public JobSelectModel(JobModel job)
        {
            this.JobId = job.JobId;
            this.JobDesc = job.JobDesc;
            this.JobRole = job.JobRole;
            this.MinExperience = job.MinExperience;
            this.MaxExperience = job.MaxExperience;
            this.PostedBy = job.PostedBy;
            this.PostedOn = job.PostedOn;
            this.Skills = job.Skills;
            this.Status = job.Status;
            this.Vacancies = job.Vacancies;
            this.AgeLimit = job.AgeLimit;
        }

        [Display(Name = "Checked")]
        public bool Checked { get; set; }
    }
}