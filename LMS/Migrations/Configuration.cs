namespace LMS.Migrations
{
    using LMS.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LMS.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.




            var courses = new[]
                     {
                new Course { Id=1, Name = ".Net", Description = "Fullstack course", StartDate = DateTime.Now.AddDays(10)},
                new Course { Id=2, Name ="Java", Description ="Java Course", StartDate = DateTime.Now.AddDays(10)},
                new Course { Id=3, Name ="C++", Description ="Fundamentals in C++", StartDate = DateTime.Now.AddDays(10)}
            };

            context.Courses.AddOrUpdate(c => c.Name, courses);
            context.SaveChanges();

            var modules = new[]
            {
                new Module {Name ="C#", Description = "Going POCO", StartDate = DateTime.Now.AddDays(10), CourseId= 1, EndDate = DateTime.Now.AddDays(110)},
                new Module {Name ="EntityFramwork", Description = "Working with Entity", StartDate = DateTime.Now.AddDays(10), CourseId=2, EndDate = DateTime.Now.AddDays(110)},
                new Module {Name ="Identity", Description = "Working with Identity", StartDate = DateTime.Now.AddDays(10), CourseId=3, EndDate = DateTime.Now.AddDays(110)},
                //new Module {Name ="Search", Description = "Learning how to ask google the right questions", StartDate = DateTime.Now.AddDays(10), CourseId=1, EndDate = DateTime.Now.AddDays(80)},

            };
            context.Modules.AddOrUpdate(m => m.Name, modules);
            context.SaveChanges();

            var activities = new[]
            {
                new Activity {Name = "Listen and learn", Type ="E-Learning", Description ="Watch video 'Entityframwork'", StartTime = DateTime.Now.AddDays(10), EndTime = DateTime.Now.AddDays(110), ModuleId = 1},
                new Activity {Name = "Writing the Matrix", Type ="CodeALong", Description ="Codeing a long with John", StartTime = DateTime.Now.AddDays(10), EndTime = DateTime.Now.AddDays(110), ModuleId = 1},
                new Activity {Name = "Zoo animals and there unatural habitat", Type ="Lecture", Description ="Adrian will talk about the animals at the zoo", StartTime = DateTime.Now.AddDays(10), EndTime = DateTime.Now.AddDays(110), ModuleId = 1},
                new Activity {Name = "Garage", Type ="Exercise", Description ="You will be given an exerices to code a garage", StartTime = DateTime.Now.AddDays(10), EndTime = DateTime.Now.AddDays(110), ModuleId = 1},

            };
            context.Activities.AddOrUpdate(a => a.Name, activities);
            context.SaveChanges();

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new ApplicationUserManager(userStore);


            var studs = new[] {new ApplicationUser {FirstName = "Erik", LastName = "Eriksson", Email = "Erik@lexicon.se", CourseId = 1, UserName ="Erik@lexicon.se" },
                               new ApplicationUser {FirstName = "Fredrik", LastName = "Fredriksson", Email = "Fredrik@lexicon.se", CourseId =1, UserName = "Fredrik@lexicon.se" },
                               new ApplicationUser {FirstName = "David", LastName ="Davidsson", Email ="David@lexicon.se", CourseId = 1, UserName ="David@lexicon.se" },
                               new ApplicationUser {FirstName = "Ahmed", LastName ="Gazawi", Email ="Ahmed@lexicon.se", CourseId= 1, UserName ="Ahmed@lexicon.se"},
                               new ApplicationUser {FirstName ="Borg", LastName ="Birkasson", Email ="Borge@lexicon.se", CourseId = 2, UserName ="Borge@lexicon.se"},
                               new ApplicationUser {FirstName ="John", LastName = "Hellman", Email = "teacher@lexicon.se", UserName = "teacher@lexicon.se" },
            };


            foreach (var stud in studs)
            {
                if (context.Users.Any(u => u.UserName == stud.UserName)) continue;

                //var user = new ApplicationUser { UserName = stud., Email = stud, FirstName = stud, LastName = stud};
                var result = userManager.Create(stud, "lexicon");
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join("\n", result.Errors));
                }
            }

            context.Users.AddOrUpdate(u => u.UserName, studs);
            context.SaveChanges();
           
            var emails = new[] { "teacher@lexicon.se",  "student@lexicon.se" };

            foreach (var email in emails)
            {
                if (context.Users.Any(u => u.UserName == email)) continue;

                var user = new ApplicationUser { UserName = email, Email = email };
                var result = userManager.Create(user, "lexicon");
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join("\n", result.Errors));
                }
                context.Users.AddOrUpdate(u => u.UserName, studs);
                context.SaveChanges();
            }


            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var roleNames = new[] { "Teacher", "Student" };

            foreach (var roleName in roleNames)
            {
                if (context.Roles.Any(r => r.Name == roleName)) continue;

                var role = new IdentityRole { Name = roleName };
                var result = roleManager.Create(role);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join("\n", result.Errors));
                }
            }

            var erikUser = userManager.FindByName("Erik@lexicon.se");
            userManager.AddToRole(erikUser.Id, "Student");


            var fredrikUser = userManager.FindByName("Fredrik@lexicon.se");
            userManager.AddToRole(fredrikUser.Id, "Student");


            var davidUser = userManager.FindByName("David@lexicon.se");
            userManager.AddToRole(davidUser.Id, "Student");


            var teacherUser = userManager.FindByName("teacher@lexicon.se");
            userManager.AddToRole(teacherUser.Id, "Teacher");






        }
    }
}
