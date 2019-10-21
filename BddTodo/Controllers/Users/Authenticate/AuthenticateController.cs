using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BddTodo.Controllers.Users.Authenticate.Commands;
using BddTodo.Data.Models.Users;

namespace BddTodo.Controllers.Users.Authenticate
{
    [Authorize]
    [ApiController]
    [Route("api/users/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody]PostAuthenticateUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (result == null)
                return BadRequest(new { message = "Username or password is incorrect." });

            return Ok(result);
        }

    }
}
