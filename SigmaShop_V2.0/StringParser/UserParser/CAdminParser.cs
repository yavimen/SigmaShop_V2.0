using SigmaShop.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaShop.StringParser.UserParser
{
    class CAdminParser: CUserParser
    {
        public override CAuthorizedUser Parse(string line)
        {
            string[] param = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (param.Length != 2)
                throw new Exception("Wrong Moder parser");

            return new CAdmin(param[0], param[1]);
        }
    }
}
