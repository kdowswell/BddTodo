using BddTodo.Data.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BddTodo.Data.Models.Todos
{
    public class Todo
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string Title { get; set; }

        public int TodoListId { get; set; }
        public TodoList TodoList { get; set; }
    }
}
