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
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                //BEGIN TRANS
                using var transaction = dbConnection.BeginTransaction();

                string insertQuery = @"INSERT INTO Tasks (Name, Description, CreatedDate, UserId) OUTPUT INSERTED.ID VALUES (@Name, @Description, @CreatedDate, @AssignedUserId)";
                var insertedId = await dbConnection.QuerySingleAsync<int>(insertQuery, task, transaction);

                string insertStatusQuery = @"INSERT INTO TaskStatuses (TaskId, TaskStatus) VALUES (@TaskId, @Status)";
                await dbConnection.ExecuteAsync(insertStatusQuery, new { TaskId = insertedId, Status = 4 }, transaction);

                TimeSpan defaultTime = new TimeSpan(0, 0, 0);

                string insertRemainingTimeQuery = @"INSERT INTO TaskRemainingTimes (TotalRemaining, TaskId) VALUES (@TotalRemaining, @TaskId)";
                await dbConnection.ExecuteAsync(insertRemainingTimeQuery, new { TotalRemaining = defaultTime, TaskId = insertedId }, transaction);


                transaction.Commit();
                //END TRANS

                task.Id = insertedId;

                return task;
            }
            catch(Exception ex)
            {
                logger.LogError($"Error in funciton {nameof(AddAsync)}. More information: {ex.Message}");
                throw;
            }
        }

        public async Task<CompleteTaskInfo> GetByIdAsync(long _taskId)
        {
            logger.LogInformation($"START function : {nameof(GetByIdAsync)} ");
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append("SELECT t.Id, t.Name, t.Description, t.UserId, t.CreatedDate, tr.TotalRemaining, ts.TaskStatus ");
                query.Append("FROM Tasks t ");
                query.Append("LEFT JOIN TaskRemainingTimes tr ON t.Id = tr.TaskId ");
                query.Append("LEFT JOIN TaskStatuses ts ON t.Id = ts.TaskId ");
                query.Append("WHERE t.Id = @taskId ");

                return await dbConnection.QuerySingleOrDefaultAsync<CompleteTaskInfo>(query.ToString(), new { taskId = _taskId });
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

        //TODO Dont return dto's files
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
