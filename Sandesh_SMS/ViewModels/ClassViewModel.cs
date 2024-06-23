using System.ComponentModel.DataAnnotations;

namespace Sandesh_SMS.ViewModels
{
    public class ClassViewModel
    {
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Class name is required.")]
        public string ClassName { get; set; }
    }
}
