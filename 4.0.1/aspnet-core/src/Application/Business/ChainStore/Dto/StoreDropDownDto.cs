using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace UnionMall.Business.Dto
{
    [AutoMapFrom(typeof(Entity.ChainStore))]
    public class StoreDropDownDto : EntityDto<long>
    {
        public string Name { get; set; }
    }
}
