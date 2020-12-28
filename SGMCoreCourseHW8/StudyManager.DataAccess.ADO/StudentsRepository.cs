using StudyManager.Models;

namespace StudyManager.DataAccess.ADO
{
    public interface IStudentsRepository: IBaseRepository <Student>
    {
    }

    public class StudentsRepository : BaseRepository<Student>, IStudentsRepository
    {
        public StudentsRepository(string connectionString) : base(connectionString)
        {
        }
    }
}