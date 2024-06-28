using StudentRegistarationPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentRegistarationPortal.Services
{
    public class AutoMapperWebProfile : AutoMapper.Profile
    {
        public AutoMapperWebProfile()
        {
            CreateMap<CourseDTO, Course>();
            CreateMap<UserDTO, User>();
            CreateMap<SelectedCourseDTO, SelectedCourse>();
        }

        public static void Run()
        {
            AutoMapper.Mapper.Initialize(profile =>
            {
                profile.AddProfile<AutoMapperWebProfile>();

            });
        }

    }
}