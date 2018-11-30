using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TGift")]
    public class Gift:Entity<long>, IMustHaveTenant
    {
 
        public int TenantId { get; set; }
        public long BusinessId { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public decimal Integral { get; set; }
        public string Img { get; set; }
        public string Images { get; set; }
        public string Memo { get; set; }
        public string ExchangeNotes { get; set; }
        public string ExchangeAddress { get; set; }
        public int StockCount { get; set; }
        public int SingleReceiveCount { get; set; }
        //0 正常 1 下架
        public int Status { get; set; }
        public int Sort { get; set; }
        public string ExchangeType { get; set; }
    }
}
