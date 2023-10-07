using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Queries.Users
{
    public class GetUserByIdQueries : IRequest<UserDTO>
    {
        public readonly long UserId;

        public GetUserByIdQueries(long userId)
        {
             UserId = userId;
        }
    }
}
