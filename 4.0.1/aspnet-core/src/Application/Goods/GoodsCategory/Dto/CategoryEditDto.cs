
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Runtime.Session;
using Abp.Timing;
using UnionMall.Entity;
namespace UnionMall.Goods.Dto
{
    [AutoMap(typeof(Entity.GoodsCategory))]
    public class CategoryEditDto : EntityDto<long>
    {
        public int? TenantId { get; set; }
        public string Title { get; set; }
        public long ParentId { get; set; } = 0;
        public int Sort { get; set; } = 0;
        public string Brand { get; set; } = "";
        public string Note { get; set; } = "";
    }
}
