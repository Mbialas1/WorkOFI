using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.InterfaceServices
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskForDashboardDto>> GetTaskForDashboardByUserId(int userId);
    }
}
