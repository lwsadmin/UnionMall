using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace UnionMall.Goods.Dto
{
    public class DropDownDto: EntityDto<long>
    {
        public string Title { get; set; }
        public int Level { get; set; }
        public long ParentId { get; set; }
    }
}
