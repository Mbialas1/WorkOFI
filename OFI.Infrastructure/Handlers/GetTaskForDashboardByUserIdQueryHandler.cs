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

namespace OFI.Infrastructure.Handlers
{
    public class GetTaskForDashboardByUserIdQueryHandler : IRequestHandler<GetTasksForDashboardByUserIdQuery, IEnumerable<TaskForDashboardDto>>
    {
        private readonly ITaskService _taskService;
        public GetTaskForDashboardByUserIdQueryHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<IEnumerable<TaskForDashboardDto>> Handle(GetTasksForDashboardByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _taskService.GetTaskForDashboardByUserId(request.UserId);
        }
    }
}
