using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TFlashSaleOrderItem")]
    public class FlashSaleOrderItem : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long OrderId { get; set; }
        public long FlashSaleId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public string ItemCode { get; set; }
        public long UseChainStoreId { get; set; }
        public DateTime? UsedTime { get; set; }
        public string UserAccount { get; set; }
    }
}
