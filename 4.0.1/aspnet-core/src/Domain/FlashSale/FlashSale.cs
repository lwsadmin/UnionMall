using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TFlashSale")]
    public class FlashSale:Entity<long>, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long BusinessId { get; set; }
        public long ChainStoreId { get; set; }
        public string UserAccount { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal MarketPrice { get; set; }
        public int SellCount { get; set; }
        public int TotalCount { get; set; }
        public string Img { get; set; }
        public int Status { get; set; }
        public string Introduce { get; set; }
        public int Sort { get; set; }
        public DateTime CreationTime { get; set; }
        public int SingleLimit { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
