using System;

namespace SigmaShop.StoragesSystem.Goods
{
    class CHousehold_Goods: CGoods
    {
        protected double _volume;
        public double Volume
        {
            get { return _volume; }
            set
            {
                if (value <= 0)
                    throw new Exception("Volume can't be zero or less than zero!");
                _volume = value;
            }
        }

        public CHousehold_Goods(string name, CGoodsId id, double price, string made, double volume) : base(name, id, price, made)
        {
            Volume = volume;
        }

        public override string ToString()
        {
            return String.Format("{0,11} ", "Household:") + base.ToString() + " " + String.Format("{0,4} ", (Math.Round(Volume, 2)).ToString()); 
        }
    }
}
