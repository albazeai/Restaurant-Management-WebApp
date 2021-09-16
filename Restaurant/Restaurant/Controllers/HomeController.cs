using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            this.roleManager = roleManager;
            this.userManager = userManager;
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
                    return View();

                }
            }
            catch (Exception)
            {

                //throw;
            }
            return View();

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
    }
}
