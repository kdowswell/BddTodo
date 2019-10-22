using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BddTodo.Controllers.Todos.Commands
{
    public class DeleteTodoCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
