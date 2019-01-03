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
        public long CategoryId { get; set; }
        public string Name { get; set; }
        /// 0 商品(默认)  1
        /// </summary>
        public int Type { get; set; } = 0;
        public string Memo { get; set; } = "";
    }
}
