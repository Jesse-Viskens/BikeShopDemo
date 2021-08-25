using BikeShopDemo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShopDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static IWebHostEnvironment _webHostEnvironment;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var product = new Product();
            product.Image = GetRandomImageName();
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public static string GetRandomImageName()
        {
            var rootFolder = _webHostEnvironment.WebRootPath;
            DirectoryInfo dir = new DirectoryInfo(Path.Combine(rootFolder, "images"));
            FileInfo[] files = dir.GetFiles();
            Random random = new Random();
            int index = random.Next(0, files.Length);
            return files[index].Name;

        }
    }
}
