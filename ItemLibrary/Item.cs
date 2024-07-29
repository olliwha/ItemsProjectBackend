namespace ItemLibrary
{
    public class Item
    {
        private int _id;
        private string? _name;
        private decimal _priceWithoutMoms;
        private const decimal Moms = 0.25m;


        public Item(int id, string name, decimal price)
        {
            _id = id;
            _name = name;
            _priceWithoutMoms = price;
        }
        public int Id => _id;
        public string? Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public decimal PriceWithoutMoms
        {
            get { return _priceWithoutMoms; }
            set { _priceWithoutMoms = value; }
        }
        public decimal Price => _priceWithoutMoms + (_priceWithoutMoms * Moms);


        public void ValidateName()
        {
            if (Name == null) throw new ArgumentNullException();
            if (Name.Length < 2) throw new ArgumentException();
        }
        public void ValidatePriceWithoutMoms()
        {

            if (PriceWithoutMoms < 1 || PriceWithoutMoms > 100000) throw new ArgumentOutOfRangeException();
        }
        public void ValidatePrice()
        {
            decimal priceNoMoms = PriceWithoutMoms * 1.25m;
            if (Price != priceNoMoms) throw new ArgumentOutOfRangeException();
        }
        public void Validate()
        {
            ValidateName();
            ValidatePriceWithoutMoms();
            ValidatePrice();
        }
        public override string ToString()
        {
            return $"{Id}, {Name}, {Price}kr.";
        }

        /// <summary>
        /// indicates whether the current instance is equal to a specified object or not.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Item)) return false;

            Item item = (Item)obj;
            if (item.Name != Name) return false;
            if (item.PriceWithoutMoms != PriceWithoutMoms) return false;
            if (item.Id != Id) return false;
            return true;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
