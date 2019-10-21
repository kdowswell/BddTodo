using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BddTodo.Data.Models.Users.Reference
{
    public class UserAccountStatus
    {
        public UserAccountStatusEnum Id { get; set; }
        public string Title { get; set; }
    }

    public enum UserAccountStatusEnum
    {
        Pending = 1,
        Active = 2,
        Locked = 3
    }
}
