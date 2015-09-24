using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IntellectTechCareers.Models
{
    public class Qualification
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required Field")]
        [StringLength(10, ErrorMessage = "Maximum Length Allowed is 25.")]
        public string Name { get; set; }
        
        public string Type { get; set; }
    }

    public class Skill : Qualification
    {
        public Skill()
        {
        }

        public Skill(Qualification q){
            this.Id = q.Id;
            this.Name = q.Name;
            this.Type = q.Type;
        }

        public bool Checked { get; set; }
    }
}