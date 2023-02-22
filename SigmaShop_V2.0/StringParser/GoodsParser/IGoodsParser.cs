using System;
using SigmaShop.StoragesSystem.Goods;

namespace SigmaShop.StringParser.GoodsParser
{
    interface IGoodsParser
    {
        (CGoods,int) Parse(string line);
    }
}
