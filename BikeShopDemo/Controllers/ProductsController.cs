using BikeShopDemo.Data;
using BikeShopDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BikeShopDemo.Controllers
{
    public class ProductsController : Controller
    {
        private readonly BikeShopContext _context;
        public ProductsController(BikeShopContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Index(int currentpage = 1, int amountPerPage = 10)
        {
            var amountToSkip = (currentpage - 1) * amountPerPage;
            var products = _context.Products.Skip(amountToSkip).Take(amountPerPage).ToList();
            return View(products);
        }
        [Authorize]
        public IActionResult Detail(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            return View(product);
        }
        [Authorize (Roles="Administrator")]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Products.Add(product);
                    _context.SaveChanges();
                    return RedirectToAction("Detail", new { id = product.Id });
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return View(product);
        }
    }
}
