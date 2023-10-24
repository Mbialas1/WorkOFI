using Core.Authorization.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Commands.Users
{
    public class LoginToApplicationCommand : IRequest<LoginComponentDTO>
    {
        public readonly LoginComponentDTO loginComponentDTO;
        public LoginToApplicationCommand(LoginComponentDTO _loginComponentDTO) 
        {
            loginComponentDTO = _loginComponentDTO; 
        }
    }
}
