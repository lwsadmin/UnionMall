using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnionMall.GiftMall.Dto
{
    public class GiftOrderDetail : EntityDto<long>
    {
        public string GiftName { get; set; }
        public int Count { get; set; }
        public decimal Point { get; set; }
    }
}
