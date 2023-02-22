using SigmaShop.StringParser.UserParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SigmaShop.Users
{
    class CAuthUserBase : IAuthUserBase
    {
        protected List<CAuthorizedUser> _users;

        static protected CAuthUserBase _instance;

        protected CAuthUserBase() 
        {
            _users = new List<CAuthorizedUser>();
        }

        static public CAuthUserBase GetInstance() 
        {
            if (_instance == null)
                _instance = new CAuthUserBase();
            return _instance;
        }

        public CAuthorizedUser this[int index] 
        {
            get {
                if (index >= 0 && index < Length)
                    return _users[index];
                throw new IndexOutOfRangeException();
            }
        }

        public int Length => _users.Count;

        public void AddUser(CAuthorizedUser user)
        {
            if (!_users.Contains(user))
                _users.Add(user);
        }

        public void DeleteUser(CAuthorizedUser user)
        {
            _users.Remove(user);
        }

        public void FillFile(string path)
        {
            string str = "";
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i] is CAdmin)
                    str += "Admin "+_users[i].Login+" " + _users[i].Password+"\n";
                if (_users[i] is CModerator)
                    str += "Moder " + _users[i].Login + " " + _users[i].Password + "\n";
                if (_users[i] is CClient)
                    str += "Client " + _users[i].Login + " " + _users[i].Password + " " + (_users[i] as CClient).Person + "\n";
                File.WriteAllText(path, str);
            }
        }

        public void FillFromFile(IUserParser parser, string path)
        {
            string[] users = File.ReadAllText(path).Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < users.Length; i++)
            {
                AddUser(parser.Parse(users[i]));
            }
        }

        public bool IsContain (string login)
        {
            for (int i = 0; i< _users.Count; i++)
			{
                if (_users[i].Login.Equals(login))
                    return true;
			}
            return false;
        }

        public string ShowUsers() 
        {
            string str = "";
            for (int i = 0; i < _users.Count; i++)
            {
                str += _users[i].ToString() + "\n";
            }
            return str;
        }

        public override string ToString()
        {
            return ShowUsers();
        }

        public CAuthorizedUser GetUser(string login)
        {
            if(IsContain(login))
                for (int i = 0; i < _users.Count; i++)
                {
                    if (_users[i].Login == login)
                        return _users[i];
                }
            return null;
        }
    }
}
