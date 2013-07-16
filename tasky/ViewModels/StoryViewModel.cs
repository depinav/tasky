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

        public static StoryViewModel convertStory(Story story)
        {
            StoryViewModel storyVM = new StoryViewModel();
            storyVM.id = story.id;
            storyVM.title = story.title;
            storyVM.description = story.description;
            storyVM.points = story.points;
            storyVM.status = story.status;
            storyVM.sprintId = story.sprintId;

            return storyVM;
        }
    }
}