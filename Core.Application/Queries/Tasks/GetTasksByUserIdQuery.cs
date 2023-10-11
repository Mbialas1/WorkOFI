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
        public long UserId { get; }
        public int Page { get; }
        public int PageSize { get; }
        public GetTasksForDashboardByUserIdQuery(long userId, int page, int pageSize)
        {
            UserId = userId;
            Page = page;
            PageSize = pageSize;
        }
    }
}
