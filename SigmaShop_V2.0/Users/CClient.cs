using System;
using System.Collections.Generic;
using SigmaShop.OrderSystem;
using SigmaShop.ShopSystem;
using SigmaShop.StoragesSystem.Goods;

namespace SigmaShop.Users
{
    class CClient : CAuthorizedUser, IClient, IShopper
    {
        protected CPerson _person;
        public CPerson Person
        {
            get { return _person; }
            protected set { _person = value; }
        }

        protected CBasket _basket;

        public CClient(string login, string password, CPerson person) : base(login, password)
        {
            Person = person;
            _basket = new CBasket();
        }

        public void AddGoodsToBasket(CGoodsId id, int num)
        {
            _basket.Add(id, num);
        }

        public void ChangePersonalData(string name, string surname, int phonenumber)
        {
            Person = new CPerson(name, surname, phonenumber);
        }

        public void DeleteGoodsFromBasket(CGoodsId id, int num)
        {
            _basket.Delete(id, num);
        }

        public COrder FormAnOrder(bool delivery)
        {
            if (_basket.Lenght == 0)
                return null;
            COrder order = new COrder(_basket.GetGoods(), Person, delivery);
            _basket.CleanBasket();
            return order;
        }

        public override string ToString()
        {
            return $"Client {Login} {Password} "+Person;
        }

        public string ShowBasket(IShop shop)
        {
            string output = String.Empty;
            double total = 0;
            output += String.Format("{0,11} ", "Type") +
                String.Format("{0, 25} {1, 6} {2, 8} {3,11}", "Name", "PID", "Price", "Made") + " " +
                String.Format("{0,4} ", "ED/V") +
                String.Format(" {0,5}", "Amount") + "\n";
            for (int i = 0; i < _basket.Lenght; i++)
            {
                total += shop.GetGoodsInfo(_basket[i].Item1).Price * _basket[i].Item2;
                output += shop.GetGoodsInfo(_basket[i].Item1).ToString() + String.Format(" {0,5}", _basket[i].Item2.ToString()) + "\n";
            }
            output += $"Total price: {total}";

            return output;
        }

        public void CleanBasket()
        {
            _basket.CleanBasket();
        }
    }
}
