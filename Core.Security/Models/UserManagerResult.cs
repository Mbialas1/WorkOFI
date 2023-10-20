using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Models
{
    public class UserManagerResult
    {
        public bool Succeeded { get; protected set; }

        public List<string> Errors { get; private set; } = new List<string>();

        public static UserManagerResult Success { get; }
            = new UserManagerResult() { Succeeded = true };

        public static UserManagerResult Failed(List<string> errors)
        {
            return new UserManagerResult()
            { Succeeded = false, Errors = errors };
        }
    }
}
