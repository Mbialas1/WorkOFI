using Core.Application.Commands;
using Core.Entities.Task;
using Core.InterfaceRepository;
using Core.Services.InterfaceServices;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFI.Infrastructure.Handlers
{
    public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, TaskAggregate>
    {
        private readonly ITaskRepository _taskRepository;
        public AddTaskCommandHandler(ITaskRepository taskRepository) {
                _taskRepository = taskRepository;
        }

        public async Task<TaskAggregate> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var taskEntity = new TaskAggregate
                {
                    Name = request.TaskDto.Name,
                    Description = request.TaskDto.Description,
                    CreatedDate = request.TaskDto.Created
                    //Future: Add rest options
                };

                return await _taskRepository.AddAsync(taskEntity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
