using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace tasky.Models
{
    public class TaskLog
    {
        public int id { get; set; }

        [Display(Name="Hours Logged")]
        [Required]
        public int loggedHours { get; set; }

        [Display(Name="Log Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime logDate { get; set; }

        [Display(Name = "Task")]
        [ForeignKey("task")]
        public int taskId { get; set; }
        public virtual Task task { get; set; }
    }
}