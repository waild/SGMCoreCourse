using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class GroupModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(4)]
        public string Name { get; set; }

        [Required] public int Year { get; set; }
    }
}