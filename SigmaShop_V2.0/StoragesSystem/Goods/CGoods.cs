using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaShop.StoragesSystem.Goods
{
    class CGoods
    {
        protected string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        protected CGoodsId _id;
        public CGoodsId Id
        {
            get { return _id; }
            protected set
            {
                _id = value;
            }
        }

        protected double _price;
        public double Price
        {
            get { return _price; }
            set
            {
                if (value <= 0)
                    throw new Exception("Price can't be zero or less than zero!");
                _price = value;
            }
        }

        protected string _made;
        public string Made
        {
            get { return _made; }
            set
            {
                if (!DateTime.TryParse(value, out DateTime date))
                    throw new Exception("Uncorrect date!");
                _made = value;
            }
        }

        protected bool _ishaspromo;
        public bool IsHasPromo 
        {
            get { return _ishaspromo; }
            set { _ishaspromo = value; }
        }

        protected int _interest;

        public int Interest
        {
            get { return _interest; }
            set {
                if (value > 99 || value < 0)
                    throw new Exception("Wrong interest for goods");
                _interest = value; }
        }

        public CGoods(string name = "n", CGoodsId id = null, double price = 1, string made = "11.11.2021")
        {
            Name = name;
            Id = id;
            Price = price;
            Made = made;
        }

        public override string ToString()
        {
            if (IsHasPromo)
                return String.Format("{0, 25} {1, 6} {2, 8} {3,11}", ("[SALE] " + _name), _id.ToString(), Math.Round(_price - _price * Interest / 100.0, 2).ToString(), _made.ToString());

            return String.Format("{0, 25} {1, 6} {2, 8} {3,11}", (_name), _id.ToString(), Math.Round(_price - _price * Interest / 100.0, 2).ToString(), _made.ToString());
        }

        public override bool Equals(object obj)
        {
            if (obj is CGoods)
                return Id.Equals((obj as CGoods).Id) && Name.Equals((obj as CGoods).Name);
            return false;
        }

        public override int GetHashCode()
        {
            return _id.Id;
        }
    }
}
