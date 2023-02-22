using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaShop
{
    class CUIBridge: IUIBridge
    {
        public string GetString()
        {
            return Console.ReadLine();
        }

        public void ShowString(string str)
        {
            Console.WriteLine(str);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
