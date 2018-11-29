using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace UnionMall.Entity
{
    [Table("TConsumeNote")]
    public class ConsumeNote : Entity<long>, IHasCreationTime, IMustHaveTenant
    {
        public long MemberId { get; set; }
        public long ChainStoreId { get; set; }
        public int TenantId { get; set; }
        public ConsumeType Type { get; set; }
        public string BillNumber { get; set; }
        public decimal Point { get; set; }
        public decimal ShouldAmount { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal MoneyPaid { get; set; }
        public decimal ValuePaid { get; set; }
        public decimal CardPaid { get; set; }
        public decimal PointPaid { get; set; }
        public decimal CouponPaid { get; set; }
        public string CouponCode { get; set; }
        public decimal OtherPaid { get; set; }
        public decimal CutAmount { get; set; }
        public string UserAccount { get; set; }
        public DateTime OperateTime { get; set; }
        public bool IsUndo { get; set; }
        public string Memo { get; set; }
        public decimal ThirdPaid { get; set; }
        public decimal AliPay { get; set; }
        public decimal AliPaid { get; set; }
        public int Status { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }

    public enum ConsumeType
    {
        GoodsOrder = 0,
        拼团下单 = 1,
        FlashSale = 2,
        GiftExchange = 3,
        QuickConsume = 4,
        GoodsConsume = 5,
        MemberRecharge = 6,
        PointRecharge = 7
    }
}
