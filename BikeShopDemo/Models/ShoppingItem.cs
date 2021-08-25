using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShopDemo.Models
{
    public class ShoppingItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ShoppingBagId { get; set; }
        public ShoppingBag ShoppingBag { get; set; }
    }
}
