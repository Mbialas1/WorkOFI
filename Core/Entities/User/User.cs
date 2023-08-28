using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.User
{
    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public int FailedLoginAttempts { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime? LockoutEnd { get; set; }
    }
}
