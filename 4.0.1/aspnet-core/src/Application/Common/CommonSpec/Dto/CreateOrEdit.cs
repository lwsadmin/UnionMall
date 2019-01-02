using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
namespace UnionMall.Common.Dto
{
    public class CreateOrEdit : EntityDto<long>
    {
        public Entity.CommonSpec Spec { get; set; }
        public List<Entity.CommonSpecValue> ValueList { get; set; }
    }
}
