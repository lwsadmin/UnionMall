using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnionMall.Goods.Dto
{
    public class ReplyListDto: EntityDto<long>
    {
        public string MemberName { get; set; }

        public string Content { get; set; }

        public string Praise { get; set; }
        public ReplyListDto(long id,string memberName,string content, string praise)
        {
            this.Id = id;
            this.MemberName = memberName;
            this.Content = content;
            this.Praise = praise;
        }
    }
}
