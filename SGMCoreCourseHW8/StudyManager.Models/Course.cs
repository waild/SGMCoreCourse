using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyManager.Models
{
    [Table("Courses")]
    public class Course
    {
        [Key] [Column("Id")] public int Id { get; set; }

        [Column("Name")] public string Name { get; set; }

        [Column("LectorName")] public string LectorName { get; set; }
    }
}