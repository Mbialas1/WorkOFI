﻿using Core.Application.Commands;
using Core.Application.Commands.Tasks;
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

        [HttpPut("task/changeStatus")]
        public async Task<IActionResult> UpdateTaskStatus([FromBody] UpdateTaskStatusDTO taskStatusDto)
        {
            logger.LogInformation($"{nameof(UpdateTaskStatus)} function just started");
            try
            {
                if(taskStatusDto.StatusTaskId  > 5 || !(taskStatusDto.TaskId > 0))
                {
                    logger.LogError($"{nameof(UpdateTaskStatus)} wrong parameters");
                    return BadRequest("Wrong parameters");
                }

                var command = new ChangeStatusTaskCommand(taskStatusDto);
                mediator.Send(command);

                return Accepted();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in {nameof(UpdateTaskStatus)} with detail error : {ex.Message} ");
                return BadRequest();
            }
        }

        [HttpGet("dashboard/{userId}")]
        public async Task<ActionResult<IEnumerable<TaskForDashboardDto>>> GetTaskForDashboardByUserId(int userId)
        {
            logger.LogInformation($"{nameof(GetTaskForDashboardByUserId)} function just started");
            try
            {
                if (userId < 1)
                {
                    logger.LogError($"{nameof(GetTaskForDashboardByUserId)} userd id cant be less than 1");
                    return BadRequest($"inncoretc user id = {userId}");
                }

                var query = new GetTasksForDashboardByUserIdQuery(userId);
                var result = await mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in {nameof(GetTaskForDashboardByUserId)} with detail error : {ex.Message} ");
                return BadRequest();
            }
        }

        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask([FromBody] AddTaskDto taskDto)
        {
            logger.LogInformation($"Start fucntion {nameof(AddTask)}");
            try
            {
                if(taskDto.AssignedUserId < 1)
                {
                    logger.LogError($"{nameof(AddTask)} assigned user id cant be null or less than 1");
                    return BadRequest("Invalid AssignedUserId");
                }

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
        public async Task<ActionResult<TaskForDetailsDto>> GetTaskById(int taskId)
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
