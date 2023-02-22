using SigmaShop.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaShop
{
    class CLogIn
    {
        static public CAuthorizedUser LogIn(IAuthUserBase userBase, IUIBridge UIBridge)
        {
            UIBridge.Clear();
            UIBridge.ShowString("Enter login: ");
            string login = UIBridge.GetString();
            while (!userBase.IsContain(login.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]))
            {
                UIBridge.Clear();
                UIBridge.ShowString("Account not found.\n" +
                                  "1. Try again\n" +
                                  "2. To register\n");
                int choice;
                while (!int.TryParse(UIBridge.GetString(), out choice) | choice > 2 | choice < 1)
                {
                    UIBridge.ShowString("Wrong choice. Try again: ");
                }
                switch (choice)
                {
                    case 1:
                        UIBridge.Clear();
                        UIBridge.ShowString("Enter login again: ");
                        login = UIBridge.GetString();
                        break;
                    default:
                        return LogUp(userBase, UIBridge);
                }
            }
            UIBridge.ShowString("Enter password");
            string password = UIBridge.GetString();
            while (!userBase.GetUser(login).Password.Equals(password))
            {
                UIBridge.ShowString("Wrong password. Try again:");
                password = UIBridge.GetString();
            }
            return userBase.GetUser(login);
        }

        static private CClient LogUp(IAuthUserBase userBase, IUIBridge UIBridge)
        {
            UIBridge.Clear();
            UIBridge.ShowString("Enter new login: ");
            string login = UIBridge.GetString();
            while (userBase.IsContain(login.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]))
            {
                UIBridge.ShowString("User exist. Enter new login: ");
                login = UIBridge.GetString();
            }
            UIBridge.ShowString("Enter password: ");
            string password = UIBridge.GetString();

            UIBridge.ShowString("Enter name: ");
            string name = UIBridge.GetString();

            UIBridge.ShowString("Enter surname: ");
            string surname = UIBridge.GetString();

            UIBridge.ShowString("Enter phonenumber: ");
            int phnum;
            while(!int.TryParse(UIBridge.GetString(), out phnum))
                UIBridge.ShowString("Uncorrect number. Enter again: ");

            return new CClient(login, password, new CPerson(name, surname, phnum));
        }
    }
}
