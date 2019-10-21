using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BddTodo.Controllers.Users.Register.Commands;

namespace BddTodo.Controllers.Users.Register
{
    [Route("api/users/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegisterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<int> Post([FromBody]PostRegisterCommand command)
        {
            return await _mediator.Send(command);
        }

    }
}
