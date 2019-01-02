using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using UnionMall.Common.Dto;
namespace UnionMall.Common.CommonSpecValue.Dto
{
    public class CreateOrEdit : EntityDto<long>
    {
        public Entity.CommonSpec Spec { get; set; }
        public List<ValueItemDto> ValueList { get; set; }
    }
}
