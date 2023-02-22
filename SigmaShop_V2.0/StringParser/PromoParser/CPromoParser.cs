using SigmaShop.PromoSystem;
using SigmaShop.StoragesSystem.Goods;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaShop.StringParser.PromoParser
{
    class CPromoParser : IPromoParser
    {
        public CPromo Parse(string line)
        {
            string[] param = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (param.Length != 2 | !int.TryParse(param[0], out int id) | !int.TryParse(param[1], out int interest))
                throw new Exception("Wrong promo parse param or number of their");
            return new CPromo(interest, new CGoodsId(id));
        }
    }
}
