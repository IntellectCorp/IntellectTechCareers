using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace IntellectTechCareers.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [StringLength(15, ErrorMessage = "The {0} should be between 6 and 15 characters long.", MinimumLength = 6)]
        [RegularExpression("^[a-zA-Z_]*$", ErrorMessage="User name can only have Alphabets and Underscore")]
        [Display(Name = "User name*")]
        public string UserName { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression("(?=.*\\d)(?=.*[a-zA-Z]).{1,}", ErrorMessage = "Password must have at least 1 digit and 1 alphabet")]
        [DataType(DataType.Password)]
        [Display(Name = "Password*")]
        //TODO : Must have one digit and one alphabet
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Name cannot be greater than 30 characters. ")]
        [Display(Name = "Name*")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email ID*")]
        public string EmailID { get; set; }

        [Required]
        [StringLength(10,  ErrorMessage = "Contact Number should be of Length 10 ", MinimumLength=10)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Contact Number should be only numbers")]
        [Display(Name = "Contact Number*")]
        public string ContactNo { get; set; }

        [Required]
        [Display(Name = "Gender*")]
        public string gender { get; set; }

        [Required]
        [Display(Name = "Date of Birth*")]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime dob { get; set; }

        [Required]
        [Display(Name = "Address*")]
        [StringLength(30, ErrorMessage = "Address cannot be greater than 30 characters. ")]
        public string Address { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }

    public class User
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string role { get; set; }
        public string state { get; set; }
        public DateTime AccountActiveDate { get; set; }
    }

    public class ChangePasswordModel
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression("(?=.*\\d)(?=.*[a-zA-Z]).{1,}", ErrorMessage = "Password must have at least 1 digit and 1 alphabet")]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string currentPassword { get; set; }     

        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression("(?=.*\\d)(?=.*[a-zA-Z]).{1,}", ErrorMessage = "Password must have at least 1 digit and 1 alphabet")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string newPassword { get; set; }

        [Required]
        [Display(Name = "Confirm New Password")]
        [System.ComponentModel.DataAnnotations.Compare("newPassword", ErrorMessage = "New password and confirmation new password do not match.")]
        public string confirmNewPassword { get; set; }

        public bool isApplicant { get; set; }
        public int selectedStaff { get; set; }
        public int selectedApplicant { get; set; }

    }

    public class ExperienceModel
    {
        public int user_id { get; set; }

        [StringLength(25, MinimumLength=1,ErrorMessage="Company name should be between 1 and 25 characters")]
        [Required]
        public string company { get; set; }

        [StringLength(30, MinimumLength = 1, ErrorMessage = "Designation should be between 1 and 30 characters")]
        [Required]
        public string designation { get; set; }

        [Required]
        [Range(1, 500)]
        public int experience { get; set; }
    }

    public class QualificationModel
    {
        public int qualification_id { get; set; }
        public string qualification { get; set; }
        public string type { get; set; }
    }

    public class CandidateModel
    {
        public int user_id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string gender { get; set; }
        public DateTime dob { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [Display(Name = "Email ID")]
        public string EmailID { get; set; }

        [StringLength(10, MinimumLength = 10)]
        [RegularExpression("^[0-9]+$")]
        [Display(Name = "Contact Number")]
        public string ContactNo { get; set; }

        [Display(Name = "Address")]
        [StringLength(30)]
        public string Address { get; set; }

        public List<String> ugQualifications { get; set; }
        public List<String> pgQualifications { get; set; }

        public List<ExperienceModel> experienceDetails { get; set; }

        public string newUgQualification {get; set;}
        public string newPgQualification { get; set; }
    }

    public class CandidateViewModel
    {
        public CandidateModel candidate;

        //Not required : TODO : REmove this view model and use only CandidateModel
        public List<SelectListItem> ugList;
        public List<SelectListItem> pgList;
    }

    public class QualificationViewModel
    {
        public List<QualificationModel> qualifications { get; set; }
    }

    public class ExperienceViewModel
    {
        public List<ExperienceModel> experience { get; set; }
    }

    public class ShowApplicantModel
    {
        public int CandidateId { get; set; }
        public string Name { get; set; }
        public int JobId { get; set; }
        public string JobDesc { get; set; }
        public DateTime Date { get; set; }
    }
}

//Microsoft enterprice library version 5
//Microsoft.Practices.Exception*.Data.dll
//Logging,
//aspnet_regiis encrypt config_file
