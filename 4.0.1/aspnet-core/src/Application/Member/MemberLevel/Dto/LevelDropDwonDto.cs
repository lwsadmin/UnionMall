using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using UnionMall.Entity;
namespace UnionMall.Member.Dto
{
    [AutoMapFrom(typeof(UnionMall.Entity.MemberLevel))]
    public class LevelDropDwonDto: EntityDto<long>
    {
        public string Title { get; set; }
    }
}
