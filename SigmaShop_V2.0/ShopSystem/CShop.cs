using SigmaShop.OrderSystem;
using SigmaShop.StoragesSystem;
using SigmaShop.StoragesSystem.Goods;
using SigmaShop.Users;
using System;

namespace SigmaShop.ShopSystem
{
    class CShop:IShop
    {
        static protected CShop _instance;

        IStorageBase _storageBase;
        
        IOrderBase _orderBase;

        private CShop() { }

        public void ConfirmOrder(COrder order)
        {
            if (order == null)
                return;
            for (int i = 0; i < order.Length; i++)
                _storageBase.DeleteGoods(order[i].Item1, order[i].Item2);

            _orderBase?.AddOrder(order);
        }

        public void SetBases(IStorageBase storageBase, IOrderBase orderBase) 
        {
            _instance._orderBase = orderBase;
            _instance._storageBase = storageBase;
        }

        static public CShop GetInstance() 
        {
            if (_instance == null)
                _instance = new CShop();
            return _instance;
        }

        public CGoods GetGoodsInfo(CGoodsId id) 
        {
            return _storageBase?.FindGoods(id);
        }

        public string ShowGoods()
        {
            return _storageBase.ShowGoods();
        }
    }
}
