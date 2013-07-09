﻿using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace tasky.Models
{
    public class Story
    {
        public int ID { get; set; }

        [Required]
        public string title { get; set; }

        public string description { get; set; }

        public int points { get; set; }

        [Required]
        public string status { get; set; }
    }
}