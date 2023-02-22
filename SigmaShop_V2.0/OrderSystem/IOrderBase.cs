using SigmaShop.StringParser.OrdersParser;
using SigmaShop.Users;

namespace SigmaShop.OrderSystem
{
    interface IOrderBase
    {
        void AddOrder(COrder order);
        
        void DeleteOrder(COrder order);

        bool IsContain(COrder order);

        void FillFromFile(IOrderParser parser, string path);

        void FillFile(string path);

        COrder this[int index] { get; }

        int Length { get; }

        public string ShowOrders();
    }
}
