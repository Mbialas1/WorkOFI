using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Authorization.Dtos
{
    public class LoginComponentDTO
    {
        public string Login {  get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
