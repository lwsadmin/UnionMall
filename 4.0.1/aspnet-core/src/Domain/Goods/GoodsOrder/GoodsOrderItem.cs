using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TGoodsOrderItem")]
    public class GoodsOrderItem : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long GoodsOrderId { get; set; }
        public long BusinessId { get; set; }
        public long ChainStoreId { get; set; }
        public long GoodsId { get; set; }
        public long SpecObjectId { get; set; } = 0;
        public decimal Price { get; set; }
        public int Count { get; set; } = 1;
        public decimal Total { get; set; }
        public decimal Commission { get; set; } = 0;
    }
}
