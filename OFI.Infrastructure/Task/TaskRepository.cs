using Core.Entities.Task;
using Core.InterfaceRepository;
using Dapper;
using Microsoft.Extensions.Configuration;
using OFI.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFI.Infrastructure.Task
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDbConnection dbConnection;
        public TaskRepository(IConfiguration configuration) 
        { 
            dbConnection = ConnectionHelper.GetSqlConnection(configuration);
        }

        public async Task<TaskAggregate> AddAsync(TaskAggregate task)
        {
            string insertQuery = @"INSERT INTO Tasks (Name, Description, CreatedDate) OUTPUT INSERTED.ID VALUES (@Name, @Description, @CreatedDate)";

            var insertedId = await dbConnection.QuerySingleAsync<int>(insertQuery, task);

            task.Id = insertedId;

            return task;
        }

        public async Task<TaskAggregate> GetByIdAsync(int taskId)
        {
            var sql = "SELECT * FROM Tasks WHERE Id = @Id";
            return await dbConnection.QuerySingleOrDefaultAsync<TaskAggregate>(sql, new { Id = taskId });
        }

        public Task<IEnumerable<TaskAggregate>> GetByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

    }
}
