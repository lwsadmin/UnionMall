using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace UnionMall.Entity
{
    [Table("TBalanceNote")]
    public class BalanceNote : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long ChainStoreId { get; set; }
        public long MemberId { get; set; }
        public string BillNumber { get; set; }
        public int Type { get; set; }
        public int Way { get; set; }
        public decimal Value { get; set; }
        public decimal Balance { get; set; }
        public string UserAccount { get; set; }
        public bool IsUndo { get; set; }
        public string Memo { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }

    enum BalanceWay
    {

    }
}
