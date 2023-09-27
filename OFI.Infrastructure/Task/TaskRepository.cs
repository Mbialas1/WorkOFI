using Core.Entities.Task;
using Core.InterfaceRepository;
using Dapper;
using Microsoft.Extensions.Configuration;
using OFI.Infrastructure.Helpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Specialized;

namespace OFI.Infrastructure.Task
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDbConnection dbConnection;
        private readonly ILogger<TaskRepository> logger;
        public TaskRepository(IConfiguration configuration, ILogger<TaskRepository> _logger)
        {
            dbConnection = ConnectionHelper.GetSqlConnection(configuration);
            logger = _logger;
        }

        public async Task<TaskAggregate> AddAsync(TaskAggregate task)
        {
            logger.LogInformation($"START function : {nameof(AddAsync)} ");
            try
            {
                string insertQuery = @"INSERT INTO Tasks (Name, Description, CreatedDate) OUTPUT INSERTED.ID VALUES (@Name, @Description, @CreatedDate)";

                var insertedId = await dbConnection.QuerySingleAsync<int>(insertQuery, task);

                task.Id = insertedId;

                return task;
            }
            catch(Exception ex)
            {
                logger.LogError($"Error in funciton {nameof(AddAsync)}. More information: {ex.Message}");
                throw;
            }
        }

        public async Task<TaskAggregate> GetByIdAsync(int taskId)
        {
            logger.LogInformation($"START function : {nameof(GetByIdAsync)} ");
            try
            {
                var sql = "SELECT * FROM Tasks WHERE Id = @Id";
                return await dbConnection.QuerySingleOrDefaultAsync<TaskAggregate>(sql, new { Id = taskId });
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in funciton {nameof(GetByIdAsync)}. More information: {ex.Message}");
                throw;
            }
        }

        public Task<IEnumerable<TaskAggregate>> GetByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

    }
}
