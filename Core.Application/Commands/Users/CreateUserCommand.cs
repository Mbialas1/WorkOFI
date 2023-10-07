using Core.Entities.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Commands
{
    public class CreateUserCommand : IRequest<UserAggregate>
    {
        public string Name { get; set; }
    }
}
