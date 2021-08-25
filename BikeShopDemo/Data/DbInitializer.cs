
using BikeShopDemo.Models;
using System.Collections.Generic;
using System.Linq;

namespace BikeShopDemo.Data
{
    public class DbInitializer
    {
        public static void Seed(BikeShopContext bikeShopContext)
        {
            bikeShopContext.Database.EnsureCreated();

            if (bikeShopContext.Products.Any())
            {
                return;
            }
            var products = new List<Product>()
            {
                new Product{Name="Felt IA16",Image="Felt.jpg",Price=1900.00},
                new Product{Name="Canyon Speedmax",Image="Canyon.png",Price=2600.00},
                new Product{Name="Ridley Dean",Image="Ridley.png",Price=2100.00},
                new Product{Name="BMC Timemachine",Image="BMC.png",Price=2900.00}
            };
            foreach(var bike in products)
            {
                bikeShopContext.Products.Add(bike);
            }
            bikeShopContext.SaveChanges();

        }
    }
}
