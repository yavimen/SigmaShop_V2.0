using SigmaShop.StringParser.OrdersParser;
using SigmaShop.Users;
using System;
using System.Collections.Generic;
using System.IO;

namespace SigmaShop.OrderSystem
{
    class COrderBase : IOrderBase
    { 
        static protected COrderBase _instance;

        protected COrderBase()
        {
            _orders = new List<COrder>();
        }

        static public COrderBase GetInstance() 
        {
            if (_instance == null)
                _instance = new COrderBase();
            return _instance;
        }

        protected List<COrder> _orders;

        public COrder this[int index] 
        {
            get {
                if (index >= 0 && index < Length)
                    return _orders[index];
                throw new IndexOutOfRangeException("COrderBase");
            }
        }

        public int Length => _orders.Count;

        public void AddOrder(COrder order)
        {
            _orders.Add(order);
        }

        public void DeleteOrder(COrder order)
        {
            _orders.Remove(order);
        }

        public bool IsContain(COrder order)
        {
            return _orders.Contains(order);
        }
        
        public void FillFromFile(IOrderParser parser, string path) 
        {
            string fullfile = File.ReadAllText(@path);
            string[] orders = fullfile.Split("\r\n-\r\n", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < orders.Length; i++)
            {
                _orders.Add(parser.Parse(orders[i]));
            }
        }

        public void FillFile(string path)
        {
            File.WriteAllText(path, ToString());
        }

        public string ShowOrders() 
        {
            string str = "";
            for (int i = 0; i < _orders.Count; i++)
            {
                str +=$"Orders number {i+1}\n"+_orders[i] + "\n-\n";
            }
            return str;
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < _orders.Count; i++)
            {
                str += _orders[i] + "\n-\n";
            }
            return str;
        }
    }
}
