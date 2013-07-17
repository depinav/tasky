using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using tasky.Models;
using tasky.ViewModels;

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

        public ICollection<StoryViewModel> stories { get; set; }

        public static SprintViewModel convertSprint(Sprint sprint)
        {
            SprintViewModel sprintVM = new SprintViewModel();

            if (sprint != null)
            {
                sprintVM.id = sprint.id;
                sprintVM.title = sprint.title;
                sprintVM.startDate = sprint.startDate;
                sprintVM.endDate = sprint.endDate;
            }
            return sprintVM;
        }

        public void convertStoriesToVMs(ICollection<Story> stories)
        {
            if (stories != null)
            {
                if (this.stories == null)
                    this.stories = new List<StoryViewModel>();

                foreach (Story story in stories)
                {
                    this.stories.Add(StoryViewModel.convertStory(story));
                }
            }
        }
    }
}