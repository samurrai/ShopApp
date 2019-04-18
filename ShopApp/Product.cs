using System.Collections.Generic;

namespace ShopApp
{
    public class Product : Entity
    {
        public ICollection<Basket> Baskets { get; set; } = new List<Basket>();
    }
}