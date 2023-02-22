using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaShop
{
    interface IUIBridge
    {
        abstract string GetString();

        abstract void ShowString(string str);

        abstract void Clear();
    }
}
