using StudyManager.Models;

namespace StudyManager.DataAccess.ADO
{
    public class StudentsRepository : BaseRepository<Student>, IBaseRepository<Student>
    {
        public StudentsRepository(string connectionString) : base(connectionString)
        {
        }
    }
}