using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using tasky.Models;

namespace tasky.ViewModels
{
    public class SprintViewModel
    {
        public int id { get; set; }

        [Display(Name = "Sprint Title")]
        [DataType(DataType.Text)]
        public string title { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }

        public void convertSprint(Sprint sprint)
        {
            if (sprint == null)
            {
                this.id = 0;
                this.title = "";
                this.startDate = DateTime.Now;
                this.endDate = DateTime.Now;
            }
            else
            {
                this.id = sprint.id;
                this.title = sprint.title;
                this.startDate = sprint.startDate;
                this.endDate = sprint.endDate;
            }
        }
    }
}