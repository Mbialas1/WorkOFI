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

namespace OFI.Infrastructure.Handlers.Tasks.Commands
{
    public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, TaskAggregate>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserService userService;
        public AddTaskCommandHandler(ITaskRepository taskRepository, IUserService _userService)
        {
            _taskRepository = taskRepository;
            userService = _userService;
        }

        public async Task<TaskAggregate> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool userExist = await userService.UserExists(request.TaskDto.AssignedUserId);
                if(!userExist)
                {
                    throw new ArgumentNullException("User doesnt exist!");
                }

                var taskEntity = new TaskAggregate
                {
                    Name = request.TaskDto.Name,
                    Description = request.TaskDto.Description,
                    CreatedDate = DateTime.UtcNow,
                    UserId = request.TaskDto.AssignedUserId
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
