using System.ComponentModel.DataAnnotations;

namespace Sandesh_SMS.ViewModels
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Course name is required.")]
        public string CourseName { get; set; }
    }
}
