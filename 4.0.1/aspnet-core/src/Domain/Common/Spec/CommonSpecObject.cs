using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TCommonSpecObject")]
    public class CommonSpecObject : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long ObjectId { get; set; }
        public int Type { get; set; }
        public string Image { get; set; }
        public string ValueText { get; set; } = "";
        public int Stock { get; set; }
        public int SellCount { get; set; } = 0;
        public decimal RetailPrice { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public string SKU { get; set; }

    }
}
