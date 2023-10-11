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
using Core.Dtos;
using System.ComponentModel.DataAnnotations;

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

                string insertQuery = @"INSERT INTO Tasks (Name, Description, CreatedDate, UserId) OUTPUT INSERTED.ID VALUES (@Name, @Description, @CreatedDate, @UserId)";
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
            catch (Exception ex)
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

        public async Task<TimeOnly> GetLoggedTimeByIdTask(long taskId)
        {
            logger.LogInformation($"Start function : {nameof(GetLoggedTimeByIdTask)} ");
            try
            {
                string query = @"SELECT TotalRemaining FROM TaskRemainingTimes WHERE TaskId = @taskId";
                TimeSpan result = await dbConnection.QuerySingleAsync<TimeSpan>(query, new { TaskId = taskId });
                return new TimeOnly(result.Hours, result.Minutes);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in fucntion {nameof(GetLoggedTimeByIdTask)}. More information: {ex.Message} ");
                throw;
            }
        }

        public async Task<IEnumerable<LogTimeTask>> GetLogTimeTasksByTaskId(long taskId, int page = 1, int pageSize = 10)
        {
            logger.LogInformation($"Start function : {nameof(GetLogTimeTasksByTaskId)} ");
            try
            {
                int offset = (page - 1) * pageSize;

                string query = @"SELECT * FROM LoggedTimes WHERE TaskId = @TaskId ORDER BY Id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
                IEnumerable<LogTimeTask> result = await dbConnection.QueryAsync<LogTimeTask>(query, new { TaskId = taskId, Offset = offset, PageSize = pageSize });
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in fucntion {nameof(GetLogTimeTasksByTaskId)}. More information: {ex.Message} ");
                throw;
            }
        }

        public async Task<IEnumerable<CompleteTaskInfo>> GetTaskForDashboardDtos(long userId, int pageNumber = 1, int pageSize = 10)
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
                query.Append("ORDER BY t.Id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

                IEnumerable<CompleteTaskInfo> tasksDb = await dbConnection.QueryAsync<CompleteTaskInfo>(query.ToString(), new { UserId = userId, Offset = (pageNumber - 1) * pageSize, PageSize = pageSize });
                return tasksDb;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in fucntion {nameof(GetTaskForDashboardDtos)}. More information: {ex.Message} ");
                throw;
            }
        }

        public async Task<bool> LogTimeToTaskById(LogTimeTask model)
        {
            logger.LogInformation($"Start function : {nameof(LogTimeToTaskById)} ");
            try
            {

                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                //BEGIN TRANS
                using var transaction = dbConnection.BeginTransaction();

                string query = @"INSERT INTO LoggedTimes (TaskId, LoggedTime, LoggedDate) OUTPUT INSERTED.ID VALUES (@taskId, @loggedTime, @loggedDate)";
                int result = await dbConnection.QuerySingleAsync<int>(query, model, transaction);
                if (result == 0)
                {
                    logger.LogError($"Error in repository function {LogTimeToTaskById}");
                    transaction.Rollback();
                    throw new ArgumentNullException("Cant add no information about logg time");
                }

                query = @"UPDATE TaskRemainingTimes set TotalRemaining = @totalRemaining where TaskId = @taskId";
                result = await dbConnection.ExecuteAsync(query, new { totalRemaining = new TimeSpan(model.TaskTime.Hour, model.TaskTime.Minute, 0), taskId = model.TaskId }, transaction);

                if (result > 0)
                    transaction.Commit();
                else
                {
                    logger.LogError($"Error in repository function {LogTimeToTaskById}");
                    transaction.Rollback();
                    throw new ArgumentNullException("Cant add no information about logg time");
                }

                return result > 0;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in fucntion {nameof(LogTimeToTaskById)}. More information: {ex.Message} ");
                throw;
            }
        }

        public async Task<bool> UpdateTaskStatus(UpdateTaskStatusDTO taskStatusDTO)
        {
            logger.LogInformation($"Start function : {nameof(UpdateTaskStatus)} ");
            try
            {
                string query = @"update TaskStatuses set TaskStatus = @StatusTaskId where TaskId = @idTask";
                var result = await dbConnection.QueryAsync<bool>(query, new { StatusTaskId = taskStatusDTO.StatusTaskId, idTask = taskStatusDTO.TaskId });
                return result != null;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in fucntion {nameof(UpdateTaskStatus)}. More information: {ex.Message} ");
                throw;
            }
        }
    }
}
