using System;
using SigmaShop.StoragesSystem.Goods;
using SigmaShop.StoragesSystem;


namespace SigmaShop.PromoSystem
{
    class CPromo
    {

        protected CGoodsId _id;
        public CGoodsId Id
        {
            get { return _id; }
            set
            {
                //ТУТ .перевірка на існування в базі 
                _id = value;
            }
        }

        protected int _interest;//1-99
        public int Interest
        {
            get { return _interest; }
            set
            {
                if (value < 1 || value > 99)
                    throw new ArgumentOutOfRangeException("Interest less than 1% or biggest than 99%");
                _interest = value;
            }
        }

        public CPromo(int interest = 1, CGoodsId id = null)
        {
            Interest = interest;
            Id = id;
        }

        public override string ToString()
        {
            return $"Id:{Id} Interest:{Interest}";
        }

        public override bool Equals(object obj)
        {
            if (obj is CPromo)
                return Id.Equals((obj as CPromo).Id);
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
