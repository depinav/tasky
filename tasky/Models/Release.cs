using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace tasky.Models
{
    public class Release
    {
        public int id { get; set; }

        [Display(Name="Release Name")]
        [DataType(DataType.Text)]
        public string title { get; set; }
        /*
        //What else is needed here?  startDate, endDate,
        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }
        */
        [Display(Name="Sprints")]
        public ICollection<Sprint> sprints { get; set; }

        [Display(Name = "Members")]
        public ICollection<TeamMember> members { get; set; }

    }
}