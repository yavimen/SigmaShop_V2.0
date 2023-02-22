using SigmaShop.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaShop.StringParser.UserParser
{
    class CUserParser : IUserParser
    {
        virtual public CAuthorizedUser Parse(string line)
        {
            string[] param = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (param[0] == "Client")
            {
                return new CClientParser().Parse(param[1] + " " + param[2] + " " + param[3] + " " + param[4] + " " + param[5]);
            }
            if (param[0] == "Admin")
            {
                return new CAdminParser().Parse(param[1] + " " + param[2]);
            }
            if (param[0] == "Moder")
            {
                return new CModerParser().Parse(param[1] + " " + param[2]);
            }

            throw new Exception("User parse fault");
        }
    }
}
