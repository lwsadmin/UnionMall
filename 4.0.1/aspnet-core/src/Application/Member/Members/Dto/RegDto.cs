using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace UnionMall.Member.Dto
{
    [AutoMap(typeof(Entity.Member))]
    public class RegDto : EntityDto<long>
    {

        public string FullName { get; set; }
        public int? TenantId { get; set; }
        public long BusinessId { get; set; }

        public long ChainStoreId { get; set; }
        public string Mobile { get; set; }
        public long LevelId { get; set; }
        public string CardID { get; set; }

        public int? ReferrerID { get; set; }

        public string PassWord { get; set; }

        public int? Sex { get; set; } = 0;
    }
    //public enum Sex
    //{

    //}
}
