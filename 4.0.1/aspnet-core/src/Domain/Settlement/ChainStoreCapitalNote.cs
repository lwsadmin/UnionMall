using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnionMall.Entity
{
    public class ChainStoreCapitalNote : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long ChainStoreId { get; set; }
        public string BillNumber { get; set; }
        public int Type { get; set; }
        public FinanceType Way { get; set; }
        public decimal Value { get; set; }
        public decimal Balance { get; set; }
        public string UserAccount { get; set; }
        public DateTime CreationTime { get; set; }
        public string Memo { get; set; }
        public int Status { get; set; }
        public DateTime ApplyTime { get; set; }

        public enum FinanceType
        {
            Consume = 0,
            SpellGroupOrder = 1,
            FlashSale = 2,
            GiftExchange = 3,
            QuickConsume = 4,
            GoodsConsume = 5,
            MemberRecharge = 6,
            PointRecharge = 7
        }

    }
}
