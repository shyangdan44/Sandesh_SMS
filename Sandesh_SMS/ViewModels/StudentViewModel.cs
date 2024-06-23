using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Sandesh_SMS.Models;
using Microsoft.AspNetCore.Http;

namespace Sandesh_SMS.ViewModels
{
    public class StudentViewModel
    {
        public StudentViewModel()
        {
            
        }
        public int StudentId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Display(Name = "Choose Image")]
        public string StdProfile { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public bool IsActive { get; set; }

        //Relationship with Class
        public int ClassId { get; set; } //Foreign Key

        [ForeignKey("Class")]
        public Class? Class { get; set; } //Reference navigation property

        //Relationship with Course

        public int CourseId { get; set; } //Foreign Key

        [ForeignKey("Course")]
        public Course? Course { get; set; } //Reference navigation property
    }
}
