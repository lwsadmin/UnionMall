using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using UnionMall.Entity;
using UnionMall.Member;
namespace UnionMall.Member.Dto
{
    [AutoMapFrom(typeof(MemberLevel))]
    public class AddOrEditDto : EntityDto<long>
    {
        [Required(ErrorMessage = "title can not be null")]
        public string Title { get; set; }

        public int TenantId { get; set; }
        public decimal InitPoint { get; set; }

        public decimal SellPrice { get; set; }

        public decimal MinPoint { get; set; }

        public decimal MaxPoint { get; set; }
    }
}
