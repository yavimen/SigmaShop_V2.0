using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaShop.Users
{
    interface IClient
    {
        //public bool isVip();
        //public void BuyVip();
        public void ChangePersonalData(string name, string surname, int phonenumber);
        //public void ChangeVipStatus(bool status);
    }
}
