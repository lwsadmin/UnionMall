
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
    [Table("TBusiness")]
    public class Business : Entity<long>, IHasCreationTime, IMustHaveTenant
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "TenantId")]
        public int TenantId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "BusinessName")]
        public string BusinessName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "AgentsId")]
        public long AgentsId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "StoreCount")]
        public int StoreCount { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "IsSystemBusiness")]
        public bool IsSystemBusiness { get; set; } = false;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Contact")]
        public string Contact { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Tel")]
        public string Tel { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "DueTime")]
        public DateTime DueTime { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Sort")]
        public int Sort { get; set; }

        [MaxLength(200,ErrorMessage="maxLength=200")]
        public string Memo { get; set; }
        public string Introduce { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "CreationTime")]
        public DateTime CreationTime { get; set; } = DateTime.Now;

    }
}
