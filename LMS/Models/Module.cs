﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description  { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Course Course { get; set; }
        public int CourseId { get; set; }
        public ICollection<Activity>activities { get; set; }
    }
}