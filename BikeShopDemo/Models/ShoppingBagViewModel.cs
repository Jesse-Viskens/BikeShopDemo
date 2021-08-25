using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShopDemo.Models
{
    public class ShoppingBagViewModel
    {
        public int Id { get; set; }
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public ICollection<ShoppingItemViewModel> ShoppingItems { get; set; }
        public DateTime Date { get; set; }
        public double AbsoluteTotal { get; set; }
    }
}
