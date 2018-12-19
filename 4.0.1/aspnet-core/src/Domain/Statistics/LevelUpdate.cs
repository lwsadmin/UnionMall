using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TMemberUpgrade")]
    public class LevelUpdate : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long BeforeId { get; set; }
        public long AfterId { get; set; }
        public long MemberId { get; set; }
        public string BillNumber { get; set; }
        public int Status { get; set; }
        public decimal AliPay { get; set; }
        public decimal CashPay { get; set; }
        public decimal WeChatPay { get; set; }
        public string Memo { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
