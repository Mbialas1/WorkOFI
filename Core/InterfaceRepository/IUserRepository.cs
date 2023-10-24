using Core.Authorization.Dtos;
using Core.Authorization.Models;
using Core.Dtos;
using Core.Entities.User;
using OFI.Infrastructure.User.RabbitMQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.InterfaceRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserAggregate>> GetUserDashboardDTOsAsync();
        Task<UserAggregate> GetAsync(long id);
        Task<UserAggregate> GetByUsernameAsync(string username);
        Task AddAsync(UserAggregate user);
        Task<IEnumerable<UserAggregate>> GetUsersToFiltr(string characters);
        Task AddTaskToUser(TaskMessage taskMessage);
        Task<LoginComponent> LogToApplication(string userName);
        Task<long?> AddUserToApplication(UserAggregate user);
        Task<bool> AddLoginComponent(LoginComponent loginComponent);
    }
}
