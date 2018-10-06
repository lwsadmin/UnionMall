using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace UnionMall.Business.Buiness.Dto
{
    [AutoMapFrom(typeof(Business))]
    public class BusinessDropDownDto : EntityDto<long>
    {
        public string BusinessName { get; set; }
    }
}
