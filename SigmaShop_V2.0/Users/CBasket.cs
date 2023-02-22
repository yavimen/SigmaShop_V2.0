using System;
using System.Collections.Generic;
using SigmaShop.StoragesSystem.Goods;

namespace SigmaShop.Users
{
    class CBasket
    {
        protected List<(CGoodsId, int)> _goods;

        public int Lenght 
        {
            get { return _goods.Count; }
        }

        public (CGoodsId,int) this[int index] 
        {
            get {
                if(index>=0&&index<Lenght)
                    return _goods[index];
                throw new IndexOutOfRangeException();
            }
        }

        public CBasket() {
            _goods = new List<(CGoodsId, int)>();
        }

        public void Add(CGoodsId id, int num) 
        {
            for (int i = 0; i < _goods.Count; i++)
            {
                if (_goods[i].Item1 == id) 
                {
                    _goods[i] = (_goods[i].Item1, _goods[i].Item2 + num);
                    return;
                }
            }
            _goods.Add((id, num));
        }

        public void Delete(CGoodsId id, int num) 
        {
            for (int i = 0; i < _goods.Count; i++)
            {
                if (_goods[i].Item1 == id) 
                {
                    _goods[i] = (_goods[i].Item1, _goods[i].Item2 - num);
                    if (_goods[i].Item2 <= 0)
                        _goods.RemoveAt(i);
                    return;
                }
            }
        }

        public void CleanBasket() 
        {
            _goods.Clear();
        }

        public override string ToString() 
        {
            string str = "";
            for (int i = 0; i < _goods.Count; i++)
            {
                str += (_goods[i].Item1).ToString() + " Number: " + _goods[i].Item2.ToString()+"\n";
            }
            return str;
        }

        public List<(CGoodsId, int)> GetGoods() 
        {
            List<(CGoodsId, int)> nlist = new List<(CGoodsId, int)>(_goods);
            return nlist;
        }
    }
}
