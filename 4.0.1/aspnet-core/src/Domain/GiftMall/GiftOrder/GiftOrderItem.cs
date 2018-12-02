using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TGiftOrderItem")]
    public class GiftOrderItem : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long GiftOrderId { get; set; }
        public long GiftId { get; set; }
        public int Count { get; set; }
        public decimal Point { get; set; }
        public string ExchangeAddress { get; set; }
    }
}
