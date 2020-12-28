using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class GroupCourseModel
    {
        public int Id { get; set; }

        [Required] public int GroupId { get; set; }

        [Required] public int CourseId { get; set; }
    }
}