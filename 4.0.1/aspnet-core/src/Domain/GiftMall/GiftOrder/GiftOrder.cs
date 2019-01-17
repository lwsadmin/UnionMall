using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TGiftOrder")]
    public class GiftOrder : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long ChainStoreId { get; set; }
        public string BillNumber { get; set; }
        public long MemberId { get; set; }
        public decimal Point { get; set; }
        public string Code { get; set; }
       // public string Images { get; set; }
        public int Status { get; set; }
        public string Memo { get; set; }
        public string ExchangeAddress { get; set; } = "";
        public DateTime ReceiveTime { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
