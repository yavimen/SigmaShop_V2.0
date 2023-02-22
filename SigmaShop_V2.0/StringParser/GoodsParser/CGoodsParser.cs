using SigmaShop.StoragesSystem.Goods;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaShop.StringParser.GoodsParser
{
    class CGoodsParser : IGoodsParser
    {
        virtual public (CGoods, int) Parse(string line)
        {
            string[] param = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (param.Length != 7) 
                throw new Exception("Wrong number of param in line for CGoodsParser!");

            if (param[0] == "Product")
                return new CProductParser().Parse(param[1] + " " + param[2] + " " + param[3] + " " + param[4] + " " + param[5]+ " " + param[6]);
            else if(param[0] == "Household_Goods")
                return new CHouseholdParser().Parse(param[1] + " " + param[2] + " " + param[3] + " " + param[4] + " " + param[5]+" " + param[6]);

            throw new Exception("First param in CGoods Parse must be Product or Household_Goods");
        }
    }
}
