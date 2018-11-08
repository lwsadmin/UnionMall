﻿using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using UnionMall.Entity;
namespace UnionMall.Business.Dto
{
    [AutoMapFrom(typeof(Entity.Business))]
    public class BusinessDropDownDto : EntityDto<long>
    {
        public string BusinessName { get; set; }
    }
}
