using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tasky.Models
{
    public class Task
    {

        public int ID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public int Estimate_Hours { get; set; }
        public int Remaining_Hours { get; set; }

        public string Status { get; set; }
    }
}