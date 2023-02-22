using SigmaShop.StoragesSystem.Goods;
using System;

namespace SigmaShop.StringParser.GoodsParser
{
    class CHouseholdParser: CGoodsParser
    {
        public override (CGoods, int) Parse(string line)
        {
            string[] param = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (param.Length != 6 | !int.TryParse(param[1], out int id) | !double.TryParse(param[2], out double price)
                | !DateTime.TryParse(param[3], out DateTime made) | !double.TryParse(param[4], out double volume)| !int.TryParse(param[5], out int number))
                throw new Exception("Wrong param in line for Household Goods");

            return (new CHousehold_Goods(param[0], new CGoodsId(id), price, param[3], volume), number);
        }
    }
}
