using System.IO;
using System;
using System.Collections.Generic;
using SigmaShop.StoragesSystem.Goods;
using SigmaShop.StringParser.GoodsParser;

namespace SigmaShop.StoragesSystem.Storage
{
    class CStorage: IStorage
    {
        protected List<(CGoods, int)> _goods;

        public int Lenght => _goods.Count;

        public (CGoods,int) this[int index]
        {
            get {
                if (index >= 0 && index < Lenght)
                    return _goods[index];
                throw new IndexOutOfRangeException();
            } 
        }

        public CStorage() 
        {
            _goods = new List<(CGoods, int)>();
        }

        public void AddGoods(CGoods goods, int num)
        {
            for (int i = 0; i < _goods.Count; i++)
            {
                if (_goods[i].Item1 == goods)
                {
                    _goods[i] = (goods, _goods[i].Item2 + num);
                    return;
                }
            }
            _goods.Add((goods, num));
        }

        public void DeleteGoods(CGoodsId id, int num)
        {
            for (int i = 0; i < _goods.Count; i++)
            {
                if (_goods[i].Item1.Id.Equals(id))
                {
                    _goods[i] = (_goods[i].Item1, _goods[i].Item2 - num);
                    if (_goods[i].Item2 <= 0)
                    {
                        _goods.Remove(_goods[i]);
                    }
                    return;
                }
            }
        }

        public CGoods GetGoods(CGoodsId id)
        {
            for (int i = 0; i < _goods.Count; i++)
            {
                if (_goods[i].Item1.Id.Equals(id))
                {
                    return _goods[i].Item1;
                }
            }
            return null;
        }

        public bool IsContain(CGoodsId id)
        {
            for (int i = 0; i < _goods.Count; i++)
            {
                if (_goods[i].Item1.Id.Equals(id))
                {
                    return true;
                }
            }
            return false;
        }

        public string ShowGoods()
        {
            return ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is CStorage && GetHashCode() == (obj as CStorage).GetHashCode())
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            int hashcode = 0;
            int i = 0;
            foreach (var item in _goods)
            {
                hashcode += item.Item1.GetHashCode() + item.Item2.GetHashCode() + i;
                i++;
            }

            return hashcode;
        }

        public override string ToString()
        {
            string str = String.Empty;

            for (int i = 0; i < _goods.Count; i++)
            {
                str += $"{_goods[i].Item1}"+String.Format(" {0,5}\n", _goods[i].Item2.ToString());
            }
            return str;
        }
    }
}
