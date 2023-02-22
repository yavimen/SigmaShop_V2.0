using SigmaShop.StoragesSystem;
using SigmaShop.StoragesSystem.Goods;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaShop.Users
{
    interface IAdmin
    {
        void AddGoodsToStorage(CGoods goods, int num, IStorageBase storageBase);
        void DeleteGoodsFromStorage(CGoodsId id, IStorageBase storageBase);
        string WatchStorage(IStorageBase storageBase);
        //+ ChangeClientStatus(login: string)
    }
}
