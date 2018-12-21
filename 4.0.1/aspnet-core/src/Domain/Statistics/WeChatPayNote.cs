using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TWeChatPayNote")]
    public class WeChatPayNote : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long ChainStoreId { get; set; }
        public string BillNumber { get; set; }
        public string BillType { get; set; }
        public decimal Amount { get; set; }
        public int Mch_Type { get; set; }
        public string Mch_Id { get; set; }
        public string Sub_Mch_Id { get; set; }
        public int Status { get; set; }
        public string Memo { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
