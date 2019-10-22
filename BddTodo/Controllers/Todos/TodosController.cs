using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class TodosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<GetTodosResult>> Get([FromQuery]GetTodosQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> Post([FromBody]PostTodoCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task<bool> Put([FromBody]PutTodoCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<bool> Delete(int id)
        {
            return await _mediator.Send(new DeleteTodoCommand { Id = id });
        }
    }
}
