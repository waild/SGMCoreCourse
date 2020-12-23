using StudyManager.Models;

namespace StudyManager.DataAccess.ADO
{
    public class GroupCoursesRepository : BaseRepository<GroupCourse>, IBaseRepository<GroupCourse>
    {
        public GroupCoursesRepository(string connectionString) : base(connectionString)
        {
        }
    }
}