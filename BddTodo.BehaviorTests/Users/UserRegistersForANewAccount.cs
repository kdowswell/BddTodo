using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using NUnit.Framework;
using BddTodo.Controllers.Users.Register.Commands;
using BddTodo.Tests._Infrastructure;
using BddTodo.Tests._Infrastructure.Builders;
using Shouldly;
using TestStack.BDDfy;
using BddTodo.Tests;

namespace BddTodo.BehaviorTests.Users
{

    [Story(
        AsA = "As a new user",
        IWant = "I want to register",
        SoThat = "So that i can manage my todo lists")]
    [TestFixture]
    public class UserRegistersForANewAccount : BaseTest
    {
        private PostRegisterCommand _command;
        private int _userId;
        private BadRequestServerResult _badRequestResult;

        [Test]
        public void UnauthenticatedUserRegisters()
        {
            this
                .Given(x => x.AnUnauthenticatedUser())
                .And(x => x.IProvideAllOfTheRequiredDataForTheRegistrationPage())
                .When(x => x.ISubmitTheRegistrationForm())
                .Then(x => x.IWillBeAddedToTheApplicationDatabase())
                .And(x => x.IWillBeLoggedIn())
                .BDDfy();
        }

        [Test]
        public void UnauthenticatedUserFailsToRegister()
        {
            this
                .Given(x => x.AnUnauthenticatedUser())
                .And(x => x.IProvideAllOfTheRequiredDataForTheRegistrationPage())
                .And(x => x.IForgetToProvideMyFirstName())
                .When(x => x.ISubmitTheRegistrationForm())
                .Then(x => x.IWillBeToldINeedToProvideMyFirstName())
                .BDDfy();
        }

        void AnUnauthenticatedUser()
        {

        }

        void IProvideAllOfTheRequiredDataForTheRegistrationPage()
        {
            var user = Build.User.Build();
            _command = new PostRegisterCommand
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = "test123",
            };
        }

        void ISubmitTheRegistrationForm()
        {
            try
            {
                _userId = CallClientPost<int>("api/users/register", _command);
                Authenticate(_command.Email, _command.Password);
            }
            catch (Exception e)
            {
                _badRequestResult = JsonConvert.DeserializeObject<BadRequestServerResult>(e.Message);
            }
        }

        void IWillBeAddedToTheApplicationDatabase()
        {
            Context.User.FirstOrDefault(x => x.Email == _command.Email).ShouldNotBeNull();
        }

        void IWillBeLoggedIn()
        {
            // TODO: Call authenticated endpoint
        }

        void IForgetToProvideMyFirstName()
        {
            _command.FirstName = null;
        }

        void IWillBeToldINeedToProvideMyFirstName()
        {
            _badRequestResult.errors.ShouldContain(x=> x.Key == "FirstName");
        }

    }

}
