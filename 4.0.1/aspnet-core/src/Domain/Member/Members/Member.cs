using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;

namespace UnionMall.Entity
{
    [Table("TMember")]
    public class Member : Entity<long>
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "title can not be null")]
        public string FullName { get; set; } = "";
        public int? TenantId { get; set; }

        public long BusinessId { get; set; }

        public long ChainStoreId { get; set; }
        public long LevelId { get; set; }

        public int? ReferrerID { get; set; }

        public string CardID { get; set; }
        public string WeChatName { get; set; } = "";
        public string PassWord { get; set; }
        public string HeadImg { get; set; }
        public int Sex { get; set; }
        public DateTime BirthDay { get; set; } = DateTime.Now;
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int ProvinceId { get; set; } = 1;
        public int CityId { get; set; } = 1;
        public int DistrictId { get; set; } = 1;
        public string Address { get; set; } = "";
        public decimal Integral { get; set; } = 0;

        public decimal Balance { get; set; } = 0;

        public int TotalConsumeCount { get; set; } = 0;
        public DateTime LastConsumeTime { get; set; } = DateTime.Now;
        public int Status { get; set; } = 0;
        public DateTime RegTime { get; set; } = DateTime.Now;
        public int RegSource { get; set; } = 0;
        public string OpenID { get; set; } = "";
    }
}
