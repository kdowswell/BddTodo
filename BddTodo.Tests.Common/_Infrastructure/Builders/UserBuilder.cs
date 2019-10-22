using System;
using System.Collections.Generic;
using Bogus;
using BddTodo.Controllers.Users._Infrastructure.Passwords;
using BddTodo.Data.Models.Users.ManyToMany;
using BddTodo.Data.Models.Users.Reference;

namespace BddTodo.Tests._Infrastructure.Builders
{
    public partial class UserBuilder
    {
        protected override void Defaults()
        {
            var faker = new Faker();

            dto.UserAccountStatusId = UserAccountStatusEnum.Active;
            dto.FirstName = faker.Name.FirstName();
            dto.LastName = faker.Name.LastName();
            dto.Email = faker.Person.Email;
            dto.UserRoles = new List<UserRoles>
            {
                new UserRoles
                {
                    UserRoleId = UserRoleEnum.Standard
                }
            };

            var passwordSalt = Salt.Create();
            var passwordHash = Hash.Create("testpw", passwordSalt);

            dto.PasswordSalt = passwordSalt;
            dto.PasswordHash = passwordHash;

        }
    }
}
