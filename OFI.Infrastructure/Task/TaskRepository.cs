using Core.Entities.Task;
using Core.InterfaceRepository;
using Dapper;
using Microsoft.Extensions.Configuration;
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
using OFI.Infrastructure.Helpers;
using Core.Application.Dtos;
using Core.Enums;

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

        public async Task<IEnumerable<TaskForDashboardDto>> GetTaskForDashboardDtos(long userId)
        {
            logger.LogInformation($"Start function : {nameof(GetTaskForDashboardDtos)} ");
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT t.Id, t.Name, t.Description, s.TaskStatus, r.TotalRemaining ");
                query.Append("FROM Tasks t ");
                query.Append("JOIN TaskStatuses s ON t.Id = s.TaskId ");
                query.Append("JOIN TaskRemainingTimes r ON t.Id = r.TaskId ");
                query.Append("WHERE t.UserId = @UserId ");
                
                IEnumerable<TaskForDashboardDto> tasksFromDb =  await dbConnection.QueryAsync<TaskForDashboardDto>(query.ToString(), new { UserId  = userId});
                tasksFromDb.ToList().ForEach(task => task.TaskStatus = ((TaskStatusEnum)int.Parse(task.TaskStatus)).ToString());
                return tasksFromDb;
            }
            catch(Exception ex)
            {
                logger.LogError($"Error in fucntion {nameof(GetTaskForDashboardDtos)}. More information: {ex.Message} ");
                throw;
            }
        }
    }
}
