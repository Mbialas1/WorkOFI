using Core.Application.Queries.Users;
using Core.Dtos;
using Core.InterfaceRepository;
using Core.Services.InterfaceServices;
using Core.Services.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFI.Infrastructure.Handlers.Users.Queries
{
    public class GetAllUsersForDashboardQueriesHandler : IRequestHandler<GetAllUsersForDashboardQueries, IEnumerable<UserDashboardDTO>>
    {
        private readonly IUserRepository userRepository;
        public GetAllUsersForDashboardQueriesHandler(IUserRepository _userRepository) {
            userRepository = _userRepository;
        }

        public async Task<IEnumerable<UserDashboardDTO>> Handle(GetAllUsersForDashboardQueries request, CancellationToken cancellationToken)
        {
            try
            {
                var taskEntity = await userRepository.GetUserDashboardDTOsAsync();

                return taskEntity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
