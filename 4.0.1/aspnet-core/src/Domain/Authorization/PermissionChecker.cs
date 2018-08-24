using Abp.Authorization;
using UnionMall.Authorization.Roles;
using UnionMall.Authorization.Users;

namespace UnionMall.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
