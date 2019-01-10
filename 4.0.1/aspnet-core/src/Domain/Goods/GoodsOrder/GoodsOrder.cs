using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TGoodsOrder")]
    public class GoodsOrder : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long BusinessId { get; set; }
        public long ChainStoreId { get; set; }
        public long MemberId { get; set; }
        public string BillNumber { get; set; }
        public decimal TotalMoney { get; set; }
        public decimal TotalPay { get; set; }
        public decimal Integral { get; set; }
        public decimal MoneyPay { get; set; }
        public decimal BalancePay { get; set; }
        public decimal IntegralPay { get; set; }
        public int DistributionWay { get; set; } = 0;
        public bool IsUsed { get; set; }
        public string FatherBillNumber { get; set; }
        public DateTime SubmitTime { get; set; } = DateTime.Now;
        public string UserAccount { get; set; }
        public DateTime OperaterTime { get; set; }
        public string Memo { get; set; }
        public decimal WechatPay { get; set; }
        public string Mobile { get; set; }
        public decimal CouponPay { get; set; }
        public string OrderCode { get; set; }
        public int Status { get; set; } = 0;
        public string RefundNO { get; set; }
        public long MemberCouponId { get; set; } = 0;
        public int RefundWay { get; set; } = 0;
        public decimal AliPay { get; set; } = 0;
        public int Type { get; set; } = 0;
    }
}
