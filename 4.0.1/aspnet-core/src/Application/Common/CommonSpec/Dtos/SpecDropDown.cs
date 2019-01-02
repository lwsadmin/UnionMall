using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnionMall.Common.Dtos
{
    [AutoMapFrom(typeof(Entity.CommonSpec))]
    public class SpecDropDown : EntityDto<long>
    {
        public string Name { get; set; }

    }
}
