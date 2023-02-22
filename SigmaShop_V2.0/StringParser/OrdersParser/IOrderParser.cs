using System;
using System.Collections.Generic;
using SigmaShop.OrderSystem;

namespace SigmaShop.StringParser.OrdersParser
{
    interface IOrderParser
    {
        COrder Parse(string line);
    }
}
