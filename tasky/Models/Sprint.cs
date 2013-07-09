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
        
        [DataType(DataType.Text)]
        public string title { get; set; }

        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }
    }
}