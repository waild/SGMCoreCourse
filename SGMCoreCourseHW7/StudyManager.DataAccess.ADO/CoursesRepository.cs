using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StudyManager.Models;

namespace StudyManager.DataAccess.ADO
{
    public class CoursesRepository : BaseRepository<Course>
    {
        public CoursesRepository(string connectionString) : base(connectionString)
        {
        }

        protected virtual string SelectByStudentQueryString => 
        @"SELECT C.Id, C.Name, C.LectorName
            FROM Courses as C
            LEFT JOIN GroupCourses as GC
            ON GC.CourseId = C.Id
            LEFT JOIN Groups as G
            ON G.Id = GC.GroupId
            LEFT JOIN Students as S
            ON S.GroupId = G.Id
            WHERE S.Id = @id";
        public async Task<List<Course>> GetStudentCourses(int studentId)
        {
            List<Course> records = new List<Course>();
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(SelectByStudentQueryString, connection);
            command.Parameters.AddWithValue("@id", studentId);
            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                records.Add(ParseFromReader(reader));
            }
            return records;
        }
    }
}
