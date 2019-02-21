using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class Activity
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public string Type{ get; set; }
        public DateTime StartTime{ get; set; }
        public DateTime EndTime{ get; set; }
        public string Description{ get; set; }

        public virtual Module Module { get; set; }
        public int ModuleId { get; internal set; }
    }
}