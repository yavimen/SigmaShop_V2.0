using SigmaShop.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaShop.StringParser.UserParser
{
    interface IUserParser
    {
        CAuthorizedUser Parse(string line);
    }
}
