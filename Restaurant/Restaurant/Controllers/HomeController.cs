using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.Data;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{

    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            this.roleManager = roleManager;
            this.userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await userManager.GetUserAsync(User);
                if (user != null)
                {
                    var userRole = await userManager.GetRolesAsync(user);
                    if (userRole.Count() > 0)
                    {
                        foreach (var role in userRole)
                        {
                            if (role.Contains("Cashier"))
                            {
                                return RedirectToAction("Index", "Cashier");
                            }
                            else if (role.Contains("Kitchen"))
                            {
                                return RedirectToAction("Index", "Kitchen");
                            }
                        }
                    }
                    return View(await _context.Events.ToListAsync());

                }
            }
            catch (Exception)
            {

            }
            return View(await _context.Events.ToListAsync());

        }
       
        public async Task<IActionResult> Menu()
        {
            var database = _context.Foods.Include(f => f.Category);
            ViewBag.Drinks = await _context.Drinks.Include(f => f.Category).ToListAsync();
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View(await database.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Menu(string category)
        {
            //MenuFilterd
            var selected = _context.Categories.FirstOrDefault(c => c.CategoryId.ToString() == category);
            ViewBag.SelectedCategory = selected.CategoryName;
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName", category);

            if (!string.IsNullOrEmpty(category))
            {
                var filterFood = await _context.Foods.Where(i => i.CategoryId.ToString() == category).Include(f => f.Category).ToListAsync();
                var filterDrinks = await _context.Drinks.Where(i => i.CategoryId.ToString() == category).Include(f => f.Category).ToListAsync();

                if (filterFood.Count <= 0)
                {
                    ViewBag.Drinks = filterDrinks;
                    return View();
                }
                return View(filterFood);
                
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
