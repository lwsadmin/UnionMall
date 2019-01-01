using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TCommonSpec")]
    public class CommonSpec : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Memo { get; set; } = "";
    }
}
