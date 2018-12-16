using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TWechatMessage")]
    public class WechatMessage : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Key { get; set; }
        public bool IsOpen { get; set; }
    }
}
