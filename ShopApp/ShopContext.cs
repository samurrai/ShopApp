namespace ShopApp
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ShopContext : DbContext
    {
        public ShopContext()
            : base("name=ShopContext")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Basket> Baskets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasRequired(user => user.Basket).WithRequiredDependent(basket => basket.User);

            modelBuilder.Entity<Basket>().HasMany(basket => basket.Products).WithMany(products => products.Baskets);

            base.OnModelCreating(modelBuilder);
        }
    }
}