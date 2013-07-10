using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace tasky.Models
{
    public class Sprint
    {
        
        public int id { get; set; }
        
        [Display(Name="Title")]
        [DataType(DataType.Text)]
        public string title { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }
        
        [Display(Name= "End Date")]
        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }

        [Display(Name="Stories")]
        public virtual IEnumerable<Story> stories { get; set; }
    }

    
    public class SprintAPI
    {
        public int id { get; set; }

        [Required]
        public string title { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }
    }
}