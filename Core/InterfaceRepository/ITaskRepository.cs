﻿using Core.Application.Dtos;
using Core.Dtos;
using Core.Entities.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.InterfaceRepository
{
    public interface ITaskRepository
    {
        Task<bool> UpdateTaskStatus(UpdateTaskStatusDTO taskStatusDTO);
        Task<IEnumerable<CompleteTaskInfo>> GetTaskForDashboardDtos(long userId, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<TaskAggregate>> GetByUserIdAsync(int userId);
        Task<TaskAggregate> AddAsync(TaskAggregate task);
        Task<CompleteTaskInfo> GetByIdAsync(long taskId);
        Task<TimeOnly> GetLoggedTimeByIdTask(long taskId);
        Task<bool> LogTimeToTaskById(LogTimeTask model);
        Task<IEnumerable<LogTimeTask>> GetLogTimeTasksByTaskId(long taskId, int page = 1, int pageSize = 10);
    }
}
