﻿using System;
using System.Linq;
using Bogus;
using NetTopologySuite.Geometries;

namespace BddTodo.Tests._Infrastructure.Builders
{
    public class Build
    {
		public static TodoBuilder Todo => new TodoBuilder();
		public static TodoListBuilder TodoList => new TodoListBuilder();
		public static UserRolesBuilder UserRoles => new UserRolesBuilder();
		public static UserAccountStatusBuilder UserAccountStatus => new UserAccountStatusBuilder();
		public static UserRoleBuilder UserRole => new UserRoleBuilder();
		public static UserBuilder User => new UserBuilder();
    }

	public abstract class Builder<TDto>
    {
        protected TDto dto;

        public Builder()
        {
            dto = Activator.CreateInstance<TDto>();
            AutoGeneratedDefaults();
			Defaults();
        }

        protected virtual void AutoGeneratedDefaults() {
        }

		protected virtual void Defaults() {
        }
        
        public TDto Build()
        {
            return dto;
        }
    }
	
	public partial class TodoBuilder : Builder<BddTodo.Data.Models.Todos.Todo>
	{

        protected override void AutoGeneratedDefaults() {
            var fake = new Faker<BddTodo.Data.Models.Todos.Todo>()
                            .RuleFor(o => o.Title, f => f.Lorem.Text())
                            .RuleFor(o => o.TodoListId, f => f.Random.Int())
            ;

            dto = fake;
        }

    	public TodoBuilder WithId(System.Int32 id)
		{
			dto.Id = id;
			return this;
		}

		public TodoBuilder WithUserId(System.Int32 userid)
		{
			dto.UserId = userid;
			return this;
		}

		public TodoBuilder WithUser(BddTodo.Data.Models.Users.User user)
		{
			dto.User = user;
			return this;
		}

	
		public TodoBuilder WithUser(Action<UserBuilder> userBuilder)
		{
			var b = new UserBuilder();
            userBuilder.Invoke(b);
            dto.User = b.Build();
            return this;
		}
		public TodoBuilder WithTitle(System.String title)
		{
			dto.Title = title;
			return this;
		}

		public TodoBuilder WithTodoListId(System.Int32 todolistid)
		{
			dto.TodoListId = todolistid;
			return this;
		}

		public TodoBuilder WithTodoList(BddTodo.Data.Models.Todos.TodoList todolist)
		{
			dto.TodoList = todolist;
			return this;
		}

	
		public TodoBuilder WithTodoList(Action<TodoListBuilder> todolistBuilder)
		{
			var b = new TodoListBuilder();
            todolistBuilder.Invoke(b);
            dto.TodoList = b.Build();
            return this;
		}
	}

	public partial class TodoListBuilder : Builder<BddTodo.Data.Models.Todos.TodoList>
	{

        protected override void AutoGeneratedDefaults() {
            var fake = new Faker<BddTodo.Data.Models.Todos.TodoList>()
                            .RuleFor(o => o.Title, f => f.Lorem.Text())
            ;

            dto = fake;
        }

    	public TodoListBuilder WithId(System.Int32 id)
		{
			dto.Id = id;
			return this;
		}

		public TodoListBuilder WithUserId(System.Int32 userid)
		{
			dto.UserId = userid;
			return this;
		}

		public TodoListBuilder WithUser(BddTodo.Data.Models.Users.User user)
		{
			dto.User = user;
			return this;
		}

	
		public TodoListBuilder WithUser(Action<UserBuilder> userBuilder)
		{
			var b = new UserBuilder();
            userBuilder.Invoke(b);
            dto.User = b.Build();
            return this;
		}
		public TodoListBuilder WithTitle(System.String title)
		{
			dto.Title = title;
			return this;
		}

		public TodoListBuilder WithTodo(System.Collections.Generic.List<BddTodo.Data.Models.Todos.Todo> todo)
		{
			dto.Todo = todo;
			return this;
		}

		
		public TodoListBuilder WithTodo(params BddTodo.Data.Models.Todos.Todo[] todo)
		{
			dto.Todo = todo.ToList();
			return this;
		}
		
		public TodoListBuilder WithTodo(params Action<TodoBuilder>[] builders)
		{
			var todo = new System.Collections.Generic.List<BddTodo.Data.Models.Todos.Todo>();

			foreach(var builder in builders)
            {
                var b = new TodoBuilder();
                builder.Invoke(b);
                todo.Add(b.Build());
            }

            dto.Todo  = todo;

			return this;
		}
	}

	public partial class UserRolesBuilder : Builder<BddTodo.Data.Models.Users.ManyToMany.UserRoles>
	{

        protected override void AutoGeneratedDefaults() {
            var fake = new Faker<BddTodo.Data.Models.Users.ManyToMany.UserRoles>()
                            .RuleFor(o => o.UserRoleId, f => f.PickRandom<BddTodo.Data.Models.Users.Reference.UserRoleEnum>())
            ;

            dto = fake;
        }

    	public UserRolesBuilder WithUserId(System.Int32 userid)
		{
			dto.UserId = userid;
			return this;
		}

		public UserRolesBuilder WithUser(BddTodo.Data.Models.Users.User user)
		{
			dto.User = user;
			return this;
		}

	
		public UserRolesBuilder WithUser(Action<UserBuilder> userBuilder)
		{
			var b = new UserBuilder();
            userBuilder.Invoke(b);
            dto.User = b.Build();
            return this;
		}
		public UserRolesBuilder WithUserRoleId(BddTodo.Data.Models.Users.Reference.UserRoleEnum userroleid)
		{
			dto.UserRoleId = userroleid;
			return this;
		}

		public UserRolesBuilder WithUserRole(BddTodo.Data.Models.Users.Reference.UserRole userrole)
		{
			dto.UserRole = userrole;
			return this;
		}

	
		public UserRolesBuilder WithUserRole(Action<UserRoleBuilder> userroleBuilder)
		{
			var b = new UserRoleBuilder();
            userroleBuilder.Invoke(b);
            dto.UserRole = b.Build();
            return this;
		}
	}

	public partial class UserAccountStatusBuilder : Builder<BddTodo.Data.Models.Users.Reference.UserAccountStatus>
	{

        protected override void AutoGeneratedDefaults() {
            var fake = new Faker<BddTodo.Data.Models.Users.Reference.UserAccountStatus>()
                            .RuleFor(o => o.Title, f => f.Lorem.Text())
            ;

            dto = fake;
        }

    	public UserAccountStatusBuilder WithId(BddTodo.Data.Models.Users.Reference.UserAccountStatusEnum id)
		{
			dto.Id = id;
			return this;
		}

		public UserAccountStatusBuilder WithTitle(System.String title)
		{
			dto.Title = title;
			return this;
		}

	}

	public partial class UserRoleBuilder : Builder<BddTodo.Data.Models.Users.Reference.UserRole>
	{

        protected override void AutoGeneratedDefaults() {
            var fake = new Faker<BddTodo.Data.Models.Users.Reference.UserRole>()
                            .RuleFor(o => o.Title, f => f.Lorem.Text())
            ;

            dto = fake;
        }

    	public UserRoleBuilder WithId(BddTodo.Data.Models.Users.Reference.UserRoleEnum id)
		{
			dto.Id = id;
			return this;
		}

		public UserRoleBuilder WithTitle(System.String title)
		{
			dto.Title = title;
			return this;
		}

	}

	public partial class UserBuilder : Builder<BddTodo.Data.Models.Users.User>
	{

        protected override void AutoGeneratedDefaults() {
            var fake = new Faker<BddTodo.Data.Models.Users.User>()
                            .RuleFor(o => o.UserAccountStatusId, f => f.PickRandom<BddTodo.Data.Models.Users.Reference.UserAccountStatusEnum>())
                            .RuleFor(o => o.FirstName, f => f.Lorem.Text())
                            .RuleFor(o => o.LastName, f => f.Lorem.Text())
                            .RuleFor(o => o.Email, f => f.Lorem.Text())
                            .RuleFor(o => o.PasswordHash, f => f.Lorem.Text())
                            .RuleFor(o => o.PasswordSalt, f => f.Lorem.Text())
                            .RuleFor(o => o.CreatedDate, f => f.Date.Recent())
            ;

            dto = fake;
        }

    	public UserBuilder WithId(System.Int32 id)
		{
			dto.Id = id;
			return this;
		}

		public UserBuilder WithUserAccountStatusId(BddTodo.Data.Models.Users.Reference.UserAccountStatusEnum useraccountstatusid)
		{
			dto.UserAccountStatusId = useraccountstatusid;
			return this;
		}

		public UserBuilder WithUserAccountStatus(BddTodo.Data.Models.Users.Reference.UserAccountStatus useraccountstatus)
		{
			dto.UserAccountStatus = useraccountstatus;
			return this;
		}

	
		public UserBuilder WithUserAccountStatus(Action<UserAccountStatusBuilder> useraccountstatusBuilder)
		{
			var b = new UserAccountStatusBuilder();
            useraccountstatusBuilder.Invoke(b);
            dto.UserAccountStatus = b.Build();
            return this;
		}
		public UserBuilder WithFirstName(System.String firstname)
		{
			dto.FirstName = firstname;
			return this;
		}

		public UserBuilder WithLastName(System.String lastname)
		{
			dto.LastName = lastname;
			return this;
		}

		public UserBuilder WithEmail(System.String email)
		{
			dto.Email = email;
			return this;
		}

		public UserBuilder WithPasswordHash(System.String passwordhash)
		{
			dto.PasswordHash = passwordhash;
			return this;
		}

		public UserBuilder WithPasswordSalt(System.String passwordsalt)
		{
			dto.PasswordSalt = passwordsalt;
			return this;
		}

		public UserBuilder WithCreatedDate(System.DateTime createddate)
		{
			dto.CreatedDate = createddate;
			return this;
		}

		public UserBuilder WithUserRoles(System.Collections.Generic.ICollection<BddTodo.Data.Models.Users.ManyToMany.UserRoles> userroles)
		{
			dto.UserRoles = userroles;
			return this;
		}

	}
}
