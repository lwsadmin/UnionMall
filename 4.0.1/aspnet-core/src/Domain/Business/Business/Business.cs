
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
namespace UnionMall.Business.Business
{
    [Table("TBusiness")]
    public class Business : Entity<long>, IHasCreationTime, IMustHaveTenant
    {

        public int TenantId { get; set; }
        public string BusinessName { get; set; }
        public long AgentsId { get; set; }
        public int StoreCount { get; set; }
        public bool IsSystemBusiness { get; set; }
        public string Contact { get; set; }
        public string Tel { get; set; }
        public DateTime DueTime{ get; set; }
        public int Sort { get; set; }
        public string Memo { get; set; }
        public string Introduce { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
