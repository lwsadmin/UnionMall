
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Runtime.Session;
using Abp.Timing;
using model = UnionMall.Goods.Category;
namespace UnionMall.Goods.GoodsCategory.Dto
{
    [AutoMap(typeof(model.GoodsCategory))]
    public class CategoryEditDto : EntityDto<long>
    {
        public int? TenantId { get; set; }
        public string Title { get; set; }
        public long ParentId { get; set; }
        public int Sort { get; set; }
        public string Brand { get; set; }
        public string Note { get; set; }
    }
}
