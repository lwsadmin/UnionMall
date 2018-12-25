using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using UnionMall.Entity;
namespace UnionMall.Goods.Dto
{
    [AutoMapFrom(typeof(Brand))]
    public class BrandSelectDto : EntityDto<long>
    {
        public string Title { get; set; }
    }
}
