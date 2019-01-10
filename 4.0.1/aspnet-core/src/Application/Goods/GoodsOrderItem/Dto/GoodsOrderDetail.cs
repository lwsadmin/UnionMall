using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnionMall.Goods.Dto
{
    public class GoodsOrderDetail : EntityDto<long>
    {
        public string GoodsName { get; set; }
        public string Count { get; set; }
        public string Price { get; set; }
        public string GoodsSpec { get; set; }
    }
}
