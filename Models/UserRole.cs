using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentRegistarationPortal.Models
{
    public class UserRole
    {
        [Key]
        public int UserId { get; set; }

        public string RoleName { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}