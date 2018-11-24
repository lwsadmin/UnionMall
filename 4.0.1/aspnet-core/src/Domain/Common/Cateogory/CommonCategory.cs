using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace UnionMall.Entity
{
    [Table("TCommonCategory")]
    public class CommonCategory : Entity<long>, IMustHaveTenant
    {
        public string Title { get; set; }
        public int TenantId { get; set; }
        public long ParentId { get; set; } = 0;
        /// <summary>
        /// 分类 类型:1 资讯  2  礼品  3..... 
        /// </summary>
        public int Type { get; set; }
        public long BusinessId { get; set; }
        public int Sort { get; set; }
        public string Memo { get; set; }
    }
}
