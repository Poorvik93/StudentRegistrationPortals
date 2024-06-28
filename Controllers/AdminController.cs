using AutoMapper;
using Newtonsoft.Json;
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
    [Authorize(Roles = UserRoles.Admin)]
    public class AdminController : Controller
    {
        private StudentRPDbContext _dbContext;
        public AdminController()
        {
            _dbContext = new StudentRPDbContext();
        }


        public ActionResult Courses()
        {
            try
            {

                ViewBag.CourseList = _dbContext.Courses.ToList();
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        public JsonResult CoursesList()
        {
            var courseList = JsonConvert.SerializeObject(_dbContext.Courses.ToList());
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateCourse()
        {
            return View();
        }

        /// <summary>
        /// Saves the course details if the course has date clashing with each other then error will be displayed
        /// </summary>
        /// <param name="courseDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCourse(CourseDTO courseDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool courseDateClash = false;

                    if (courseDetails.Date.Count > 1)
                    {

                        if (courseDetails.Date.Count != courseDetails.Date.Distinct().Count())
                        {
                            courseDateClash = true;
                        }
                    }
                    if (!courseDateClash)
                    {
                        var courseDateAndTime = courseDetails.Date.Zip(courseDetails.Time, (date, time) => new { Date = date, Time = time });
                        foreach (var courseDateTime in courseDateAndTime)
                        {
                            Course course = new Course();
                            Mapper.Map(courseDetails, course);
                            course.Date = courseDateTime.Date;
                            course.Time = courseDateTime.Time;

                            _dbContext.Courses.Add(course);
                            _dbContext.SaveChanges();
                        }

                        return RedirectToAction("Courses");
                    }
                    ModelState.AddModelError(string.Empty, "Course dates cannot be same ");
                }
                return View("CreateCourse");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("CreateCourse");
            }

        }
        /// <summary>
        /// Accept the id and edit the course details//
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View</returns>
        public ActionResult EditCourse(int id)
        {
            try
            {
                var courseDetails = _dbContext.Courses.Find(id);
                ViewBag.CourseDetails = null;

                if (courseDetails != null)
                {
                    ViewBag.CourseDetails = courseDetails;
                    return View();
                }

                ModelState.AddModelError(string.Empty, "Course Not Found!");
                return View("Courses");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Courses");
            }


        }
        /// <summary>
        /// Updates the course details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseDetails"></param>
        /// <returns>View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCourse(int id, CourseDTO courseDetails)
        {
            var validCourse = _dbContext.Courses.Find(id);
            Mapper.Map(courseDetails, validCourse);
            ViewBag.CourseDetails = validCourse;
            try
            {
                if (validCourse != null)
                {
                    if (ModelState.IsValid)
                    {
                        Mapper.Map(courseDetails, validCourse);
                        validCourse.Date = courseDetails.Date.First();
                        validCourse.Time = courseDetails.Time.First();

                        _dbContext.SaveChanges();
                        return RedirectToAction("Courses");
                    }
                }
                return View("EditCourse");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("EditCourse");
            }
        }

        public ActionResult DeleteCourse(int id)
        {
            try
            {
                var validCourse = _dbContext.Courses.Find(id);

                if (validCourse != null)
                {
                    _dbContext.Courses.Remove(validCourse);
                    _dbContext.SaveChanges();
                }

                return RedirectToAction("Courses");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Courses");
            }
        }
    }
}