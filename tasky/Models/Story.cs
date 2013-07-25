using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace tasky.Models
{
    public class Story
    {
        public int id { get; set; }
        
        [Display(Name = "Story Title")]
        [Required]
        public string title { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Story Points")]
        public int points { get; set; }

        [Display(Name = "Status")]
        [Required]
        public string status { get; set; }

        public int sprintOrder { get; set; }

        [Display(Name = "Sprint")]
        [ForeignKey("sprint")]
        public int sprintId { get; set; }
        public virtual Sprint sprint { get; set; }

        [ScriptIgnore]
        [Display(Name = "Tasks")]
        public ICollection<Task> tasks { get; set; }
    }
}