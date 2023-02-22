using SigmaShop.OrderSystem;
using SigmaShop.PromoSystem;
using SigmaShop.ShopSystem;
using SigmaShop.StoragesSystem;
using SigmaShop.StoragesSystem.Goods;
using SigmaShop.StringParser.GoodsParser;
using SigmaShop.StringParser.OrdersParser;
using SigmaShop.StringParser.PromoParser;
using SigmaShop.StringParser.UserParser;
using SigmaShop.Users;
using System;

namespace SigmaShop
{
    class CStoreSystem
    {
        protected string _storagefilepath = "Goods.txt";
        protected string _userfilepath = "Users.txt";
        protected string _promofilepath = "Promos.txt";
        protected string _orderfilepath = "Orders.txt";
        protected IStorageBase _storageBase;
        protected IPromoBase _promoBase;
        protected IAuthUserBase _userBase;
        protected IOrderBase _orderBase;
        protected IShop _shop;
        IUIBridge _uiBridge;
        public CStoreSystem(IStorageBase storageBase, IPromoBase promoBase, 
            IAuthUserBase userBase, IOrderBase orderBase, IShop shop, IUIBridge UIBridge)
        {
            this._uiBridge = UIBridge;
            _orderBase = orderBase;
            _promoBase = promoBase;
            _storageBase = storageBase;
            _userBase = userBase;
            _shop = shop;
            LoadSystem();
        }

        protected void LoadSystem()
        {
            _storageBase.FillFromFile(new CGoodsParser(), _storagefilepath);

            _promoBase.FillFromFile(new CPromoParser(), _promofilepath, _storageBase);

            _userBase.FillFromFile(new CUserParser(), _userfilepath);

            _orderBase.FillFromFile(new COrderParser(), _orderfilepath);

            _shop.SetBases(_storageBase, _orderBase);
        }

        public void ShowMainPage()
        {
            CAuthorizedUser guest = ShowGuestPage();
            while (guest != null)
            {
                if (guest is CClient)
                {
                    ShowClientPage(guest);
                }

                else if (guest is CAdmin)
                {
                    ShowAdminPage(guest);
                }
                else if (guest is CModerator)
                {
                    ShowModerPage(guest);
                }

                guest = ShowGuestPage();
            }

            _uiBridge.ShowString("Bye ;)");
        }

        protected CAuthorizedUser ShowGuestPage()
        {
            CGuest guest = new CGuest();

            bool prexit = false;
            while (!prexit)
            {
                _uiBridge.ShowString("1. Watch goods\n" +
                                    "2. Go to basket\n" +
                                    "3. Log in\n" +
                                    "4. Exit");
                int guestchoice = 0;
                while (!int.TryParse(_uiBridge.GetString(), out guestchoice) | guestchoice > 4 | guestchoice < 1)
                {
                    _uiBridge.ShowString("Wrong choice. Try again: ");
                }

                switch (guestchoice)
                {
                    case 1:
                        bool exit = false;
                        while (!exit)
                        {
                            _uiBridge.Clear();
                            _uiBridge.ShowString(_shop.ShowGoods());
                            _uiBridge.ShowString("1. AddGoodsToBasket\n" +
                                                "2. Back\n");
                            while (!int.TryParse(_uiBridge.GetString(), out guestchoice) || guestchoice > 2 || guestchoice < 1)
                            {
                                _uiBridge.ShowString("Wrong choice. Try again: ");
                            }
                            switch (guestchoice)
                            {
                                case 1:
                                    _uiBridge.Clear();
                                    _uiBridge.ShowString(_shop.ShowGoods());
                                    _uiBridge.ShowString("Enter goods id: ");
                                    int goodsid;
                                    while (!int.TryParse(_uiBridge.GetString(), out goodsid) || _storageBase.FindGoods(new CGoodsId(goodsid)) == null)
                                    {
                                        _uiBridge.ShowString("Wrong id. Try again: ");
                                    }
                                    _uiBridge.ShowString("Enter number: ");
                                    int numgoods = 0;
                                    while (!int.TryParse(_uiBridge.GetString(), out numgoods))
                                    {
                                        _uiBridge.ShowString("Wrong number. Try again: ");
                                    }
                                    guest.AddGoodsToBasket(new CGoodsId(goodsid), numgoods);
                                    break;
                                case 2:
                                    _uiBridge.Clear();
                                    exit = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 2:
                        _uiBridge.Clear();
                        _uiBridge.ShowString(guest.ShowBasket(_shop));
                        _uiBridge.ShowString("\n" +
                                          "1. Make an order\n" +
                                          "2. Clean the basket\n" +
                                          "3. Back\n");
                        while (!int.TryParse(_uiBridge.GetString(), out guestchoice) || guestchoice > 3 || guestchoice < 1)
                        {
                            _uiBridge.ShowString("Wrong choice. Try again: ");
                        }
                        _uiBridge.Clear();
                        switch (guestchoice)
                        {
                            case 1:
                                _uiBridge.ShowString("Do u want deliveries?\n" +
                                                  "1. Yes\n" +
                                                  "2. No\n");
                                while (!int.TryParse(_uiBridge.GetString(), out guestchoice) || guestchoice > 2 || guestchoice < 1)
                                {
                                    _uiBridge.ShowString("Wrong choice. Try again: ");
                                }
                                COrder gorder = guest.FormAnOrder(guestchoice == 1 ? true : false);
                                _uiBridge.Clear();
                                _uiBridge.ShowString("Your order:\n");
                                _uiBridge.ShowString(gorder.ToString());
                                _uiBridge.ShowString("\nConfirm?\n1.Yes\n2.No");
                                while (!int.TryParse(_uiBridge.GetString(), out guestchoice) || guestchoice > 2 || guestchoice < 1)
                                {
                                    _uiBridge.ShowString("Wrong choice. Try again: ");
                                }
                                if (guestchoice == 1)
                                {
                                    _shop.ConfirmOrder(gorder);
                                }
                                guest.CleanBasket();
                                _uiBridge.Clear();
                                break;
                            case 2:
                                guest.CleanBasket();
                                _uiBridge.Clear();
                                break;
                            default:
                                break;
                        }
                        break;
                    case 3:
                        return CLogIn.LogIn(_userBase, _uiBridge);
                    case 4:
                        prexit = true;
                        _uiBridge.Clear();
                        break;
                    default:
                        break;
                }
            }
            return null;
        }

        protected void ShowClientPage(CAuthorizedUser user)
        {
            if (!(user is CClient))
                throw new Exception("Client page accessed only for clients");

            bool prexit = false;
            while (!prexit)
            {
                _uiBridge.Clear();
                _uiBridge.ShowString("1. Watch goods\n" +
                                  "2. Go to basket\n" +
                                  "3. Main menu");
                int clientchoice = 0;
                while (!int.TryParse(_uiBridge.GetString(), out clientchoice) | clientchoice > 3 | clientchoice < 1)
                {
                    _uiBridge.ShowString("Wrong choice. Try again: ");
                }
                switch (clientchoice)
                {
                    case 1:
                        bool exit = false;
                        while (!exit)
                        {
                            _uiBridge.Clear();
                            _uiBridge.ShowString(_shop.ShowGoods());
                            _uiBridge.ShowString("\n" +
                                              "1. AddGoodsToBasket\n" +
                                              "2. Back\n");
                            while (!int.TryParse(_uiBridge.GetString(), out clientchoice) || clientchoice > 2 || clientchoice < 1)
                            {
                                _uiBridge.ShowString("Wrong choice. Try again: ");
                            }
                            switch (clientchoice)
                            {
                                case 1:
                                    _uiBridge.Clear();
                                    _uiBridge.ShowString(_shop.ShowGoods());
                                    _uiBridge.ShowString("Enter goods id: ");
                                    int goodsid;
                                    while (!int.TryParse(_uiBridge.GetString(), out goodsid) || _storageBase.FindGoods(new CGoodsId(goodsid)) == null)
                                    {
                                        _uiBridge.ShowString("Wrong id. Try again: ");
                                    }
                                    _uiBridge.ShowString("Enter number: ");
                                    int numgoods = 0;
                                    while (!int.TryParse(_uiBridge.GetString(), out numgoods))
                                    {
                                        _uiBridge.ShowString("Wrong number. Try again: ");
                                    }
                                    (user as CClient).AddGoodsToBasket(new CGoodsId(goodsid), numgoods);
                                    break;
                                case 2:
                                    _uiBridge.Clear();
                                    exit = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 2:
                        _uiBridge.Clear();
                        _uiBridge.ShowString((user as CClient).ShowBasket(_shop));
                        _uiBridge.ShowString("\n" +
                                          "1. Make an order\n" +
                                          "2. Clean the basket\n" +
                                          "3. Back\n");
                        while (!int.TryParse(_uiBridge.GetString(), out clientchoice) || clientchoice > 3 || clientchoice < 1)
                        {
                            _uiBridge.ShowString("Wrong choice. Try again: ");
                        }
                        _uiBridge.Clear();
                        switch (clientchoice)
                        {
                            case 1:
                                _uiBridge.ShowString("Do u want deliveries?\n" +
                                                  "1. Yes\n" +
                                                  "2. No\n");
                                while (!int.TryParse(_uiBridge.GetString(), out clientchoice) || clientchoice > 2 || clientchoice < 1)
                                {
                                    _uiBridge.ShowString("Wrong choice. Try again: ");
                                }
                                _shop.ConfirmOrder((user as CClient).FormAnOrder(clientchoice == 1 ? true : false));

                                _orderBase.FillFile(_orderfilepath);
                                _storageBase.FillFile(_storagefilepath);

                                _uiBridge.Clear();
                                break;
                            case 2:
                                (user as CClient).CleanBasket();
                                _uiBridge.Clear();
                                break;
                            default:
                                break;
                        }
                        break;
                    case 3:
                        prexit = true;
                        _uiBridge.Clear();
                        break;
                    default:
                        break;
                }


            }
        }

        protected void ShowAdminPage(CAuthorizedUser user)
        {
            if (!(user is CAdmin))
                throw new Exception("Admin page accessed only for admins");

            bool prexit = false;
            while (!prexit)
            {
                _uiBridge.Clear();
                _uiBridge.ShowString("1. Watch goods\n" +
                                  "2. Main menu\n");
                int clientchoice = 0;
                while (!int.TryParse(_uiBridge.GetString(), out clientchoice) | clientchoice > 2 | clientchoice < 1)
                {
                    _uiBridge.ShowString("Wrong choice. Try again: ");
                }
                switch (clientchoice)
                {
                    case 1:

                        bool cycle = true;
                        while (cycle)
                        {
                            _uiBridge.Clear();
                            _uiBridge.ShowString((user as CAdmin).WatchStorage(_storageBase));
                            _uiBridge.ShowString("1. Add goods\n" +
                                              "2. Delete goods\n" +
                                              "3. Back\n");
                            clientchoice = 0;
                            while (!int.TryParse(_uiBridge.GetString(), out clientchoice) | clientchoice > 3 | clientchoice < 1)
                            {
                                _uiBridge.ShowString("Wrong choice. Try again: ");
                            }

                            switch (clientchoice)
                            {
                                case 1:

                                    _uiBridge.Clear();
                                    _uiBridge.ShowString((user as CAdmin).WatchStorage(_storageBase));
                                    string name = "";
                                    _uiBridge.ShowString("Enter goods name: ");
                                    name = _uiBridge.GetString();
                                    _uiBridge.ShowString("Enter goods id: ");
                                    int goodsid = 0;
                                    while (!int.TryParse(_uiBridge.GetString(), out goodsid) || _storageBase.IsContainGoods(new CGoodsId(goodsid)))
                                    {
                                        _uiBridge.ShowString("Id is exist. Try again: ");
                                    }
                                    _uiBridge.ShowString("Enter goods price: ");
                                    double price;
                                    while (!double.TryParse(_uiBridge.GetString(), out price))
                                    {
                                        _uiBridge.ShowString("Wrong price. Try again: ");
                                    }
                                    _uiBridge.ShowString("Enter goods made date. Format [dd.mm.yyyy]: ");
                                    string made = _uiBridge.GetString();
                                    while (!DateTime.TryParse(made, out DateTime date))
                                    {
                                        _uiBridge.ShowString("Wrong date. Try again: ");
                                        made = _uiBridge.GetString();
                                    }
                                    _uiBridge.ShowString("Enter goods type.\n" +
                                                      "1. Product\n" +
                                                      "2. Household goods");
                                    while (!int.TryParse(_uiBridge.GetString(), out clientchoice) || clientchoice < 1 || clientchoice > 2)
                                    {
                                        _uiBridge.ShowString("Id is exist. Try again: ");
                                    }
                                    if (clientchoice == 1)
                                    {
                                        int expd;
                                        _uiBridge.ShowString("Enter expiration days: ");
                                        while (!int.TryParse(_uiBridge.GetString(), out expd) || expd < 1)
                                        {
                                            _uiBridge.ShowString("Wrong expiration days. Try again: ");
                                        }
                                        int num;
                                        _uiBridge.ShowString("Enter number of goods: ");
                                        while (!int.TryParse(_uiBridge.GetString(), out num) || num < 1)
                                        {
                                            _uiBridge.ShowString("Wrong number of goods. Try again: ");
                                        }
                                        (user as CAdmin).AddGoodsToStorage(new CProduct(name, new CGoodsId(goodsid), price, made, expd), num, _storageBase);
                                    }
                                    if (clientchoice == 2)
                                    {
                                        double vol;
                                        _uiBridge.ShowString("Enter volume: ");
                                        while (!double.TryParse(_uiBridge.GetString(), out vol) || vol < 0.01)
                                        {
                                            _uiBridge.ShowString("Wrong expiration days. Try again: ");
                                        }
                                        int num;
                                        _uiBridge.ShowString("Enter number of goods: ");
                                        while (!int.TryParse(_uiBridge.GetString(), out num) || num < 1)
                                        {
                                            _uiBridge.ShowString("Wrong number of goods. Try again: ");
                                        }
                                        (user as CAdmin).AddGoodsToStorage(new CHousehold_Goods(name, new CGoodsId(goodsid), price, made, vol), num, _storageBase);
                                    }
                                    _storageBase.FillFile(_storagefilepath);
                                    break;
                                case 2:
                                    _uiBridge.Clear();
                                    _uiBridge.ShowString((user as CAdmin).WatchStorage(_storageBase));
                                    _uiBridge.ShowString("Enter goods id: ");
                                    int id = 0;
                                    while (!int.TryParse(_uiBridge.GetString(), out id) || !_storageBase.IsContainGoods(new CGoodsId(id)))
                                    {
                                        _uiBridge.ShowString("Wrong Id or is not exist. Try again: ");
                                    }
                                    (user as CAdmin).DeleteGoodsFromStorage(new CGoodsId(id), _storageBase);
                                    _storageBase.FillFile(_storagefilepath);
                                    break;
                                default:
                                    cycle = false;
                                    break;
                            }
                        }
                        break;
                    default:
                        prexit = true;
                        _uiBridge.Clear();
                        break;
                }
            }

        }

        protected void ShowModerPage(CAuthorizedUser user)
        {
            bool prexit = false;
            while (!prexit)
            {
                _uiBridge.Clear();
                _uiBridge.ShowString("1. Watch orders\n" +
                                    "2. Watch promos\n" +
                                    "3. Main menu");
                int clientchoice;
                while (!int.TryParse(_uiBridge.GetString(), out clientchoice) | clientchoice > 3 | clientchoice < 1)
                {
                    _uiBridge.ShowString("Wrong choice. Try again: ");
                }
                switch (clientchoice)
                {
                    case 1:
                        bool cycle = true;
                        while (cycle)
                        {
                            _uiBridge.Clear();
                            _uiBridge.ShowString((user as CModerator).WatchOrders(_orderBase));
                            _uiBridge.ShowString("1. DeleteOrder\n" +
                                                "2. Back");

                            while (!int.TryParse(_uiBridge.GetString(), out clientchoice) | clientchoice > 2 | clientchoice < 1)
                            {
                                _uiBridge.ShowString("Wrong choice. Try again: ");
                            }
                            switch (clientchoice)
                            {
                                case 1:
                                    if (_orderBase.Length < 1)
                                    {
                                        _uiBridge.Clear();
                                        _uiBridge.ShowString("We havn't orders ;)\n");
                                        break;
                                    }
                                    _uiBridge.Clear();
                                    _uiBridge.ShowString((user as CModerator).WatchOrders(_orderBase));
                                    _uiBridge.ShowString("Write number of order: ");
                                    while (!int.TryParse(_uiBridge.GetString(), out clientchoice) | clientchoice > _orderBase.Length | clientchoice < 1)
                                    {
                                        _uiBridge.ShowString("Wrong choice. Try again: ");
                                    }
                                    (user as CModerator).DeleteOrder(_orderBase[clientchoice - 1], _orderBase);
                                    _orderBase.FillFile(_orderfilepath);
                                    break;
                                default:
                                    cycle = false;
                                    _uiBridge.Clear();
                                    break;
                            }
                        }
                        break;
                    case 2:
                        bool cycle1 = true;
                        while (cycle1)
                        {
                            _uiBridge.Clear();
                            _uiBridge.ShowString((user as CModerator).WatchPromos(_promoBase));
                            _uiBridge.ShowString("1. Add promo\n" +
                                                "2. Delete promo\n" +
                                                "3. Back\n");
                            while (!int.TryParse(_uiBridge.GetString(), out clientchoice) | clientchoice > 3 | clientchoice < 1)
                            {
                                _uiBridge.ShowString("Wrong choice. Try again: ");
                            }
                            switch (clientchoice)
                            {
                                case 1:
                                    _uiBridge.Clear();
                                    _uiBridge.ShowString((user as CModerator).WatchStorage(_storageBase));
                                    _uiBridge.ShowString("Enter goods id for sale: ");
                                    while (!int.TryParse(_uiBridge.GetString(), out clientchoice) || _storageBase.FindStorage(new CGoodsId(clientchoice)) == null)
                                    {
                                        _uiBridge.ShowString("Wrong id. Try again: ");
                                    }
                                    int interest = 0;
                                    _uiBridge.ShowString("Enter interest: ");
                                    while (!int.TryParse(_uiBridge.GetString(), out interest) || interest > 99 || interest < 1)
                                    {
                                        _uiBridge.ShowString("Wrong interest. Try again: ");
                                    }
                                    _promoBase.AddPromo(new CPromo(interest, new CGoodsId(clientchoice)), _storageBase);
                                    _promoBase.FillFile(_promofilepath);
                                    break;
                                case 2:
                                    _uiBridge.Clear();
                                    _uiBridge.ShowString((user as CModerator).WatchPromos(_promoBase));
                                    _uiBridge.ShowString("Enter goods id for delete: ");
                                    while (!int.TryParse(_uiBridge.GetString(), out clientchoice) || !_promoBase.IsContain(new CPromo(id: new CGoodsId(clientchoice))))
                                    {
                                        _uiBridge.ShowString("Wrong id. Try again: ");
                                    }
                                    _promoBase.DeletePromo(new CPromo(id: new CGoodsId(clientchoice)), _storageBase);
                                    _promoBase.FillFile(_promofilepath);
                                    break;
                                default:
                                    cycle1 = false;
                                    _uiBridge.Clear();
                                    break;
                            }
                        }
                        break;
                    case 3:
                        _uiBridge.Clear();
                        prexit = true;
                        break;
                }
            }
        }
    }
}
