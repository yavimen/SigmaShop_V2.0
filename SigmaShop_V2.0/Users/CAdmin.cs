using SigmaShop.StoragesSystem;
using SigmaShop.StoragesSystem.Goods;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaShop.Users
{
    class CAdmin : CAuthorizedUser, IAdmin
    {
        public CAdmin(string login, string password) : base(login, password) { }

        public void AddGoodsToStorage(CGoods goods, int num, IStorageBase storageBase)
        {
            storageBase.AddGoods(goods, num);
        }

        public void DeleteGoodsFromStorage(CGoodsId id, IStorageBase storageBase)
        {
            int tempnum = 0;
            CGoods goods = storageBase.FindGoods(id);
            for (int i = 0; i < storageBase.FindStorage(id).Lenght; i++)
            {
                if (storageBase.FindStorage(id)[i].Item1.Equals(goods))
                    tempnum = storageBase.FindStorage(id)[i].Item2;
            }
            storageBase.DeleteGoods(id, tempnum);
        }

        public string WatchStorage(IStorageBase storageBase) => storageBase.ShowStorages();

        public override string ToString()
        {
            return $"Admin {Login} {Password} ";
        }
    }
}
