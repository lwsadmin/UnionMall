using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TImage")]
    public class Image : Entity<long>, IMustHaveTenant
    {
        public string Name { get; set; }
        public int TenantId { get; set; }
        public long BusinessId { get; set; }
        public long ChainStoreId { get; set; }
        public long ObjectId { get; set; }
        public decimal Size { get; set; }
        public int Type { get; set; }
        public string Url { get; set; }
        public int Sort { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;

    }
    public enum ImageType
    {
        商品图片 = 0,
        礼品图片 = 1,
        多人拼团 = 2,
        限时抢购 = 3
    }
}
