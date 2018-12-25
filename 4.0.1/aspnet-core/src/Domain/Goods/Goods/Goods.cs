using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TGoods")]
    public class Goods: Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long CategoryId { get; set; }
        public long BrandId { get; set; }
        public long ChainStoreId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public string RecommendType { get; set; }
        public string Image { get; set; }
        public int Click { get; set; }
        public int Stock { get; set; }
        public int SellCount { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal Price { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
        public string Detail { get; set; }
    }
}
