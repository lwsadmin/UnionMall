using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using UnionMall.Entity;
namespace UnionMall.FlashSale.Dto
{
    public class CreateOrEditDto : EntityDto<long>
    {
        public Entity.FlashSale FlashSale { get; set; }
        public List<Entity.Image> ImageList { get; set; }

    }
}
