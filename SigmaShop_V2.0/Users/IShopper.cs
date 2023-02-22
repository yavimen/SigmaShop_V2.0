using SigmaShop.OrderSystem;
using SigmaShop.ShopSystem;
using SigmaShop.StoragesSystem.Goods;

namespace SigmaShop.Users
{
    interface IShopper
    {
        public COrder FormAnOrder(bool delivery);
        public void AddGoodsToBasket(CGoodsId id, int num);
        public void DeleteGoodsFromBasket(CGoodsId id, int num);
        public void CleanBasket();
        public string ShowBasket(IShop shop);
    }
}
