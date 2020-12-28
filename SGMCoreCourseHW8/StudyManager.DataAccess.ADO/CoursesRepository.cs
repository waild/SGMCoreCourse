using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using StudyManager.Models;

namespace StudyManager.DataAccess.ADO
{
    public interface ICoursesRepository: IBaseRepository<Course>
    {
        Task<List<Course>> GetStudentCourses(int studentId);
    }

    public class CoursesRepository : BaseRepository<Course>, ICoursesRepository
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
            var records = new List<Course>();
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(SelectByStudentQueryString, connection);
            command.Parameters.AddWithValue("@id", studentId);
            connection.Open();
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync()) records.Add(ParseFromReader(reader));
            return records;
        }
    }
}