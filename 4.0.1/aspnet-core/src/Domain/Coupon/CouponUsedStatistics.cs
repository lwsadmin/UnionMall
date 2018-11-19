using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;

namespace UnionMall.Entity
{
    [Table("TCouponUsedStatistics")]
    public class CouponUsedStatistics : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long MemberId { get; set; }
        public long ChainStoreId { get; set; }
        public long CouponId { get; set; }
        public long CouponSendId { get; set; }
        public decimal BillValue { get; set; }
        public string BillNumber { get; set; }
        public decimal CouponValue { get; set; }
        public int UseCount { get; set; }
        public string Account { get; set; }
        public DateTime UseTime { get; set; } = DateTime.Now;
    }
}
