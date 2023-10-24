using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Authorization.Models
{
    public class LoginComponent
    {
        public long Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public long IdUser {  get; set; }
        public string Salt { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
