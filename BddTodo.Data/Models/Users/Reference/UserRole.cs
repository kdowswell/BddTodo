using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BddTodo.Data.Models.Users.Reference
{
    public class UserRole
    {
        public UserRoleEnum Id { get; set; }
        public string Title { get; set; }
    }

    public enum UserRoleEnum
    {
        Standard = 1,
        Admin = 2,
    }
}
