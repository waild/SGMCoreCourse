using StudyManager.Models;

namespace StudyManager.DataAccess.ADO
{
    public interface IGroupCoursesRepository: IBaseRepository<GroupCourse>
    {
    }

    public class GroupCoursesRepository : BaseRepository<GroupCourse>, IGroupCoursesRepository
    {
        public GroupCoursesRepository(string connectionString) : base(connectionString)
        {
        }
    }
}