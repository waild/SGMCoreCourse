using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyManager.Models;

namespace StudyManager.DataAccess.ADO
{
    public class GroupsRepository : BaseRepository<Group>
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
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(SelectSingleQueryString, connection);
            connection.Open();

            command.Parameters.AddWithValue("@id", studentId);
            SqlDataReader reader = await command.ExecuteReaderAsync();

            List<Group> records = new List<Group>();
            while (await reader.ReadAsync())
            {
                records.Add(ParseFromReader(reader));
            }
            await reader.CloseAsync();
            return records.FirstOrDefault();
        }
    }
}
