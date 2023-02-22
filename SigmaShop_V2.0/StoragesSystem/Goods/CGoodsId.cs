namespace SigmaShop.StoragesSystem.Goods
{
    class CGoodsId
    {
        protected int _id;
        
        public int Id 
        {
            get { return _id; }
            set {
                //перевірка бази даних складів
                _id = value; }
        }

        public CGoodsId(int id = 0) 
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (obj is CGoodsId)
                return Id.Equals((obj as CGoodsId).Id);
            return false;
        }

        public override int GetHashCode()
        {
            return _id;
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
