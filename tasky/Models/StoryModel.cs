using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;

namespace tasky.Models
{
    public class Story
    {
        public int ID { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        //creator
        //assignee
        public int points { get; set; }
        //sprint
        public string status { get; set; }
    }

    public class StoryDBContext : DbContext
    {
        public DbSet<Story> Stories { get; set; }
    }
}