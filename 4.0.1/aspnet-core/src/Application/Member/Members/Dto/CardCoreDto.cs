using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnionMall.Member.Dto
{
    [AutoMapFrom(typeof(UnionMall.Entity.Member))]
    public class CardCoreDto : EntityDto<long>
    {
        public string FullName { get; set; }
        public string Level { get; set; }
        public long LevelId { get; set; }
        public string CardID { get; set; }
        public string WeChatName { get; set; }
        public string HeadImg { get; set; }
        public long ChainStoreId { get; set; }
        public int Sex { get; set; }
        public string BirthDay { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public decimal Integral { get; set; }
        public decimal Balance { get; set; }
        public int Status { get; set; }
        public string RegTime { get; set; }
        public int RegSource { get; set; }
    }
}
