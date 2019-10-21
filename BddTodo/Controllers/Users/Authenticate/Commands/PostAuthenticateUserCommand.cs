using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using BddTodo.Data;
using BddTodo._Infrastructure;
using BddTodo.Controllers.Users._Infrastructure.Passwords;

namespace BddTodo.Controllers.Users.Authenticate.Commands
{
    public class PostAuthenticateUserCommand : IRequest<PostAuthenticateUserCommandResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class PostAuthenticateUserCommandHandler : IRequestHandler<PostAuthenticateUserCommand, PostAuthenticateUserCommandResult>
    {
        private readonly IOptions<AppSettings> _appSettings;
        private readonly BddTodoDbContext _context;

        public PostAuthenticateUserCommandHandler(IOptions<AppSettings> appSettings, BddTodoDbContext context)
        {
            _appSettings = appSettings;
            _context = context;
        }

        public async Task<PostAuthenticateUserCommandResult> Handle(PostAuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.User
                .Include(x => x.UserRoles)
                .FirstOrDefaultAsync(x => x.Email.ToLower() == request.Username.ToLower()
                ,cancellationToken);

            if (user == null)
                return null;

            var roleId = (int)user.UserRoles.FirstOrDefault().UserRoleId;

            var doesPasswordMatch = Hash.Validate(request.Password, user.PasswordSalt, user.PasswordHash);
            if (!doesPasswordMatch)
                return null;

            // authentication successful. generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, roleId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateEncodedJwt(tokenDescriptor);
            return new PostAuthenticateUserCommandResult
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token
            };
        }
    }

    public class PostAuthenticateUserCommandResult
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
    }
}

