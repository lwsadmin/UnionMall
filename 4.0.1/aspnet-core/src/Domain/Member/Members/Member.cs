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
        public string FullName { get; set; }
        public int? TenantId { get; set; }

        public long BusinessId { get; set; }

        public long ChainStoreId { get; set; }
        public long LevelId { get; set; }
        public string CardID { get; set; }
        public string WeChatName { get; set; }
        public string PassWord { get; set; }
        public string HeadImg { get; set; }
        public int Sex { get; set; }
        public DateTime BirthDay { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string Address { get; set; }
        public decimal Integral { get; set; }

        public decimal Balance { get; set; }

        public int TotalConsumeCount { get; set; }
        public DateTime LastConsumeTime { get; set; }
        public int Status { get; set; }
        public DateTime RegTime { get; set; }
        public int RegSource { get; set; }
        public string OpenID { get; set; }
        public string Memo { get; set; }
    }
}
