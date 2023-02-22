using System;

namespace SigmaShop.StoragesSystem.Goods
{
    interface IProduct
    {
        public bool IsEndOfExpirationDays();
    }

    class CProduct : CGoods, IProduct
    {
        protected int _expdays;
        public int ExpirationDays
        {
            get { return _expdays; }
            set
            {
                if (value < 1)
                    throw new Exception("Expiration days can't be less than 1!");
                _expdays = value;
            }
        }

        public CProduct(string name, CGoodsId id, double price, string made, int expdays) : base(name, id, price, made)
        {
            ExpirationDays = expdays;
        }

        public override string ToString()
        {
            return String.Format("{0,11} ", "Product:") + base.ToString() + " " + String.Format("{0,4} ", ExpirationDays.ToString());
        }

        public bool IsEndOfExpirationDays()
        {
            DateTime date = DateTime.Parse(Made);
            date.AddDays(ExpirationDays);
            if (date < DateTime.Now)
                return true;
            return false;
        }
    }
}
