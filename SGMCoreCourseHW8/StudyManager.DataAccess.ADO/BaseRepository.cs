using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudyManager.DataAccess.ADO
{
    public interface IBaseRepository<T> where T : new()
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetSingleAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(T entity);
        Task<T> CreateAsync(T entity);
    }

    public abstract class BaseRepository<T> : IBaseRepository<T> where T : new()
    {
        protected readonly string connectionString;

        protected BaseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        protected virtual string SelectQueryString =>
            $"SELECT {string.Join(", ", Tools.Properties<T>().Values)} FROM {Tools.GetTableName<T>()}";

        protected virtual string SelectSingleQueryString => $"{SelectQueryString} WHERE Id = @id";
        protected virtual string DeleteQueryString => $"DELETE FROM {Tools.GetTableName<T>()} WHERE Id = @id";

        protected virtual string UpdateQueryString =>
            $"UPDATE {Tools.GetTableName<T>()} SET {string.Join(", ", Tools.GetPropertiesWithoutKey<T>().Values.Select((x, index) => x + " = @p" + index))} WHERE id = @id";

        protected virtual string InsertQueryString =>
            $"INSERT INTO {Tools.GetTableName<T>()} ({string.Join(", ", Tools.GetPropertiesWithoutKey<T>().Values)}) " +
            "OUTPUT Inserted.Id " +
            $"VALUES ({string.Join(", ", Tools.GetPropertiesWithoutKey<T>().Values.Select((x, index) => "@p" + index))})";

        public async Task<List<T>> GetAllAsync()
        {
            var records = new List<T>();
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(SelectQueryString, connection);
            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            while (reader.Read()) records.Add(ParseFromReader(reader));
            await reader.CloseAsync();
            return records;
        }

        public async Task<T> GetSingleAsync(int id)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(SelectSingleQueryString, connection);
            await connection.OpenAsync();

            command.Parameters.AddWithValue("@id", id);
            var reader = await command.ExecuteReaderAsync();

            var records = new List<T>();
            while (await reader.ReadAsync()) records.Add(ParseFromReader(reader));
            await reader.CloseAsync();
            return records.FirstOrDefault();
        }

        protected virtual void FillCreateCommand(SqlCommand cmd, T entity)
        {
            var i = 0;
            foreach (var keyValuePair in Tools.GetPropertiesWithoutKey<T>())
            {
                cmd.Parameters.AddWithValue($"@p{i}", Tools.GetPropValue(entity, keyValuePair.Key));
                i++;
            }
        }

        protected virtual void FillUpdateCommand(SqlCommand cmd, T entity)
        {
            var i = 0;
            foreach (var keyValuePair in Tools.GetPropertiesWithoutKey<T>())
            {
                cmd.Parameters.AddWithValue($"@p{i}", Tools.GetPropValue(entity, keyValuePair.Key));
                i++;
            }

            var id = Tools.GetKeyValue(entity);
            cmd.Parameters.AddWithValue("@id", id);
        }

        protected virtual T ParseFromReader(SqlDataReader reader)
        {
            var entity = new T();
            var i = 0;
            foreach (var keyValuePair in Tools.Properties<T>())
            {
                Tools.SetPropValue(entity, keyValuePair.Key, reader.GetValue(i));
                i++;
            }

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(DeleteQueryString, connection);
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@id", id);
            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            using var command = new SqlCommand(UpdateQueryString, connection);
            FillUpdateCommand(command, entity);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            int id;
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand(InsertQueryString, connection);
                FillCreateCommand(command, entity);
                await connection.OpenAsync();
                id = (int) await command.ExecuteScalarAsync();
            }
            return await GetSingleAsync(id);
        }
    }
}