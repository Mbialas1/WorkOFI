using Core.Application.Dtos;
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
        Task<IEnumerable<TaskForDashboardDto>> GetTaskForDashboardDtos(long userId);
        Task<IEnumerable<TaskAggregate>> GetByUserIdAsync(int userId);
        Task<TaskAggregate> AddAsync(TaskAggregate task);
        Task<CompleteTaskInfo> GetByIdAsync(long taskId);
    }
}
