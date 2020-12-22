using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyManager.Models
{
    [Table("Groups")]
    public class Group
    {
        [Key] [Column("Id")] public int Id { get; set; }

        [Column("Name")] public string Name { get; set; }

        [Column("Year")] public int Year { get; set; }
    }
}