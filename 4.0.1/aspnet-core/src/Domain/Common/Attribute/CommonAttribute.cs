using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TCommonAttribute")]
    public class CommonAttribute:Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public string DataType { get; set; }
        public string Name { get; set; }
        public string ValueName { get; set; }
        public string DefultValue { get; set; }
        public string Options { get; set; }
        public int Type { get; set; }
    }
}
