using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace tasky.Models
{
    public class Task
    {
        
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [Display(Name="Estimated")]
        [Range(0, int.MaxValue, ErrorMessage="Hours must be greater than 0")]
        public int Estimate_Hours { get; set; }

        [Display(Name="Remaining")]
        [Range(0, int.MaxValue, ErrorMessage="Hours must be greater than 0")]
        public int Remaining_Hours { get; set; }

        public string Status { get; set; }

        public string TeamMember { get; set; }
    }
}