using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Authorization.Roles;
using Abp.AutoMapper;
using UnionMall.Authorization.Roles;

namespace UnionMall.Roles.Dto
{
    [AutoMapTo(typeof(Role))]
    public class RoleDropDownDto: EntityDto<long>
    {
        [Required]
        [StringLength(AbpRoleBase.MaxDisplayNameLength)]
        public string DisplayName { get; set; }
    }
}
