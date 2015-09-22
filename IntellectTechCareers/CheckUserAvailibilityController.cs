using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IntellectTechCareers
{
    public class CheckUserAvailibilityController : ApiController
    {
        // GET api/<controller>/5
        public string isUserAvailable(string id)
        {
            return "true";
        }
    }
}