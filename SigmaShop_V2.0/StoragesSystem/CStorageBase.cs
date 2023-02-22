using SigmaShop.StoragesSystem.Goods;
using SigmaShop.StoragesSystem.Storage;
using SigmaShop.StringParser.GoodsParser;
using System.IO;
using System.Collections.Generic;
using System;

namespace SigmaShop.StoragesSystem
{
    class CStorageBase: IStorageBase
    {
        static protected CStorageBase _instance;

        protected List<IStorage> _storages;

        protected CStorageBase()
        {
            _storages = new List<IStorage>();
        }

        static public CStorageBase GetInstance()
        {
            if (_instance == null)
                _instance = new CStorageBase();
            return _instance;
        }

        public bool DeleteGoods(CGoodsId id, int num)
        {
            FindStorage(id).DeleteGoods(id, num);
            return true;
        }

        public void AddGoods(CGoods goods, int num) 
        {
            if (goods is CProduct)
            {
                if (_storages.Count == 0)
                    _storages.Add(new CStorage());
                _storages[0].AddGoods(goods, num);
            }
            else if (goods is CHousehold_Goods)
            {
                if (_storages.Count == 1)
                    _storages.Add(new CStorage());
                _storages[1].AddGoods(goods, num);
            }
        }

        public void FillFile(string path)
        {
            string str = "";
            for (int i = 0; i < _storages.Count; i++)
            {
                for (int j = 0; j < _storages[i].Lenght; j++)
                {
                    if(_storages[i][j].Item1 is CProduct)
                        str += $"Product {_storages[i][j].Item1.Name} {_storages[i][j].Item1.Id} {_storages[i][j].Item1.Price} {_storages[i][j].Item1.Made} {(_storages[i][j].Item1 as CProduct).ExpirationDays} {_storages[i][j].Item2}\n";
                    if(_storages[i][j].Item1 is CHousehold_Goods)
                        str += $"Household_Goods {_storages[i][j].Item1.Name} {_storages[i][j].Item1.Id} {_storages[i][j].Item1.Price} {_storages[i][j].Item1.Made} {(_storages[i][j].Item1 as CHousehold_Goods).Volume} {_storages[i][j].Item2}\n";
                }
            }
            File.WriteAllText(path, str);
        }

        public void FillFromFile(IGoodsParser parser, string path)
        {
            string textfile = File.ReadAllText(@path);

            string[] products = textfile.Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < products.Length; i++)
            {
                (CGoods, int) goods;
                goods = parser.Parse(products[i]);
                if (goods.Item1 is CProduct)
                {
                    if (_storages.Count == 0)
                        _storages.Add(new CStorage());
                    _storages[0].AddGoods(goods.Item1, goods.Item2);
                }
                if (goods.Item1 is CHousehold_Goods) 
                {
                    if (_storages.Count == 1)
                        _storages.Add(new CStorage());
                    _storages[1].AddGoods(goods.Item1, goods.Item2);
                }
            }
        
        }

        public CGoods FindGoods(CGoodsId id)
        {
            if (FindStorage(id) == null)
                return null;
            return FindStorage(id).GetGoods(id);
        }

        public IStorage FindStorage(CGoodsId id)
        {
            for (int i = 0; i < _storages.Count; i++)
            {
                if (_storages[i].IsContain(id)) 
                {
                    return _storages[i];
                }
            }

            return null;
        }

        public string ShowGoods()
        {
            string str = "";
            str+= String.Format("{0,11} ", "Type") +
                String.Format("{0, 25} {1, 6} {2, 8} {3,11}", "Name", "PID", "Price", "Made") + " " +
                String.Format("{0,4} ", "ED/V") +
                String.Format(" {0,5}", "Amount") + "\n";
            for (int i = 0; i < _storages.Count; i++)
            {
                str += _storages[i].ShowGoods();
            }
            return str;
        }

        public string ShowStorages()
        {
            string str = "";
            for (int i = 0; i < _storages.Count; i++)
            {
                str += "Storage" + (i+1) +"\n"+ _storages[i].ShowGoods();
            }
            return str;
        }

        public bool IsContainGoods(CGoodsId id)
        {
            for (int i = 0; i < _storages.Count; i++)
            {
                if (_storages[i].IsContain(id))
                    return true;
            }
            return false;
        }
    }
}
