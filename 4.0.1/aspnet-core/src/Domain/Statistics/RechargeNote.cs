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
        public string BillNumber { get; set; } = DateTime.Now.ToString();

        public decimal TotalPay { get; set; } = 0;
        public decimal CashPay { get; set; } = 0;
        public decimal AliPay { get; set; } = 0;
        public decimal WeChatPay { get; set; } = 0;

        public int Status { get; set; } = 0;
        public decimal Value { get; set; } = 0;
        public decimal Balance { get; set; } = 0;
        public string UserAccount { get; set; } = "";
        public string Note { get; set; } = "";
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
