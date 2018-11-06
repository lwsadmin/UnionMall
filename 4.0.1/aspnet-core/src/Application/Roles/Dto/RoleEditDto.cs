using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Roles;
using UnionMall.Authorization.Roles;

namespace UnionMall.Roles.Dto
{
    public class RoleEditDto: EntityDto<long>
    {
        [Required]
        [StringLength(AbpRoleBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpRoleBase.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        [StringLength(Role.MaxDescriptionLength)]
        public string Description { get; set; }

        public long BusinessId { get; set; }
        public bool IsStatic { get; set; }
        public string ManageRole { get; set; } = "";
    }
}