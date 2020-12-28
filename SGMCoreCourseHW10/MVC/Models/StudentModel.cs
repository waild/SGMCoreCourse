using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class StudentModel
    {
        public int Id { get; set; }

        [Required] public int GroupId { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(4)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(4)]
        public string LastName { get; set; }

        public int BirthYear { get; set; }
        public int AverageMark { get; set; }
    }
}