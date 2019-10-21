using Microsoft.AspNetCore.Http;
using BddTodo.Data.Models.Users.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace BddTodo.Controllers.Users._Infrastructure
{
    public class CurrentUser : ICurrentUser
    {
        private ClaimsPrincipal Identity;

        public CurrentUser(IHttpContextAccessor user)
        {
            Identity = user.HttpContext.User;
        }

        public int Id
        {
            get
            {
                var id = Identity?.FindFirst(ClaimTypes.Name)?.Value;
                return string.IsNullOrEmpty(id) ? 0 : int.Parse(id);
            }
        }

        public string LastName
        {
            get
            {
                var lastNameClaim = Identity.Claims.FirstOrDefault(x => x.Type == "lastName");
                return lastNameClaim != null ? lastNameClaim.Value : "";
            }
        }

        public string Email => Identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

        public string FirstName
        {
            get
            {
                var firstNameClaim = Identity.Claims.FirstOrDefault(x => x.Type == "firstName");
                return firstNameClaim != null ? firstNameClaim.Value : "";
            }
        }

        public string FullName => FirstName + ' ' + LastName;

        //public List<UserRoleEnum> Roles
        //{
        //    get
        //    {
        //        var roleIds = Identity.Claims
        //            .Where(x => x.Type == ClaimTypes.Role)
        //            .Select(x => Convert.ToInt32(x.Value));

        //        return roleIds.Select(roleId => (UserRoleEnum)roleId).ToList();
        //    }
        //}

    }
}