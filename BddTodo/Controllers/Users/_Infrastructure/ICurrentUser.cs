using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BddTodo.Controllers.Users._Infrastructure
{
    public interface ICurrentUser
    {
        int Id { get; }

        string FirstName { get; }

        string LastName { get; }

        string FullName { get; }

        string Email { get; }
    }
}
