﻿using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Roles;
using UnionMall.Authorization.Users;

namespace UnionMall.Authorization.Roles
{
    public class Role : AbpRole<User>
    {
        public const int MaxDescriptionLength = 5000;

        public Role()
        {
        }

        public Role(int? tenantId, string displayName)
            : base(tenantId, displayName)
        {
            if (string.IsNullOrEmpty(displayName))
            {
                displayName=this.Name;
            }
        }

        public Role(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {

        }
        [StringLength(MaxDescriptionLength)]
        public string Description {get; set;}
        public long BusinessId { get; set; }
        public string ManageRole { get; set; }
    }
}
