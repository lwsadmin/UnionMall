using System.Collections.Generic;
using UnionMall.Roles.Dto;

namespace UnionMall.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}