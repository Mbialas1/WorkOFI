using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserDTO(long id, string firsName, string lastName)
        {
            Id = id;
            FirstName = firsName;
            LastName = lastName;
        }
    }
}
