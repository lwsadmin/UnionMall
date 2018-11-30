using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnionMall.Entity
{
    public class Image : Entity<long>, IMustHaveTenant
    {
        public string Name { get; set; }
        public int TenantId { get; set; }
        public long BusinessId { get; set; }
        public long ChainStoreId { get; set; }
        public int Size { get; set; }
        public int Type { get; set; }
        public string Url { get; set; }
        public int Sort { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
