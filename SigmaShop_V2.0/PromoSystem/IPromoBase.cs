using System;
using System.Collections.Generic;
using SigmaShop.StoragesSystem;
using SigmaShop.StringParser.PromoParser;

namespace SigmaShop.PromoSystem
{
    interface IPromoBase
    {
        public void AddPromo(CPromo promo, IStorageBase storageBase);
        public void DeletePromo(CPromo promo, IStorageBase storageBase);
        public bool IsContain(CPromo promo);
        void FillFromFile(IPromoParser parser, string path, IStorageBase storageBase);
        void FillFile(string path);
    }
}
