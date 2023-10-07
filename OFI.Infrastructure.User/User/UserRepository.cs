using Core.Dtos;
using Core.Entities.Task;
using Core.Entities.User;
using Core.InterfaceRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OFI.Infrastructure.Helpers;
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

        //TODO Return here normaln entities no DTO!
        public async Task<IEnumerable<UserDashboardDTO>> GetUserDashboardDTOsAsync()
        {
            logger.LogInformation($"START function : {nameof(GetUserDashboardDTOsAsync)} ");
            try
            {
                const string query = "SELECT Id, FirstName, LastName FROM Users";
                var users = await dbConnection.QueryAsync<UserDashboardDTO>(query);
                return users;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in funciton {nameof(GetUserDashboardDTOsAsync)}. More information: {ex.Message}");
                throw;
            }
        }
    }
}
