using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentRegistarationPortal.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        public int NoOfDays { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

    }
}