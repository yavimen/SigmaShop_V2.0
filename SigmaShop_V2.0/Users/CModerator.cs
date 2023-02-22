using SigmaShop.OrderSystem;
using SigmaShop.PromoSystem;
using SigmaShop.StoragesSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaShop.Users
{
    class CModerator: CAuthorizedUser, IModerator
    {
        public CModerator(string login, string password):base(login, password){}

        public void AddPromo(CPromo promo, IPromoBase promobase, IStorageBase storageBase)=> promobase.AddPromo(promo, storageBase);

        public void DeleteOrder(COrder order, IOrderBase orderbase) => orderbase.DeleteOrder(order);
        

        public void DeletePromo(CPromo promo, IPromoBase promobase, IStorageBase storageBase) => promobase.DeletePromo(promo, storageBase);

        public string WatchOrders(IOrderBase orderbase) => orderbase.ShowOrders();

        public string WatchPromos(IPromoBase promobase) => promobase.ToString();

        public string WatchStorage(IStorageBase storageBase) => storageBase.ShowStorages();

        public override string ToString()
        {
            return $"Moder {Login} {Password} ";
        }
    }
}
