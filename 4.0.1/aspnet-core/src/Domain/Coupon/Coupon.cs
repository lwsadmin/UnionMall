using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace UnionMall.Entity
{

    [Table("TCoupon")]
    public class Coupon : Entity<long>, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long BusinessId { get; set; }
        public string Title { get; set; }
        public decimal Value { get; set; }
        public decimal UseValueLimit { get; set; }
        public int Type { get; set; }
        public int TotalCount { get; set; }
        public int SigleReceiveCount { get; set; }
        public int Status { get; set; }
        public int ReceiveType { get; set; }
        public string GoodsCategoryId { get; set; }
        public int ValidityDay { get; set; }

        public int MemberReceiveCount { get; set; }
        public int MemberUseCount { get; set; }
        public string Image { get; set; }
        public string Introduce { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
