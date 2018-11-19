using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace UnionMall.Entity
{

    [Table("TLog")]
    public class Log : Entity<long>, IHasCreationTime, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public string Content { get; set; }
        public string IPAddress { get; set; }
        public string UserAccount { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}   
