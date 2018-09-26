using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Domain.Entities;

namespace UnionMall.Goods.Category
{
    [System.ComponentModel.DataAnnotations.Schema.Table("TGoodsCategory")]
    public class GoodsCategory : Entity<long>, IMustHaveTenant
    {    
        public int TenantId { get; set; }

        [Required(ErrorMessage = "请输入类别名称!")]
        public string Title { get; set; }
        public long ParentId { get; set; }
        public int Sort { get; set; }
        public string Brand { get; set; }
        public string Note { get; set; }

    }
}
