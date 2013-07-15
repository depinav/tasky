using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using System.Data.Entity;

namespace tasky.Models
{
    public class TeamMember
    {
        public int id { get; set; }

        [Display(Name = "Team Member Name")]
        public string name { get; set; }

        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Tasks")]
        public IEnumerable<Task> tasks { get; set; }

        // the following is a reminder for app security purposes
        public string hash { get; set; }
        public string salt { get; set; }
    }
}