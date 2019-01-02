using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnionMall.Common.Dto
{
    [AutoMapFrom(typeof(Entity.CommonSpecValue))]
    public class ValueItemDto: EntityDto<long>
    {
        public string Name { get; set; }
        public long SpecId { get; set; }
    }
}
