using Core.Dtos;
using Core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.InterfaceRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDashboardDTO>> GetUserDashboardDTOsAsync();
        Task<User> GetAsync(Guid id);
        Task<User> GetByUsernameAsync(string username);
        Task AddAsync(User user);
    }
}
