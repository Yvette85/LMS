using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [Display(Name= "Course Name ")]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description{ get; set; }

        public DateTime StartDate{ get; set; }
        public DateTime EndDate { get; set; }


        public ICollection<ApplicationUser> students { get; set; }
        public ICollection<Module> Modules { get; set; }


        
    }
}