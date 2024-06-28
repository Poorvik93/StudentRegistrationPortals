using StudentRegistarationPortal.Constant;
using StudentRegistarationPortal.Context;
using StudentRegistarationPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistarationPortal.Controllers
{
    [Authorize(Roles = UserRoles.Student)]
    public class StudentController : Controller
    {
        private StudentRPDbContext _dbContext;

        public StudentController()
        {
            _dbContext = new StudentRPDbContext();
        }

        public ActionResult AllCourses()
        {
            try
            {
                ViewBag.CourseList = _dbContext.Courses.Where(course => course.Date >= DateTime.Now).ToList();

                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }

        }

        public ActionResult SelectedCourses()
        {
            try
            {
                var validUser = _dbContext.Users.Any(user => user.UserName == User.Identity.Name);

                if (validUser)
                {
                    var validUserId = _dbContext.Users.Single(user => user.UserName == User.Identity.Name).Id;
                    ViewBag.SelectedCourses = _dbContext.SelectedCourses.Where(course => course.UserId == validUserId).ToList();
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }

        }

        /// <summary>
        /// If the course is valid and selected course date doesn't clash with existing courses of student then it will be saved.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="selectedCourseDetails"></param>
        /// <returns>View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCourse(int courseId, SelectedCourseDTO selectedCourseDetails)
        {
            try
            {
                var validCourse = _dbContext.Courses.Find(courseId);
                var validUser = _dbContext.Users.Any(user => user.UserName == User.Identity.Name);

                if (validCourse != null && validUser)
                {
                    var validUserId = _dbContext.Users.Single(user => user.UserName == User.Identity.Name).Id;

                    var userHasCourse = _dbContext.SelectedCourses.Where(course => course.CourseId == courseId && course.UserId == validUserId).Count() > 0;

                    if (!userHasCourse)
                    {
                        var minusOneHour = selectedCourseDetails.Time - selectedCourseDetails.Time.AddHours(1);
                        var plusOneHour = selectedCourseDetails.Time.AddHours(1) - selectedCourseDetails.Time;

                        /*var userSelectedCourseTimeClash = _dbContext.SelectedCourses.Any(course => 
                        course.UserId == validUserId && 
                        (selectedCourseDetails.Time == course.Time && 
                        ((selectedCourseDetails.Time - course.Time).Equals(plusOneHour) || 
                        (course.Time - selectedCourseDetails.Time).Equals(minusOneHour))) && 
                        selectedCourseDetails.FromDate == course.FromDate);*/

                        var userSelectedCourseTimeClash = _dbContext.SelectedCourses.Any(course =>
                        course.UserId == validUserId &&
                        selectedCourseDetails.FromDate == course.FromDate);

                        if (!userSelectedCourseTimeClash)
                        {
                            if (selectedCourseDetails.FromDate < validCourse.Date.AddDays(validCourse.NoOfDays))
                            {
                                var newSelectedCourse = new SelectedCourse
                                {
                                    UserId = validUserId,
                                    CourseId = courseId,

                                    ToDate = selectedCourseDetails.FromDate.AddDays(validCourse.NoOfDays)
                                };
                                AutoMapper.Mapper.Map(selectedCourseDetails, newSelectedCourse);

                                _dbContext.SelectedCourses.Add(newSelectedCourse);
                                _dbContext.SaveChanges();

                                return RedirectToAction("SelectedCourses");
                            }
                            ModelState.AddModelError(string.Empty, "Selected date is not qualified to complete the course.");
                            ViewBag.CourseList = _dbContext.Courses.Where(course => course.Date >= DateTime.Now).ToList();
                            return View("AllCourses");
                        }
                        ModelState.AddModelError(string.Empty, "Selected date clashes with existing courses");
                        ViewBag.CourseList = _dbContext.Courses.Where(course => course.Date >= DateTime.Now).ToList();
                        return View("AllCourses");

                    }
                    ModelState.AddModelError(string.Empty, "Course already exists");

                }
                ViewBag.CourseList = _dbContext.Courses.Where(course => course.Date >= DateTime.Now).ToList();
                return View("AllCourses");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.CourseList = _dbContext.Courses.ToList();
                return View("Courses");
            }

        }

        public ActionResult UnregisterCourse(int courseId)
        {
            try
            {
                var validUser = _dbContext.Users.Any(user => user.UserName == User.Identity.Name);

                if (validUser)
                {
                    var validUserId = _dbContext.Users.Single(user => user.UserName == User.Identity.Name).Id;

                    var validSelectedCourse = _dbContext.SelectedCourses.Single(selectedCourse => selectedCourse.CourseId == courseId && selectedCourse.UserId == validUserId);

                    if (validSelectedCourse != null)
                    {
                        _dbContext.SelectedCourses.Remove(validSelectedCourse);
                        _dbContext.SaveChanges();
                    }
                    ModelState.AddModelError(string.Empty, "Not a valid course.");
                }
                ModelState.AddModelError(string.Empty, "Not a valid User.");

                return RedirectToAction("SelectedCourses");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("SelectedCourses");
            }


        }
    }
}