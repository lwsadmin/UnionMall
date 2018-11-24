
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Dependency;
using Abp.Runtime.Session;
namespace UnionMall.Entity
{
    //TArticle
    [Table("TArticle")]
    public class Article : Entity<long>, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long CategoryId { get; set; }
        public string Title { get; set; }
        public string Souce { get; set; }
        public string author { get; set; }
        public int Sort { get; set; } = 0;
        public long UserId { get; set; }
        public bool IsManager { get; set; }
        public string SmallImg { get; set; }
        public int Status { get; set; }=0;
        public string Target { get; set; }
        public string Level { get; set; }
        public string Describle { get; set; }
        public string Content { get; set; }
        public int Click { get; set; }
        public DateTime CreationTime { get; set; }
    }
}