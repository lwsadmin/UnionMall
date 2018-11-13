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
    [AutoMapFrom(typeof(Role))]
    public class RoleDropDownDto : EntityDto<long>
    {
        [Required]
        [StringLength(AbpRoleBase.MaxDisplayNameLength)]
        public string DisplayName { get; set; }
        public string NormalizedName { get; set; }
        public long BusinessId { get; set; }
    }
}
