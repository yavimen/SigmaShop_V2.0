using System;
using System.Collections.Generic;
using SigmaShop.OrderSystem;
using SigmaShop.StoragesSystem.Goods;
using SigmaShop.Users;

namespace SigmaShop.StringParser.OrdersParser
{
    class COrderParser:IOrderParser
    {
        public COrder Parse(string line) 
        {
            string[] PersonStringAndOther = line.Split("ordered:", StringSplitOptions.RemoveEmptyEntries);

            if (PersonStringAndOther.Length != 2)
                throw new Exception("Order parser is fault by person parse");

            CPerson person = new CPerson();
            person.Parse(PersonStringAndOther[0]);


            string[] IdListAndNeedDelivery = PersonStringAndOther[1].Split("Need delivery: ", StringSplitOptions.RemoveEmptyEntries);

            if(IdListAndNeedDelivery.Length != 2)
                throw new Exception("Order parser is fault by need delivery parse");

            if(!bool.TryParse(IdListAndNeedDelivery[1], out bool needdelivery))
                throw new Exception("Wrong bool variable in text");

            List<(CGoodsId, int)> ps = new List<(CGoodsId, int)>();

            string[] goods = IdListAndNeedDelivery[0].Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            string[] temp;
            for (int i = 0; i < goods.Length; i++)
            {
                temp = goods[i].Split(" Number: ", StringSplitOptions.RemoveEmptyEntries);
                if (temp.Length != 2)
                    throw new Exception();
                if (!int.TryParse(temp[0], out int id) | !int.TryParse(temp[1], out int num))
                    throw new Exception("Wrong id or num!");
                ps.Add((new CGoodsId(id), num));
            }

            return new COrder(ps, person, needdelivery);
        }
    }
}
