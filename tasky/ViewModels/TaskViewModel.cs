using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tasky.Models;

namespace tasky.ViewModels
{
    public class TaskViewModel
    {
        public int id { get; set; }

        [Display(Name = "Task Title")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Estimated")]
        [Range(0, int.MaxValue, ErrorMessage = "Hours must be greater than 0")]
        public int Estimate_Hours { get; set; }

        [Display(Name = "Remaining")]
        [Range(0, int.MaxValue, ErrorMessage = "Hours must be greater than 0")]
        public int Remaining_Hours { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        public static TaskViewModel convertTask(Task task)
        {
            TaskViewModel taskVM = new TaskViewModel();
            if (task != null)
            {
                taskVM.id = task.id;
                taskVM.Title = task.Title;
                taskVM.Description = task.Description;
                taskVM.Estimate_Hours = task.Estimate_Hours;
                taskVM.Remaining_Hours = task.Remaining_Hours;
                taskVM.Status = task.Status;
            }
            return taskVM;
        }
    }
}