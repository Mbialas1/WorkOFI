using Core.Application.Dtos;
using Core.Application.Queries;
using Core.Entities.Task;
using Core.Enums;
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
        private readonly ITaskRepository taskRepository;
        public GetTaskForDashboardByUserIdQueryHandler(ITaskRepository _taskRepository)
        {
            taskRepository = _taskRepository;
        }

        public async Task<IEnumerable<TaskForDashboardDto>> Handle(GetTasksForDashboardByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<CompleteTaskInfo> tasks = await taskRepository.GetTaskForDashboardDtos(request.UserId, request.Page, request.PageSize);

                IEnumerable<TaskForDashboardDto> tasksDto = tasks
                    .Select(task => new TaskForDashboardDto() {
                        Id = task.Id,
                        Name = task.Name,   
                        Description = task.Description, 
                        TaskStatus = ((TaskStatusEnum)task.TaskStatus).ToString(),
                        TotalRemaing = new TimeOnly(task.TotalRemaining.Hours, task.TotalRemaining.Minutes)
                    });

                return tasksDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
