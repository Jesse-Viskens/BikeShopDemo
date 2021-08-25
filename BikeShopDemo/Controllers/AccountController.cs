using BikeShopDemo.Data;
using BikeShopDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShopDemo.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly BikeShopContext _context;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, BikeShopContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> RegisterUser(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid == true)
            {
                var user = new IdentityUser { UserName = loginViewModel.UserName, PasswordHash = loginViewModel.Password, Email = loginViewModel.Email };
                var result = await _userManager.CreateAsync(user, loginViewModel.Password);

                if (result.Succeeded)
                {
                    var name = await  _userManager.FindByNameAsync(loginViewModel.UserName);
                    var loggedInUser = await _userManager.FindByNameAsync(name.UserName);
                    try
                    {
                        _context.ShoppingBags.Add(new ShoppingBag { Date = DateTime.Now, IdentityUserId = loggedInUser.Id });
                        _context.SaveChanges();
                    }
                    catch(Exception e)
                    {
                        throw new Exception(e.Message);
                    }

                    return View("Login");
                }
                else
                {
                    throw new Exception("Couldn't register this user");
                }
            }
            return View("register", loginViewModel);
        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Authenticate(LoginViewModel login)
        {
            if (ModelState.IsValid == true)
            {
                var user = await _userManager.FindByNameAsync(login.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, login.Password, true, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Products");
                    }
                }
            }
            return View("Login", login);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Route("/Account/AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
