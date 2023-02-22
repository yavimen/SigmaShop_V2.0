using System;
using System.Collections.Generic;
using SigmaShop.PromoSystem;

namespace SigmaShop.StringParser.PromoParser
{
    interface IPromoParser
    {
        CPromo Parse(string line);
    }
}
