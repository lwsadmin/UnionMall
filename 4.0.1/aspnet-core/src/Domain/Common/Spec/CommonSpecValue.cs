using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TCommonSpecValue")]
    public class CommonSpecValue : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long SpecId { get; set; }
        public string Text { get; set; }
    }
}
