using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace LMS.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
      
        public DbSet<Course> Courses{ get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Activity> Activities{ get; set; }



        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}