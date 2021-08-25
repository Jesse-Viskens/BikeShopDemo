using BikeShopDemo.Data;
using BikeShopDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShopDemo.Controllers
{
    public class ShoppingBagsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly BikeShopContext _context;
        public ShoppingBagsController(UserManager<IdentityUser> userManager, BikeShopContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> AddProductToBag(int productId, int quantity)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var bag = await _context.ShoppingBags.FirstOrDefaultAsync(b => b.IdentityUserId == user.Id);

            try
            {
                _context.ShoppingItems.Add(new ShoppingItem { ShoppingBagId = bag.Id, ProductId = productId, Quantity = quantity });
                _context.SaveChanges();
                return RedirectToAction("GetBag", "ShoppingBags");
            }
            catch (Exception)
            {
                return RedirectToAction("Detail", "Products", new { id = productId });
            }

        }
        [Authorize]
        public async Task<IActionResult> GetBag()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var bag = await _context.ShoppingBags
                .Include(x => x.ShoppingItems)
                .ThenInclude(x =>x.Product)
                .Include(x => x.IdentityUser)
                .FirstOrDefaultAsync(b => b.IdentityUserId == user.Id);

            List<ShoppingItemViewModel> shoppingitems = new List<ShoppingItemViewModel>();
            foreach(var shopping in bag.ShoppingItems)
            {
                var shoppingItem = new ShoppingItemViewModel
                {
                    shoppingItems = shopping,
                    TotalPerProduct = shopping.Quantity * shopping.Product.Price
                };
                shoppingitems.Add(shoppingItem);
            }

            var shoppingbagVM = new ShoppingBagViewModel()
            {
                ShoppingItems = shoppingitems,
                Date = bag.Date,
                AbsoluteTotal = shoppingitems.Sum(x => x.TotalPerProduct),
                IdentityUser = bag.IdentityUser,
                Id = bag.Id,
                IdentityUserId = bag.IdentityUserId
            };
            return View(shoppingbagVM);
        }
    }
}
