using BikeShopDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BikeShopDemo.Data
{
    public class BikeShopContext : IdentityDbContext<IdentityUser>
    {
        public BikeShopContext(DbContextOptions<BikeShopContext>options): base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingBag> ShoppingBags { get; set; }
        public DbSet<ShoppingItem> ShoppingItems { get; set; }
    }
}
