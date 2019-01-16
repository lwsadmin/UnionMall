using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;


namespace UnionMall.Entity
{
    [Table("TCouponSendStatistics")]
    public class CouponSendStatistics : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long MemberId { get; set; }
        public long CouponId { get; set; }
        public string CouponCode { get; set; } = "";
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime SendTime { get; set; } = DateTime.Now;
        public string SendOrderBillNumber { get; set; } = "";
        public int ReceiveCount { get; set; } = 1;
        public int UsedCount { get; set; } = 0;
        public string BillNumber { get; set; } = "";
        public int Status { get; set; } = 0;
        public decimal PayAmount { get; set; } = 0;
    }
}
