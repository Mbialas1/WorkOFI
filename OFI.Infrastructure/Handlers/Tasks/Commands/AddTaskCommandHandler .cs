using Core.Application.Commands;
using Core.Entities.Task;
using Core.InterfaceRepository;
using Core.Services.InterfaceServices;
using MediatR;
using OFI.Infrastructure.RabbitMQ;
using RabbitMQ.Client;
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
        private readonly IModel channel;
        private const string MESSAGE_NAME_QUEUE = "TaskMessage";
        public AddTaskCommandHandler(ITaskRepository taskRepository, IUserService _userService, IModel _channel)
        {
            _taskRepository = taskRepository;
            userService = _userService;
            channel = _channel;
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

                var newTask =  await _taskRepository.AddAsync(taskEntity);

                if(newTask != null)
                {
                    var message = new
                    {
                        TaskId = newTask.Id,
                        UserId = newTask.UserId
                    };
                    //TODO Container here for resolved by interface
                    var rabbitMsg = new TaskRabbitHandler(channel);
                    rabbitMsg.SendMessage(message, Common.ServicesNameEnum.UsersService, MESSAGE_NAME_QUEUE);
                }

                return newTask;
            }
            catch
            {
                throw;
            }
        }
    }
}
