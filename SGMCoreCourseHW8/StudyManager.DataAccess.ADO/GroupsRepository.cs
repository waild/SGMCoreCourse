using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using StudyManager.Models;

namespace StudyManager.DataAccess.ADO
{
    public interface IGroupsRepository: IBaseRepository<Group>
    {
        Task<Group> GetStudentGroup(int studentId);
    }

    public class GroupsRepository : BaseRepository<Group>, IGroupsRepository
    {
        public GroupsRepository(string connectionString) : base(connectionString)
        {
        }


        protected virtual string SelectGroupByStudentQueryString => @"
            SELECT G.Id, G.Name, G.Year
            FROM Groups as G
            LEFT JOIN Students as S
	            ON S.GroupId = G.Id
            WHERE S.Id = @id";

        public async Task<Group> GetStudentGroup(int studentId)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(SelectSingleQueryString, connection);
            connection.Open();

            command.Parameters.AddWithValue("@id", studentId);
            var reader = await command.ExecuteReaderAsync();

            var records = new List<Group>();
            while (await reader.ReadAsync()) records.Add(ParseFromReader(reader));
            await reader.CloseAsync();
            return records.FirstOrDefault();
        }
    }
}