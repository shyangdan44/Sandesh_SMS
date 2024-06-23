namespace Sandesh_SMS.Models
{
    public class Course
    {
        public int CourseId { get; set; } //Primary Key
        public string CourseName { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
