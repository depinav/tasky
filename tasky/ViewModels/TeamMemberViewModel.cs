using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace tasky.ViewModels
{
    public class TeamMemberViewModel
    {
        public int id { get; set; }

        [Display(Name = "Team Member Name")]
        public string name { get; set; }

        [Display(Name = "Email")]
        public string email { get; set; }

        public IEnumerable<TaskViewModel> tasks { get; set; }

        // the following is a reminder for app security purposes
        public string hash { get; set; }
        public string salt { get; set; }
    }
}