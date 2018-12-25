using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using UnionMall.Entity;
namespace UnionMall.Goods.Dto
{
    public class CreateOrEditDto : EntityDto<long>
    {
        public Entity.Goods Goods { get; set; }
        public List<Entity.Image> ImageList { get; set; }

    }
}
