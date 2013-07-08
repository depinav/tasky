using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;

namespace tasky.Models
{
    public class TeamMember
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }

        // the following is a reminder for app security purposes
        public string hash { get; set; }
        public string salt { get; set; }
    }
}