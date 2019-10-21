using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BddTodo.Data.Models.Users.Reference;

namespace BddTodo.Data.Models.Users.ManyToMany
{
    public class UserRoles
    {
        [Column(Order = 0)]
        public int UserId { get; set; }
        public User User { get; set; }

        [Column(Order = 1)]
        public UserRoleEnum UserRoleId { get; set; }
        public UserRole UserRole { get; set; }
    }
}
