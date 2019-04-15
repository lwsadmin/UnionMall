using System.Collections.Generic;
using UnionMall.Roles.Dto;
using UnionMall.Users.Dto;

namespace UnionMall.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
