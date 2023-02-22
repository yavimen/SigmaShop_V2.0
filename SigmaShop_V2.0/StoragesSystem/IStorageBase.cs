using SigmaShop.StoragesSystem.Storage;
using SigmaShop.StoragesSystem.Goods;
using System.Collections.Generic;
using SigmaShop.StringParser.GoodsParser;

namespace SigmaShop.StoragesSystem
{
    interface IStorageBase
    {
        void AddGoods(CGoods goods, int num);
        bool DeleteGoods(CGoodsId id, int num);
        CGoods FindGoods(CGoodsId id);
        IStorage FindStorage(CGoodsId id);
        string ShowGoods();
        string ShowStorages();
        void FillFromFile(IGoodsParser parser, string path);
        void FillFile(string path);
        bool IsContainGoods(CGoodsId id);
    }
}
