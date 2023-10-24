using Core.Authorization.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Commands.Users
{
    public class RegisterToApplicationCommand : IRequest<bool>
    {
        public readonly RegisterLoginComponentDTO registerLoginComponentDTO;
        public RegisterToApplicationCommand(RegisterLoginComponentDTO _registerLoginComponentDTO) 
        {
            registerLoginComponentDTO = _registerLoginComponentDTO;
        }
    }
}
