using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using NUnit.Framework;
using BddTodo.Controllers.Users.Authenticate.Commands;
using BddTodo.Controllers.Users.Register.Commands;
using BddTodo.Tests._Infrastructure;
using BddTodo.Tests._Infrastructure.Builders;
using Shouldly;
using TestStack.BDDfy;
using BddTodo.Tests;

namespace BddTodo.BehaviorTests.Users
{

    [Story(
        AsA = "As an unauthenticated user",
        IWant = "I want to login",
        SoThat = "so that i can manage my todo lists")]
    [TestFixture]
    public class UsersLoginTests : BaseTest
    {
        private PostAuthenticateUserCommand _command;
        private int _userId;
        private BadRequestServerResult _badRequestResult;
        private PostRegisterCommand _registerCommand;

        [Test]
        public void UnauthenticatedUserLogsIn()
        {
            this
                .Given(x => x.ARegisteredUser())
                .And(x => x.IProvideAllOfTheRequiredDataForTheLoginPage())
                .When(x => x.ISubmitTheLoginForm())
                .Then(x => x.IWillBeLoggedIn())
                .BDDfy();
        }

        void ARegisteredUser()
        {
            var user = Build.User.Build();
            _registerCommand = new PostRegisterCommand
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = "test123",
            };

            _userId = CallClientPost<int>("api/users/register", _registerCommand);
        }

        void IProvideAllOfTheRequiredDataForTheLoginPage()
        {
            _command = new PostAuthenticateUserCommand
            {
                Username = _registerCommand.Email,
                Password = _registerCommand.Password
            };
        }

        void ISubmitTheLoginForm()
        {
            try
            {
                Authenticate(_command.Username, _command.Password);
            }
            catch (Exception e)
            {
                _badRequestResult = JsonConvert.DeserializeObject<BadRequestServerResult>(e.Message);
            }
        }

        void IWillBeLoggedIn()
        {
            _badRequestResult.ShouldBeNull();
        }

    }

}
