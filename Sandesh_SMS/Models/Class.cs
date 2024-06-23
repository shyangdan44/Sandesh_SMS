namespace Sandesh_SMS.Models
{
    public class Class
    {
        public int ClassId { get; set; } //Primary Key
        public string ClassName { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
