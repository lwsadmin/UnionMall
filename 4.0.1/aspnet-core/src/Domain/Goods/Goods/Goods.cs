using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnionMall.Entity
{
    [Table("TGoods")]
    public class Goods : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }
        public long CategoryId { get; set; }
        public long BrandId { get; set; } = 0;
        public long ChainStoreId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public string RecommendType { get; set; } = "";
        public string Image { get; set; }
        public int Click { get; set; }
        public int Stock { get; set; }
        public int SellCount { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal Price { get; set; }
        public int Sort { get; set; }
        public int Commission { get; set; } = 0;
        public string Remark { get; set; } = "";
        public string Detail { get; set; }
        public string Parameter1 { get; set; } = "";
        public string Parameter2 { get; set; } = "";
        public string Parameter3 { get; set; } = "";
        public string Parameter4 { get; set; } = "";
        public string Parameter5 { get; set; } = "";
        public string Parameter6 { get; set; } = "";
        public string Parameter7 { get; set; } = "";
        public string Parameter8 { get; set; } = "";
        public string Parameter9 { get; set; } = "";
        public string Parameter10 { get; set; } = "";
    }
    public enum GoodsType
    {
        //materialObject fictitiousGoodss
        GoodsSW = 0,
        GoodsXN = 1
    }
    public enum GoodsState
    {
        Shelve = 0,
        UnShelve = 1,
        Aduit = 2,
        StockOut = 3
    }
}
