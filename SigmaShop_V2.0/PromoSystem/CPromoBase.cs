using System.IO;
using System;
using System.Collections.Generic;
using SigmaShop.StoragesSystem.Goods;
using SigmaShop.StoragesSystem;
using SigmaShop.StringParser.PromoParser;

namespace SigmaShop.PromoSystem
{
    class CPromoBase : IPromoBase
    {
        static protected CPromoBase _instance;

        protected List<CPromo> _promos;

        protected CPromoBase() 
        {
            _promos = new List<CPromo>();
        }

        static public CPromoBase GetInstance() 
        {
            if (_instance == null) 
            {
                _instance = new CPromoBase();
            }
            return _instance;
        }

        public void AddPromo(CPromo promo, IStorageBase storageBase)
        {
            if (storageBase.FindGoods(promo.Id) == null)
                return;

            if (!_promos.Contains(promo)) 
            {
                _promos.Add(promo);
                CGoods goods = storageBase.FindGoods(promo.Id);
                if (goods == null)
                    return;
                goods.IsHasPromo = true;
                goods.Interest = promo.Interest;
                return;
            }

            DeletePromo(promo, storageBase);
            _promos.Add(promo);
        }

        public void DeletePromo(CPromo promo, IStorageBase storageBase)
        {
            _promos.Remove(promo);
            CGoods goods = storageBase.FindGoods(promo.Id);
            goods.IsHasPromo = false;
            goods.Price = goods.Price + (goods.Price * (promo.Interest / 100));
        }

        public void FillFromFile(IPromoParser parser, string path, IStorageBase storageBase)
        {
            string[] promos = File.ReadAllText(path).Split(new char[] { '\n','\r' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < promos.Length; i++)
            {
               AddPromo(parser.Parse(promos[i]), storageBase);
            }
        }

        public void FillFile(string path)
        {
            string str = "";
            for (int i = 0; i < _promos.Count; i++)
            {
                str += $"{_promos[i].Id} {_promos[i].Interest}\n";
            }

            File.WriteAllText(path, str);
        }

        public bool IsContain(CPromo promo)
        {
            return _promos.Contains(promo);
        }

        public override string ToString()
        {
            string outputstr = "";

            for (int i = 0; i < _promos.Count; i++)
            {
                outputstr += _promos[i].ToString() + "\n";
            }

            return outputstr;
        }
    }
}
