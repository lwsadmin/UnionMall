using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TMemberRechargeNote")]
    public class RechargeNote : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long BusinessId { get; set; }
        public long ChainStoreId { get; set; }
        public long MemberId { get; set; }
        public string BillNumber { get; set; }

        public decimal TotalPay { get; set; }
        public decimal CashPay { get; set; }
        public decimal AliPay { get; set; }
        public decimal WeChatPay { get; set; }

        public int Status { get; set; }
        public decimal Value { get; set; }
        public decimal Balance { get; set; }
        public string UserAccount { get; set; }
        public string Note { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
