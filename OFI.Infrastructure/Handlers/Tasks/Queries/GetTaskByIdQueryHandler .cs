using Core.Application.Queries;
using Core.Dtos;
using Core.Entities.Task;
using Core.Enums;
using Core.InterfaceRepository;
using Core.Services.InterfaceServices;
using MediatR;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFI.Infrastructure.Handlers.Tasks.Queries
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskForDetailsDto>
    {
        private readonly ITaskRepository taskRepository;
        private readonly IUserService userService;
        public GetTaskByIdQueryHandler(ITaskRepository _taskRepository, IUserService _userService)
        {
            taskRepository = _taskRepository;
            userService = _userService;
        }

        public async Task<TaskForDetailsDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                CompleteTaskInfo taskEntity = await taskRepository.GetByIdAsync(request.TaskId);

                if (taskEntity is null || !(taskEntity.Id > 0))
                    throw new ArgumentNullException($"Cant find task by id: {request.TaskId}.");

                string assigneUser = string.Empty;
                try
                {
                    UserDTO userDto = await userService.GetUserById(taskEntity.UserId);
                    assigneUser = $"{userDto.FirstName} {userDto.LastName}";
                }
                catch
                {
                    assigneUser = "Unkown assigne"; 
                }

                return new TaskForDetailsDto
                {
                    Id = taskEntity.Id,
                    Name = taskEntity.Name,
                    Description = taskEntity.Description,
                    CreatedDate = taskEntity.CreatedDate,
                    LastEditTime = DateTime.Now,
                    TottalRemaining = TimeOnly.Parse(taskEntity.TotalRemaining.ToString()),
                    TaskStatus = ((TaskStatusEnum)taskEntity.TaskStatus).ToString(),
                    NameOfUser = assigneUser
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
