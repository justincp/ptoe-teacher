using Microsoft.EntityFrameworkCore;

namespace BlazingPizza
{
      public class PizzaStoreContext : DbContext
      {
            public PizzaStoreContext(
                DbContextOptions options) : base(options)
            {
            }
    
            public DbSet<Order> Orders { get; set; }
    
            public DbSet<Pizza> Pizzas { get; set; }

            public DbSet<GameResult> GameResults { get; set; }
    
            public DbSet<PizzaSpecial> Specials { get; set; }

            public DbSet<Topping> Toppings { get; set; }

            public DbSet<Stupid> Stupids { get; set; }
    
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
    
                // Configuring a many-to-many special -> topping relationship that is friendly for serialization
                modelBuilder.Entity<PizzaTopping>().HasKey(pst => new { pst.PizzaId, pst.ToppingId });
                modelBuilder.Entity<PizzaTopping>().HasOne<Pizza>().WithMany(ps => ps.Toppings);
                modelBuilder.Entity<PizzaTopping>().HasOne(pst => pst.Topping).WithMany();
                //modelBuilder.Entity<GameResult>().HasKey(pst => new { pst.CreatedTime});
                //modelBuilder.Entity<GameResult>().HasKey(pst => new { pst.GameId });
        }
      }
}