﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace tasky.Models
{
    public class Task
    {

        public int id { get; set; }

        [Display(Name = "Task Title")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name="Estimated")]
        [Range(0, int.MaxValue, ErrorMessage = "Hours must be greater than 0")]
        public int Estimate_Hours { get; set; }

        [Display(Name="Remaining")]
        public int Remaining_Hours { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Team Member")]
        [ForeignKey("TeamMember")]
        public int TeamMemberId { get; set; }
        public virtual TeamMember TeamMember { get; set; }

        [Display(Name = "Story")]
        [ForeignKey("story")]
        [ScriptIgnore]
        public int storyId { get; set; }

        [ScriptIgnore]
        public virtual Story story { get; set; }
    }
}