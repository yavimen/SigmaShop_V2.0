using System;
using System.Collections.Generic;
using SigmaShop.StoragesSystem.Goods;
using SigmaShop.Users;

namespace SigmaShop.OrderSystem
{
    class COrder
    {
        protected List<(CGoodsId, int)> _goods;

        protected CPerson _person;
        protected CPerson Person
        {
            get { return _person; }
            set { _person = value; }
        }

        protected bool _needdelivery;
        protected bool NeedDelivery
        {
            get { return _needdelivery; }
            set { _needdelivery = value; }
        }
        
        public int Length { get { return _goods.Count; } }

        public (CGoodsId, int) this[int index] 
        {
            get 
            {
                if (index >= 0 && index < Length)
                    return _goods[index];
                throw new IndexOutOfRangeException();
            }
        }

        public COrder(List<(CGoodsId, int)> goods, CPerson person, bool needdelivery = false)
        {
            _goods = goods;
            _person = person;
            _needdelivery = needdelivery;
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < _goods.Count; i++)
                str += _goods[i].Item1 + $" Number: {_goods[i].Item2}" + "\n";

            return $"{Person} ordered:\n{str}Need delivery: {NeedDelivery}";
        }

        public override bool Equals(object obj)
        {
            if (obj is COrder)
                return Person.Equals((obj as COrder).Person);
            return false;
        }

        public override int GetHashCode()
        {
            return Person.GetHashCode();
        }
    }
}
