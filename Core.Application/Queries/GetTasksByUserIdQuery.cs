using Core.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Queries
{
    public class GetTasksForDashboardByUserIdQuery : IRequest<IEnumerable<TaskForDashboardDto>>
    {
        public int UserId { get; }

        public GetTasksForDashboardByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
