using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using tasky.Models;
using tasky.ViewModels;

namespace tasky.ViewModels
{
    public class StoryViewModel
    {
        public int id { get; set; }

        [Display(Name = "Story Title")]
        [Required]
        public string title { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Points")]
        public int points { get; set; }

        [Display(Name = "Status")]
        [Required]
        public string status { get; set; }

        [Display(Name = "Sprint")]
        [ForeignKey("sprint")]
        public int sprintId { get; set; }

        public ICollection<TaskViewModel> tasks { get; set; }

        public SprintViewModel sprintViewModel {get;set;}

        public int convertStory(Story story)
        {
            if (story == null)
                return -1;
            else
            {
                this.id = story.id;
                this.title = story.title;
                this.description = story.description;
                this.points = story.points;
                this.status = story.status;
                this.sprintId = story.sprintId;
                return 0;
            }
        }
    }
}