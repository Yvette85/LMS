using LMS.Models;
using LMS.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LMS.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public UserManager<IdentityUser> userManager => HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();

        

        public ActionResult Index()
        {

            var viewModel = new RegisterViewModel();

            viewModel.Roles = context.Roles.ToList();



        List<UserViewModel> rv = new List<UserViewModel>();



            //foreach (var u in context.Users.ToList())
            //{
            //    rv.Add(new UserViewModel(u));
            //}



            //var User = userManager.FindByName(model.Email);
            //userManager.AddToRole(User.Id, Role.Name);

            return View(viewModel);


        }


        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var user = context.Users.Find(id);



        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    DetailsViewModel dv = new DetailsViewModel(user);

        //    return View(dv);
        //}







        // GET: User

        [Authorize(Roles = "Teacher")]
        public ActionResult Register()
        {

            ApplicationDbContext context = new ApplicationDbContext();

            var viewModel = new RegisterViewModel();


            viewModel.Roles = context.Roles.ToList();
            //viewModel.Courses = context.Courses.ToList();

            return View(viewModel);
        }




        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            var userStore = new UserStore<ApplicationUser>(context);

            //var identityResult = UserManager.Create(new IdentityUser(model.Email), model.Password);

            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

            //if (identityResult.Succeeded)
            //{
            //    return RedirectToAction("Index" ,"Home");
            //}


            if (ModelState.IsValid)
            {

                var courses = context.Courses.ToList();

                var user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CourseId = model.CourseId

                };



                var identityResult = userManager.Create(user, model.Password);

                //(new IdentityUser (model.Email), model.Password);






                if (identityResult.Succeeded)
                {
                  
                    var Role = context.Roles.FirstOrDefault(x => x.Id == model.RoleId);

                    var User = userManager.FindByName(model.Email);
                    //userManager.AddToRole(User.Id, context.Roles.FirstOrDefault(x => x.Id == model.RoleId).Name);
                    userManager.AddToRole(User.Id, Role.Name);

                    //context.Roles.FirstOrDefault(x => x.Id == model.RoleId);




                    //context.Users.Add(user);
                    //context.SaveChanges();


                    return RedirectToAction("Index", "Home");
                }







                ModelState.AddModelError("", identityResult.Errors.FirstOrDefault());


            }

            model.Courses = context.Courses.ToList();

            model.Roles = context.Roles.ToList();

            return View(model);

        }





        public ActionResult Edit(string id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = context.Users.Find(id);

            EditViewModel ev = new EditViewModel(user);


            if (user == null)
            {
                return HttpNotFound();
            }



            return View(ev);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email")]EditViewModel editv)
        {
            if (ModelState.IsValid)
            {

                var uv = context.Users.FirstOrDefault(x => x.Id == editv.Id);
                uv.FirstName = editv.FirstName;
                uv.LastName = editv.LastName;
                uv.Email = editv.Email;



                //context.Entry(editv).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editv);
        }
    }
}


