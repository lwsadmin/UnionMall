using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TCommonAttribute")]
    public class CommonAttribute : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long CategoryId { get; set; }
        public string DataType { get; set; }
        public string Name { get; set; }
        public string ValueName { get; set; } = "";
        public string DefaultValue { get; set; } = "";
        public string Options { get; set; } = "";

        /// <summary>
        /// 0 商品(默认)  1
        /// </summary>
        public int Type { get; set; } = 0;
    }
}
