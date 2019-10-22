using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BddTodo.Controllers.TodoLists.Queries;
using BddTodo.Controllers.Todos.Commands;
using BddTodo.Controllers.Todos.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BddTodo.Controllers.Todos
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TodosListsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodosListsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<GetTodoListsResult>> Get([FromQuery]GetTodoListsQuery query)
        {
            return await _mediator.Send(query);
        }
       
    }
}
