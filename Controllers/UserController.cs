using StudentRegistarationPortal.Constant;
using StudentRegistarationPortal.Context;
using StudentRegistarationPortal.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
namespace StudentRegistarationPortal.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private StudentRPDbContext _dbContext;
        public UserController()
        {
            _dbContext = new StudentRPDbContext();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUpUser(UserDTO user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool userAlreadyExists = _dbContext.Users.Any(existingUser => existingUser.UserName.Equals(user.UserName));

                    if (userAlreadyExists)
                    {
                        ModelState.AddModelError(string.Empty, "User Already Exists!");
                        return View("SignUp");
                    }

                    var newUser = new User();
                    AutoMapper.Mapper.Map(user, newUser);
                    _dbContext.Users.Add(newUser);

                    //Seeded admin user by using migration and any new signup from now on results in user role of student.
                    var newUserRole = new UserRole()
                    {
                        UserId = newUser.Id,
                        RoleName = UserRoles.Student,
                    };

                    _dbContext.Roles.Add(newUserRole);
                    _dbContext.SaveChanges();

                    return RedirectToAction("Login");
                }

                return View("SignUp");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("SignUp");
            }

        }

        public ActionResult LogIn()
        {
            return View();
        }


        //[HttpPost]
        //public ActionResult LogInUser(UserLoginDetails loginDetails)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var validUser = _dbContext.Users.Any(user => user.UserName == loginDetails.UserName && user.Password == loginDetails.Password);

        //            if (validUser)
        //            {
        //                FormsAuthentication.SetAuthCookie(loginDetails.UserName, true);



        //                if (User.IsInRole(UserRoles.Admin))
        //                    return RedirectToAction("Courses", "Admin");
        //                if (User.IsInRole(UserRoles.Student))
        //                    return RedirectToAction("Courses", "Student");
        //            }
        //            ModelState.AddModelError(string.Empty, "Invalid Username or Password");
        //        }
        //        return View("LogIn");
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError(string.Empty, ex.Message);
        //        return View("LogIn");
        //    }
        //}

        [HttpPost]
public ActionResult LogInUser(UserLoginDetails loginDetails, string returnUrl)
{
    try
    {
        if (ModelState.IsValid)
        {
            var validUser = _dbContext.Users.FirstOrDefault(user => user.UserName == loginDetails.UserName && user.Password == loginDetails.Password);

            if (validUser != null)
            {
                FormsAuthentication.SetAuthCookie(loginDetails.UserName, true);

                // Check if the logged-in user is admin
                var userRoles = _dbContext.Roles.Where(r => r.UserId == validUser.Id).Select(r => r.RoleName).ToList();

                if (userRoles.Contains(UserRoles.Admin))
                    return RedirectFromLoginPage(returnUrl); // Redirect to Admin section
                else if (userRoles.Contains(UserRoles.Student))
                    return RedirectFromLoginPage(returnUrl); // Redirect to Student section
            }

            ModelState.AddModelError(string.Empty, "Invalid Username or Password");
        }

        return View("LogIn");
    }
    catch (Exception ex)
    {
        ModelState.AddModelError(string.Empty, ex.Message);
        return View("LogIn");
    }
}

// Helper method to handle redirect based on returnUrl
private ActionResult RedirectFromLoginPage(string returnUrl)
{
    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
    {
        return Redirect(returnUrl);
    }
    else
    {
        // If no valid returnUrl provided, redirect to default location based on roles
        if (User.IsInRole(UserRoles.Admin))
        {
            return RedirectToAction("Courses", "Admin");
        }
        else if (User.IsInRole(UserRoles.Student))
        {
            return RedirectToAction("AllCourses", "Student");
        }
        else
        {
            // Handle other roles or default case here
            return RedirectToAction("Index", "Home");
        }
    }
}

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn");
        }

    }

}
