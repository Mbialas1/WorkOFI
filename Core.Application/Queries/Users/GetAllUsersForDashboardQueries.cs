using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Queries.Users
{
    public class GetAllUsersForDashboardQueries : IRequest<IEnumerable<UserDashboardDTO>>
    {
        //Nothing. All users for test.
    }
}
