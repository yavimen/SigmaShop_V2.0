using SigmaShop.StoragesSystem.Goods;
using SigmaShop.StringParser.GoodsParser;

namespace SigmaShop.StoragesSystem.Storage
{
    interface IStorage
    {
        void AddGoods(CGoods goods, int num);
        void DeleteGoods(CGoodsId id, int num);
        bool IsContain(CGoodsId id);
        CGoods GetGoods(CGoodsId id);
        string ShowGoods();
        bool Equals(object obj);
        int GetHashCode();
        string ToString();
        (CGoods, int) this[int index] { get; }
        int Lenght { get; }
    }
}
