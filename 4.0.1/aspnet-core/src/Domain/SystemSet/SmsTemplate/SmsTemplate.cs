using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TSmsTemplate")]
    public class SmsTemplate : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public string Title { get; set; }
        public string Template { get; set; }
        public string KeyName { get; set; }
        public bool IsEnable { get; set; }
    }
}
