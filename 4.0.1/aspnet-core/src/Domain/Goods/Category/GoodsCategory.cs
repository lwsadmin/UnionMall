using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;

namespace UnionMall.Goods.Category
{
    public class GoodsCategory : Entity<long>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        public string Title { get; set; }


        public long ParentId { get; set; }

        public int Sort { get; set; }


        public string Brand { get; set; }


        public string Memo { get; set; }

    }
}
