using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;

namespace UnionMall.Entity
{
    [Table("TParameter")]
    public class Parameter : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public string Title { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }
        public string Memo { get; set; }
    }
}
