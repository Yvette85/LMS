using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.ViewModels
{
    public class DetailsViewModel
    {
        public ApplicationUser u;

        public DetailsViewModel()
        {
        }

        public DetailsViewModel(ApplicationUser u)
        {
            Id = u.Id;
            FirstName = u.FirstName;
            LastName = u.LastName;
            Email = u.Email;
        }


        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }

    }
}
