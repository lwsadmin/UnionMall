using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnionMall.Common.Cateogory.Dto
{
    public class CatDropDownDto : EntityDto<long>
    {
        public string Title { get; set; }
        public long ParentId { get; set; }
    }
}
