using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class CourseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(4)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(4)]
        public string LectorName { get; set; }
    }
}