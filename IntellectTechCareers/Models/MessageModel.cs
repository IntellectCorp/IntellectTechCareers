using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntellectTechCareers.Models
{
    public class Message
    {
        Message(int Id, string Type, string Text)
        {

        }
        public int Id { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
    
    }
}