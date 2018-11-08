using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using UnionMall.Entity;
namespace UnionMall.Member
{
    [AutoMapFrom(typeof(UnionMall.Entity.Member))]
    public class AddOrEditDto : EntityDto<long>
    {
        [Required(ErrorMessage = "title can not be null")]
        public string Title { get; set; }

        public int TenantId { get; set; }
    }
}
