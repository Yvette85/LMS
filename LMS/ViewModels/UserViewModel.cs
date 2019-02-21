using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.ViewModels
{
    public class UserViewModel
    {

        public ApplicationUser u;

        public UserViewModel()
        {
        }

        public UserViewModel(ApplicationUser u)
        {
            Id = u.Id;
            FirstName = u.FirstName;
            LastName = u.LastName;
            Email = u.Email;
            //CourseName = u.CourseId;


        }


        public string Id  { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public String Email { get; set; }
    }
}