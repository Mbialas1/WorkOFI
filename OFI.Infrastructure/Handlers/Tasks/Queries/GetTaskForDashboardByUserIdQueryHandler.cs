using Core.Application.Dtos;
using Core.Application.Queries;
using Core.InterfaceRepository;
using Core.Services.InterfaceServices;
using MediatR;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFI.Infrastructure.Handlers.Tasks.Queries
{
    public class GetTaskForDashboardByUserIdQueryHandler : IRequestHandler<GetTasksForDashboardByUserIdQuery, IEnumerable<TaskForDashboardDto>>
    {
        private readonly ITaskService taskService;
        public GetTaskForDashboardByUserIdQueryHandler(ITaskService _taskService)
        {
            taskService = _taskService;
        }

        public async Task<IEnumerable<TaskForDashboardDto>> Handle(GetTasksForDashboardByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await taskService.GetTaskForDashboardByUserId(request.UserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
