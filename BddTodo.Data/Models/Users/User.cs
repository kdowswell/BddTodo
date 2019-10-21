using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BddTodo.Data.Models.Users.ManyToMany;
using BddTodo.Data.Models.Users.Reference;

namespace BddTodo.Data.Models.Users
{
    public class User
    {
        public int Id { get; set; }

        public UserAccountStatusEnum UserAccountStatusId { get; set; }
        public UserAccountStatus UserAccountStatus { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }
    }
}
