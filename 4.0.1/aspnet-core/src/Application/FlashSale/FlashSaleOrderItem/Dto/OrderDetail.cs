using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnionMall.FlashSale.Dto
{
    public class OrderDetail : EntityDto<long>
    {
        public string GoodsName { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
