using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using tasky.Models;

namespace tasky.ViewModels
{
    public class TaskLogViewModel
    {
        public int taskId { get; set; }
        public string taskTitle { get; set; }
        public string taskDesc { get; set; }
        public int taskEstHours { get; set; }
        public int taskRemHours { get; set; }
        public string taskStatus { get; set; }
        public string taskTmName { get; set; }
        public int taskTmID { get; set; }
        public string taskStoryTitle { get; set; }
        public int taskStoryId { get; set; }

        public int loggedHours { get; set; }

        [Display(Name = "Log Date")]
        [DataType(DataType.Date)]
        public DateTime logDate { get; set; }

        public int taskLogID { get; set; }

        public void getTaskInfo(Task task)
        {
            this.taskId = task.id;
            this.taskTitle = task.Title;
            this.taskDesc = task.Description;
            this.taskEstHours = task.Estimate_Hours;
            this.taskRemHours = task.Remaining_Hours;
            this.taskStatus = task.Status;
            this.taskTmName = task.TeamMember.name;
            this.taskTmID = task.TeamMember.id;
            this.taskStoryTitle = task.story.title;
            this.taskStoryId = task.story.id;
        }

        public void getTaskLogInfo(TaskLog taskLog)
        {
            this.loggedHours = taskLog.loggedHours;
            this.logDate = taskLog.logDate;
            this.taskLogID = taskLog.id;
        }
    }
}