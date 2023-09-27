using Core.Application.Queries;
using Core.Dtos;
using Core.InterfaceRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFI.Infrastructure.Handlers
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskForDetailsDto>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTaskByIdQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskForDetailsDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var taskEntity = await _taskRepository.GetByIdAsync(request.TaskId);

            return new TaskForDetailsDto
            {
                Id = taskEntity.Id,
                Name = taskEntity.Name,
                Description = taskEntity.Description,
                CreatedDate = taskEntity.CreatedDate
            };
        }
    }
}
