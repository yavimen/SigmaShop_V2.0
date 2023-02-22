using SigmaShop.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaShop.StringParser.UserParser
{
    class CClientParser: CUserParser
    {
        public override CAuthorizedUser Parse(string line) 
        {
            string[] param = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (param.Length != 5)
                throw new Exception("Wrong Client parse");

            CPerson person = new CPerson();
            person.Parse(param[2] + " "+param[3] + " "+param[4]);

            return new CClient(param[0], param[1], person);
        }
    }
}
