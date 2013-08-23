using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace tasky.Models
{
    public class Sprint
    {
        
        public int id { get; set; }
        
        [Display(Name="Sprint Title")]
        [DataType(DataType.Text)]
        public string title { get; set; }

        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }

        [Display(Name="Stories")]
        public ICollection<Story> stories { get; set; }

        
        [Display(Name="Release")]
        [ForeignKey("release")]
        public int releaseId { get; set; }
        public virtual Release release { get; set; }
        
         }

    
    public class SprintAPI
    {
        public int id { get; set; }

        [Required]
        public string title { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public int releaseId { get; set; }
    }
}