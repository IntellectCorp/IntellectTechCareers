using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntellectTechCareers.Utils
{
    public class Time24hrFormat
    {
        public Time24hrFormat()
        {
            Hour = 0;
            Minute = 0;
        }

        public Time24hrFormat(string HourAndMinute)
        {
            string[] val = HourAndMinute.Split(':');
            Hour = Int32.Parse(val[0]);
            Minute = Int32.Parse(val[1]);
        }

        public int Hour { get; set; }
        public int Minute { get; set; }

        public override string ToString()
        {
            return Hour+":"+Minute;
        }
    }
}