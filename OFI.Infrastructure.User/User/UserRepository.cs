using Core.Dtos;
using Core.Entities.Task;
using Core.Entities.User;
using Core.InterfaceRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OFI.Infrastructure.Helpers;
using OFI.Infrastructure.User.RabbitMQ.Models;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OFI.Infrastructure.User
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection dbConnection;
        private readonly ILogger<UserRepository> logger;
        public UserRepository(IConfiguration configuration, ILogger<UserRepository> _logger)
        {
            dbConnection = ConnectionHelper.GetSqlConnection(configuration);
            logger = _logger;
        }

        public System.Threading.Tasks.Task AddAsync(Core.Entities.User.UserAggregate user)
        {
            throw new NotImplementedException();
        }

        public async Task AddTaskToUser(TaskMessage taskMessage)
        {
            logger.LogInformation($"START function : {nameof(AddTaskToUser)} ");
            try
            {
                const string query = "INSERT INTO UsersTask (id_user, id_task) VALUES (@UserId, @TaskId); ";
                await dbConnection.ExecuteAsync(query, new { UserId = taskMessage.UserId, TaskId = taskMessage.TaskId });
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in funciton {nameof(AddTaskToUser)}. More information: {ex.Message}");
                throw;
            }
        }

        public async Task<Core.Entities.User.UserAggregate> GetAsync(long userId)
        {
            logger.LogInformation($"START function : {nameof(GetAsync)} ");
            try
            {
                const string query = "SELECT Id, FirstName, LastName FROM Users WHERE Id = @id";
                var user = await dbConnection.QuerySingleOrDefaultAsync<UserAggregate>(query, new { id = userId });
                return user;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in funciton {nameof(GetAsync)}. More information: {ex.Message}");
                throw;
            }
        }

        public Task<Core.Entities.User.UserAggregate> GetByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserAggregate>> GetUserDashboardDTOsAsync()
        {
            logger.LogInformation($"START function : {nameof(GetUserDashboardDTOsAsync)} ");
            try
            {
                const string query = "SELECT Id, FirstName, LastName FROM Users";
                var users = await dbConnection.QueryAsync<UserAggregate>(query);
                return users;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in funciton {nameof(GetUserDashboardDTOsAsync)}. More information: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<UserAggregate>> GetUsersToFiltr(string characters)
        {
            logger.LogInformation($"START function : {nameof(GetUsersToFiltr)} ");
            try
            {
                string query = @"SELECT TOP 10 * FROM Users 
                         WHERE CONTAINS((FirstName, LastName), @Characters) ORDER BY FirstName, LastName";

                var users = await dbConnection.QueryAsync<UserAggregate>(query, new { Characters = $"\"{characters}*\"" });

                return users;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in funciton {nameof(GetUsersToFiltr)}. More information: {ex.Message}");
                throw;
            }
        }
    }
}
