using System.Collections.Generic;

namespace ShopApp
{
    public class Basket : Entity
    {
        public User User { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}