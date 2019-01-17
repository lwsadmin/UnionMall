using System;
using System.Collections.Generic;
using System.Text;
namespace UnionMall.GiftMall.Dto
{
    public class ExchangDto
    {
        public Entity.GiftOrder Order { get; set; }

        public List<Entity.GiftOrderItem> ItemList { get; set; }
    }
}
