using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudyManager.Models
{

    [Table("GroupCourses")]
    public class GroupCourse
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("GroupId")]
        public int GroupId { get; set; }

        [Column("CourseId")]
        public int CourseId { get; set; }
    }
}
