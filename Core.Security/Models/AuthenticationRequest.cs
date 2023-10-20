using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Models
{
    public class AuthenticationRequest
    {
        public string Password { get; set; } = string.Empty;
    }
}
