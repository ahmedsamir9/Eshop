using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjectMVC.Models
{
    public class ShopDBContext: IdentityDbContext
    {
            public DbSet<Client> clients { get; set; }
            public DbSet<Category> categories { get; set; }
            public DbSet<Order> orders { get; set; }
            public DbSet<Product> products { get; set; }
            public DbSet<Cart> carts { get; set; }
            public DbSet<OrderProduct> OrderProducts { get; set; }
            public ShopDBContext(DbContextOptions<ShopDBContext> options) : base(options)
            {

            }


            protected override void OnModelCreating(ModelBuilder builder)
            {
                builder.Entity<Cart>()
                   .HasOne(Tc => Tc.Client)
                   .WithMany(t => t.carts)
                   .HasForeignKey(bc => bc.ClientId);


                builder.Entity<Cart>()
              .HasOne(Tc => Tc.Product)
              .WithMany(c => c.carts)
              .HasForeignKey(bc => bc.ProductId);
                base.OnModelCreating(builder);

                builder.Entity<OrderProduct>()
                   .HasOne(Tc => Tc.Order)
                   .WithMany(t => t.orderProducts)
                   .HasForeignKey(bc => bc.OrderId);


                builder.Entity<OrderProduct>()
              .HasOne(Tc => Tc.Product)
              .WithMany(c => c.orderProducts)
              .HasForeignKey(bc => bc.ProductId);
                base.OnModelCreating(builder);
            }
        }
    }
