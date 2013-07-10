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

        public int id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Hours must be greater than 0")]
        public int Estimate_Hours { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Hours must be greater than 0")]
        public int Remaining_Hours { get; set; }

        public string Status { get; set; }

        [Display(Name = "Team Member")]
        [ForeignKey("TeamMember")]
        public int TeamMemberId { get; set; }
        public virtual TeamMember TeamMember { get; set; }
    }
}