using Core.Application.Commands;
using Core.Application.Dtos;
using Core.Application.Queries;
using Core.Dtos;
using Core.Enums;
using Core.InterfaceRepository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OFI.Infrastructure.Task;

namespace OFI.TasksService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ITaskRepository taskRepository;
        public TasksController(ITaskRepository _taskRepository, IMediator _mediator)
        {
            taskRepository = _taskRepository;
            this.mediator = _mediator;
        }

        [HttpGet("task/{userId}")]
        public async Task<IEnumerable<TaskForDashboardDto>> GetTaskForDashboardByUserId(int userId)
        {
            var tasks = await taskRepository.GetByUserIdAsync(userId);
            return tasks.Select(task => new TaskForDashboardDto
            {
                Name = task.Name,
                Description = task.Description,
                TaskStatusStatus = (TaskStatusEnum)task.Progress
            }).ToList();
        }

        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask([FromBody] AddTaskDto taskDto)
        {
            var command = new AddTaskCommand(taskDto);
            var result = await mediator.Send(command);

            if (result != null && result.Id > 0)
                return Ok($"Task {taskDto.Name} add successful");

            return BadRequest($"Task {taskDto.Name} can't be added");
        }

        [HttpGet("getTask/{taskId}")]
        public async Task<IActionResult> GetTaskById(int taskId)
        {
            var query = new GetTaskByIdQuery(taskId);
            var task = await mediator.Send(query);
            return Ok(task);
        }
    }
}
