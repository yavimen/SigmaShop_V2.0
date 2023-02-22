using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaShop.Users
{
    class CPerson
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        private int _phonenumber;

        public int PhoneNumber
        {
            get { return _phonenumber; }
            set { _phonenumber = value; }
        }

        public CPerson(string name = "N", string surname = "S", int phonenumber = 0)
        {
            Name = name;
            Surname = surname;
            PhoneNumber = phonenumber;
        }

        public override string ToString()
        {
            return $"{Name} {Surname} {PhoneNumber}";
        }

        public override bool Equals(object obj)
        {
            if (obj is CPerson)
                return (Name.Equals((obj as CPerson).Name) && Surname.Equals((obj as CPerson).Surname) &&
                    PhoneNumber.Equals((obj as CPerson).PhoneNumber));
            return false;
        }

        public override int GetHashCode()
        {
            return PhoneNumber;
        }

        public void Parse(string line) 
        {
            string[] param = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (param.Length != 3 | !int.TryParse(param[2], out int phnumber))
                throw new Exception("Person parse fault!");
            Name = param[0];
            Surname = param[1];
            PhoneNumber = phnumber;
        }
    }
}
