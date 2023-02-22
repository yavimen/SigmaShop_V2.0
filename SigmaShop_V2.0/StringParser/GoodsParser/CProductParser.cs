using System;
using System.Collections.Generic;
using SigmaShop.StoragesSystem.Goods;

namespace SigmaShop.StringParser.GoodsParser
{
    class CProductParser:CGoodsParser
    {
        public override (CGoods, int) Parse(string line) 
        {
            string[] param = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (param.Length != 6 | !int.TryParse(param[1], out int id) | !double.TryParse(param[2], out double price)
                | !DateTime.TryParse(param[3], out DateTime made) | !int.TryParse(param[4], out int expdays)| !int.TryParse(param[5], out int number))
                throw new Exception("Wrong param in line for Product");

            return (new CProduct(param[0], new CGoodsId(id), price, param[3], expdays), number);
        } 
    }
}
