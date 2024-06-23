using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Sandesh_SMS.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        [NotMapped]
        
        [Display(Name = "Choose Image")]
        public IFormFile StdProfile { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }

        //Relationship with Class
        [ForeignKey("Class")]
        public int ClassId { get; set; } //Foreign Key
        //[ForeignKey("Class")]
        //public string ClassName { get; set; } //Foreign Key
        public Class? Class { get; set; } //Reference navigation property

        //Relationship with Course
        [ForeignKey("Course")]
        public int CourseId { get; set; } //Foreign Key
        //[ForeignKey("Course")]
        //public string CourseName { get; set; } //Foreign Key
        public Course? Course { get; set; } //Reference navigation property

    }
}
