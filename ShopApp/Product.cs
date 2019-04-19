using System.Collections.Generic;

namespace ShopApp
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public ICollection<Basket> Baskets { get; set; } = new List<Basket>();
    }
}