using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BddTodo.Data.Models.Todos;
using BddTodo.Data.Models.Users;
using BddTodo.Data.Models.Users.ManyToMany;
using BddTodo.Data.Models.Users.Reference;
using Microsoft.EntityFrameworkCore;


namespace BddTodo.Data
{
    public class BddTodoDbContext : DbContext
    {
        public BddTodoDbContext()
        {

        }

        public BddTodoDbContext(DbContextOptions<BddTodoDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<UserRoles>().HasKey(table => new
            {
                table.UserId,
                table.UserRoleId
            });

            AddReferenceTableData(ref modelBuilder);

        }

        private void AddReferenceTableData(ref ModelBuilder modelBuilder)
        {
            var values = Enum.GetValues(typeof(UserRoleEnum));
            foreach (UserRoleEnum val in values)
            {
                var name = Enum.GetName(typeof(UserRoleEnum), val);
                modelBuilder.Entity<UserRole>().HasData(new UserRole { Id = val, Title = name });
            }

            values = Enum.GetValues(typeof(UserAccountStatusEnum));
            foreach (UserAccountStatusEnum val in values)
            {
                var name = Enum.GetName(typeof(UserAccountStatusEnum), val);
                modelBuilder.Entity<UserAccountStatus>().HasData(new UserAccountStatus { Id = val, Title = name });
            }
        }

        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }


        public DbSet<TodoList> TodoList { get; set; }
        public DbSet<Todo> Todo { get; set; }
    }
}
