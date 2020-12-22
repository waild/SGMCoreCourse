using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyManager.Models
{
    [Table("Students")]
    public class Student
    {
        [Key] [Column("Id")] public int Id { get; set; }

        [Column("GroupId")] public int GroupId { get; set; }

        [Column("FirstName")] public string FirstName { get; set; }

        [Column("LastName")] public string LastName { get; set; }

        [Column("BirthYear")] public int BirthYear { get; set; }

        [Column("AverageMark")] public int AverageMark { get; set; }
    }
}