using SigmaShop.OrderSystem;
using SigmaShop.StoragesSystem;
using SigmaShop.StoragesSystem.Goods;

namespace SigmaShop.ShopSystem
{
    interface IShop
    {
        string ShowGoods();
        void ConfirmOrder(COrder order);
        void SetBases(IStorageBase storageBase, IOrderBase orderBase);
        CGoods GetGoodsInfo(CGoodsId id);
    }
}
