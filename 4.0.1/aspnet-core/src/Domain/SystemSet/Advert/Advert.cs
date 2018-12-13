using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{

    [Table("TAdvert")]
    public class Advert : Entity<long>, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string LinkUrl { get; set; }
        public int Sort { get; set; }
        public string Memo { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
