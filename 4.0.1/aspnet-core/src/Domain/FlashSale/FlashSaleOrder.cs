using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TFlashSaleOrder")]
    public class FlashSaleOrder : Entity<long>, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long BusinessId { get; set; }
        public long ChainStoreId { get; set; }
        public long FlashSaleId { get; set; }
        public long MemberId { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalMoney { get; set; }
        public decimal TotalPay { get; set; }
        public decimal BalancePay { get; set; }
        public decimal WeChatPay { get; set; }
        public decimal IntegralPay { get; set; }
        public int Status { get; set; }
        public decimal AliPay { get; set; }
        public decimal CouponPay { get; set; }
        public string CouponCode { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
