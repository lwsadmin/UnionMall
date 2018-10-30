using System.Collections.Generic;
using System.Linq;
using UnionMall.Roles.Dto;
using UnionMall.Users.Dto;

namespace UnionMall.Web.Models.Users
{
    public class EditUserModalViewModel
    {
        public UserDto User { get; set; }

        public IReadOnlyList<RoleDropDownDto> Roles { get; set; }

        public bool UserIsInRole(RoleDropDownDto role)
        {
            return User.RoleNames != null && User.RoleNames.Any(r => r == role.DisplayName);
        }
    }
}
