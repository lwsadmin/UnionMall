using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using UnionMall.Entity;
namespace UnionMall.Coupon.Dto
{
    [AutoMapFrom(typeof(Entity.Coupon))]
    public class CreateEditDto : EntityDto<long>
    {
        public int TenantId { get; set; }
        public long BusinessId { get; set; }
        public string Title { get; set; }
        public decimal Value { get; set; }
        public decimal UseValueLimit { get; set; }
        public int Type { get; set; }
        public int TotalCount { get; set; }
        public int SigleReceiveCount { get; set; }
        public int Status { get; set; }
        public int ReceiveType { get; set; }
        public string GoodsCategoryId { get; set; }
        public int ValidityDay { get; set; }
        public string Image { get; set; }
        public string Introduce { get; set; }

    }
}
