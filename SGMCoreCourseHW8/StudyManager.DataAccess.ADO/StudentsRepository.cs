using StudyManager.Models;

namespace StudyManager.DataAccess.ADO
{
    public class StudentsRepository : BaseRepository<Student>
    {
        public StudentsRepository(string connectionString) : base(connectionString)
        {
        }
    }
}