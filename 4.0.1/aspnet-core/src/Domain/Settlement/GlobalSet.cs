using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TGlobalSet")]
    public class GlobalSet : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public decimal FirstRecommender { get; set; } = 0;
        public decimal SecondRecommender { get; set; } = 0;
        public decimal ThirdRecommender { get; set; } = 0;


        public decimal FirstDistributor { get; set; } = 0;
        public decimal SecondDistributor { get; set; } = 0;
        public decimal ThirdDistributor { get; set; } = 0;


        public decimal FirstAgent { get; set; } = 0;
        public decimal SecondAgent { get; set; } = 0;
        public decimal ThirdAgent { get; set; } = 0;
        public GlobalSet(int tenantId)
        {
            this.TenantId = tenantId;
        }
    }
}
