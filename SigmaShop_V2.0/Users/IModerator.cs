using SigmaShop.OrderSystem;
using SigmaShop.PromoSystem;
using SigmaShop.StoragesSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaShop.Users
{
    interface IModerator
    {
        void AddPromo(CPromo promo, IPromoBase promobase, IStorageBase storageBase);
        void DeletePromo(CPromo promo, IPromoBase promobase, IStorageBase storageBase);
        void DeleteOrder(COrder order, IOrderBase orderbase);
        string WatchOrders(IOrderBase orderbase);
        string WatchPromos(IPromoBase promobase);
        string WatchStorage(IStorageBase storageBase);
    }
}
