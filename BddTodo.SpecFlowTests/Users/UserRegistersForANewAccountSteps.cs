using BddTodo.Tests;
using System;
using TechTalk.SpecFlow;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using BddTodo.Controllers.Users.Register.Commands;
using BddTodo.Tests._Infrastructure;
using BddTodo.Tests._Infrastructure.Builders;
using Shouldly;
using BddTodo.Tests;

namespace BddTodo.SpecFlowTests.Users
{
    [Binding]
    public class UserRegistersForANewAccountSteps : BaseTest
    {
        private PostRegisterCommand _command;
        private int _userId;
        private BadRequestServerResult _badRequestResult;

        [Given]
        public void GivenAnUnauthenticatedUser()
        {
        }
        
        [Given]
        public void GivenIProvideAllOfTheRequiredDataForTheRegistrationPage()
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
        
        [Given]
        public void GivenIForgetToProvideMyFirstName()
        {
            _command.FirstName = null;
        }

        [When]
        public void WhenISubmitTheRegistrationForm()
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
        
        [Then]
        public void ThenIWillBeToldINeedToProvideMyFirstName()
        {
            _badRequestResult.errors.ShouldContain(x => x.Key == "FirstName");
        }

        [Then]
        public void ThenIWillBeAddedToTheApplicationDatabase()
        {
            Context.User.FirstOrDefault(x => x.Email == _command.Email).ShouldNotBeNull();
        }

        [Then]
        public void ThenIWillBeLoggedIn()
        {
            //ScenarioContext.Current.Pending();
        }
    }
}
