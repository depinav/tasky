using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using tasky.Models;
using tasky.ViewModels;

namespace tasky.ViewModels
{
    public class ReleaseViewModel
    {
        public int id { get; set; }

        [Display(Name = "Release Name")]
        [DataType(DataType.Text)]
        public string title { get; set; }

        public ICollection<SprintViewModel> sprints { get; set; }


        public static ReleaseViewModel convertRelease(Release release)
        {
            ReleaseViewModel releaseVM = new ReleaseViewModel();

            if (release != null)
            {
                releaseVM.id = release.id;
                releaseVM.title = release.title;
            }
            return releaseVM;
        }

        public void convertSprintsToVMs(ICollection<Sprint> sprints)
        {
            if (sprints != null)
            {
                if (this.sprints == null)
                    this.sprints = new List<SprintViewModel>();
                foreach (Sprint sprint in sprints)
                {
                    SprintViewModel tempSVM = SprintViewModel.convertSprint(sprint);
                    this.sprints.Add(tempSVM);
                }
            }
        }
    }
}