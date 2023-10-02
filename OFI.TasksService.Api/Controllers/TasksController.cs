using Core.Application.Commands;
using Core.Application.Dtos;
using Core.Application.Queries;
using Core.Dtos;
using Core.Enums;
using Core.InterfaceRepository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OFI.Infrastructure.Task;
using Serilog;
using Serilog.Data;
using System.Runtime.CompilerServices;

namespace OFI.TasksService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ITaskRepository taskRepository;
        private readonly ILogger<TasksController> logger;
        public TasksController(ITaskRepository _taskRepository, IMediator _mediator, ILogger<TasksController> _logger)
        {
            taskRepository = _taskRepository;
            this.mediator = _mediator;
            this.logger = _logger;
        }

        [HttpGet("task/{userId}")]
        public async Task<IEnumerable<TaskForDashboardDto>> GetTaskForDashboardByUserId(int userId)
        {
            logger.LogInformation($"{nameof(GetTaskForDashboardByUserId)} function just started");
            try
            {
                var tasks = await taskRepository.GetByUserIdAsync(userId);
                return tasks.Select(task => new TaskForDashboardDto
                {
                    Name = task.Name,
                    Description = task.Description,
                    TaskStatusStatus = (TaskStatusEnum)task.Progress
                }).ToList();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in {nameof(GetTaskForDashboardByUserId)} with detail error : {ex.Message} ");
                return null;
            }
        }

        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask([FromBody] AddTaskDto taskDto)
        {
            logger.LogInformation($"Start fucntion {nameof(AddTask)}");
            try
            {
                var command = new AddTaskCommand(taskDto);
                var result = await mediator.Send(command);

                if (result != null && result.Id > 0)
                    return Ok($"Task {taskDto.Name} add successful");
                else
                    throw new ArgumentNullException(nameof(taskDto));
            }
            catch (Exception ex)
            {
                logger.LogError($"{nameof(AddTask)} cant be add. More information about: {ex.Message}");
                return BadRequest($"Task {taskDto.Name} can't be added");
            }
        }

        [HttpGet("getTask/{taskId}")]
        public async Task<IActionResult> GetTaskById(int taskId)
        {
            logger.LogInformation($"Start fucntion {nameof(GetTaskById)}");
            try
            {
                var query = new GetTaskByIdQuery(taskId);
                var task = await mediator.Send(query);
                return Ok(task);
            }
            catch(Exception ex) {
                logger.LogError($"{nameof(GetTaskById)} cant find by id task. More information about: {ex.Message}");
                return BadRequest($"Task {taskId} can't be find");
            }
        }
    }
}
