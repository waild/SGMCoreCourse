using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyManager.Models
{
    [Table("GroupCourses")]
    public class GroupCourse
    {
        [Key] [Column("Id")] public int Id { get; set; }

        [Column("GroupId")] public int GroupId { get; set; }

        [Column("CourseId")] public int CourseId { get; set; }
    }
}