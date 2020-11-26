using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace StudyManager.DataAccess.ADO
{
    public abstract class BaseRepository<T> where T: new()
    {
        protected readonly string connectionString;

        protected BaseRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public async Task<List<T>> GetAllAsync()
        {
            List<T> records = new List<T>();
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(SelectQueryString, connection);
            await connection.OpenAsync();
            SqlDataReader reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                records.Add(ParseFromReader(reader));
            }
            await reader.CloseAsync();
            return records;
        }

        public async Task<T> GetSingleAsync(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(SelectSingleQueryString, connection);
            await connection.OpenAsync();

            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = await command.ExecuteReaderAsync();

            List<T> records = new List<T>();
            while (await reader.ReadAsync())
            {
                records.Add(ParseFromReader(reader));
            }
            await reader.CloseAsync();
            return records.FirstOrDefault();
        }


        protected virtual string SelectQueryString => $"SELECT {String.Join(", ", Tools.Properties<T>().Values)} FROM {Tools.GetTableName<T>()}";
        protected virtual string SelectSingleQueryString => $"{SelectQueryString} WHERE Id = @id";
        protected virtual string DeleteQueryString => $"DELETE FROM {Tools.GetTableName<T>()} WHERE Id = @id";
        protected virtual string UpdateQueryString => $"UPDATE {Tools.GetTableName<T>()} SET {String.Join(", ", Tools.GetPropertiesWithoutKey<T>().Values.Select((x, index) => x + " = @p" + index))} WHERE id = @id";

        protected virtual string InsertQueryString =>
            $"INSERT INTO {Tools.GetTableName<T>()} ({String.Join(", ", Tools.GetPropertiesWithoutKey<T>().Values)}) " +
            "OUTPUT Inserted.Id " +
            $"VALUES ({String.Join(", ", Tools.GetPropertiesWithoutKey<T>().Values.Select((x, index) => "@p" + index))})";

        protected virtual void FillCreateCommand(SqlCommand cmd, T entity)
        {
            int i = 0;
            foreach (var keyValuePair in Tools.GetPropertiesWithoutKey<T>())
            {
                cmd.Parameters.AddWithValue($"@p{i}", Tools.GetPropValue<T>(entity, keyValuePair.Key));
                i++;
            }
        }

        protected virtual void FillUpdateCommand(SqlCommand cmd, T entity)
        {
            int i = 0;
            foreach (var keyValuePair in Tools.GetPropertiesWithoutKey<T>())
            {
                cmd.Parameters.AddWithValue($"@p{i}", Tools.GetPropValue(entity, keyValuePair.Key));
                i++;
            }

            var id = Tools.GetKeyValue<T>(entity);
            cmd.Parameters.AddWithValue("@id", id);
        }

        protected virtual T ParseFromReader(SqlDataReader reader)
        {
            var entity = new T();
            int i = 0;
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
                using SqlConnection connection = new SqlConnection(connectionString);
                using SqlCommand command = new SqlCommand(InsertQueryString, connection);
                FillCreateCommand(command, entity);
                await connection.OpenAsync();
                id = (int) await command.ExecuteScalarAsync();
            }
            return await GetSingleAsync(id);
        }

    }
}