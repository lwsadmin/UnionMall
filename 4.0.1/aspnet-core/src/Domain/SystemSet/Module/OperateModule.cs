using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TOperateModule")]
    public class OperateModule : Entity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        public string Title { get; set; }
        public string KeyName { get; set; }
        public bool Visabled { get; set; }
        public string LinkUrl { get; set; }
        public string Icon { get; set; }
        public int Sort { get; set; }
        public int Version { get; set; }
        public int Type { get; set; }
        public string Memo { get; set; }
    }
}
