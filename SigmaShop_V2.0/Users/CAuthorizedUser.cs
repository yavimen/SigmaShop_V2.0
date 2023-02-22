using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaShop.Users
{
    abstract class CAuthorizedUser
    {
        protected string _login;
        public string Login
        {
            get { return _login; }
            protected set
            {
                //перевірити на наявність логіну в базі
                _login = value;
            }
        }

        protected string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public CAuthorizedUser(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public override bool Equals(object obj)
        {
            if (obj is CAuthorizedUser)
                return _login.Equals((obj as CAuthorizedUser)._login);
            return false;
        }

        public override int GetHashCode()
        {
            int code = 0;
            for (int i = 0; i < _login.Length; i++)
            {
                code += (int)_login[i];
            }
            return code;
        }
    }
}
