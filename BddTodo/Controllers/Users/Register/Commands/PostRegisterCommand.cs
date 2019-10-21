using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BddTodo.Controllers.Users._Infrastructure.Passwords;
using BddTodo.Data;
using BddTodo.Data.Models.Users;
using BddTodo.Data.Models.Users.ManyToMany;
using BddTodo.Data.Models.Users.Reference;

namespace BddTodo.Controllers.Users.Register.Commands
{
    public class PostRegisterCommand : IRequest<int>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }

    public class PostRegisterCommandHandler : IRequestHandler<PostRegisterCommand, int>
    {
        private readonly BddTodoDbContext _context;

        public PostRegisterCommandHandler(BddTodoDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(PostRegisterCommand request, CancellationToken cancellationToken)
        {
            var passwordSalt = Salt.Create();
            var passwordHash = Hash.Create(request.Password, passwordSalt);

            var user = new User
            {
                UserAccountStatusId = UserAccountStatusEnum.Active,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedDate = DateTime.Now,
                UserRoles = new List<UserRoles>
                {
                    new UserRoles
                    {
                        UserRoleId = UserRoleEnum.Standard
                    }
                }
            };

            _context.User.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
