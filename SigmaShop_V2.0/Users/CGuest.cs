using SigmaShop.OrderSystem;
using SigmaShop.ShopSystem;
using SigmaShop.StoragesSystem.Goods;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaShop.Users
{
    class CGuest : IShopper
    {
        protected CBasket _basket;

        public CGuest()
        {
            _basket = new CBasket();
        }

        public void AddGoodsToBasket(CGoodsId id, int num)
        {
            _basket.Add(id, num);
        }

        public void CleanBasket()
        {
            _basket.CleanBasket();
        }

        public void DeleteGoodsFromBasket(CGoodsId id, int num)
        {
            _basket.Delete(id, num);
        }

        public COrder FormAnOrder(bool delivery)
        {
            if (_basket.Lenght == 0)
                return null;
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your surname: ");
            string surname = Console.ReadLine();
            Console.WriteLine("Enter your phonenumber: ");
            string phnum = Console.ReadLine();

            return new COrder(_basket.GetGoods(), new CPerson(name, surname, int.Parse(phnum)), delivery);
        }

        public string ShowBasket(IShop shop)
        {
            string output = String.Empty;
            double total = 0;
            output += String.Format("{0,11} ", "Type") + 
                String.Format("{0, 25} {1, 6} {2, 8} {3,11}", "Name", "PID", "Price", "Made") + " " + 
                String.Format("{0,4} ", "ED/V")+
                String.Format(" {0,5}", "Amount")+"\n";
            for (int i = 0; i < _basket.Lenght; i++)
            {
                total += shop.GetGoodsInfo(_basket[i].Item1).Price * _basket[i].Item2;
                output += shop.GetGoodsInfo(_basket[i].Item1).ToString() + String.Format(" {0,5}", _basket[i].Item2.ToString()) + "\n";
            }
            output += $"Total price: {total}";

            return output;
        }
    }
}
