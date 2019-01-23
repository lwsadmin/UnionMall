using System;
using System.Collections.Generic;
using System.Text;

namespace UnionMall.Goods.GoodsOrder.Dto
{
    public class SubmitOrderDto
    {
        public Entity.GoodsOrder Order { get; set; }
        public List<Entity.GoodsOrderItem> ItemList { get; set; }
    }
}
