using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(Role model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole { Name = model.RoleName };

                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "roles");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        /// <summary>
        ///  Assigning Roles.
        /// </summary>
        /// <returns></returns>
        public IActionResult AssignRole()
        {
            ViewBag.Users = new SelectList(userManager.Users, "UserName", "UserName");
            ViewBag.Roles = new SelectList(roleManager.Roles, "Name", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(string roleName, string userName)
        {

            if (roleName != null && userName != null)
            {
                var role = roleManager.Roles.FirstOrDefault(x => x.Name == roleName);
                var user = await userManager.FindByNameAsync(userName);
                if (role != null && user != null)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                    return RedirectToAction("Index", "Roles");

                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ManageRoles(string id)
        {
            if (id != null)
            {
                ViewBag.RoleName = roleManager.Roles.FirstOrDefault(x => x.Name == id);
                var users = await userManager.GetUsersInRoleAsync(id);
                ViewBag.Users = new SelectList(users, "UserName", "UserName");
                return View();

            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string roleName, string userName)
        {

            if (roleName != null && userName != null)
            {
                var role = roleManager.Roles.FirstOrDefault(x => x.Name == roleName);
                var user = await userManager.FindByNameAsync(userName);
                if (role != null && user != null)
                {
                    await userManager.RemoveFromRoleAsync(user, roleName);
                    return RedirectToAction("ManageRoles", "Roles", new { id = roleName });

                }
            }
            return RedirectToAction("Index", "Roles");
        }

        [HttpGet]
        public IActionResult DeleteRole(string id)
        {
            if (id != null)
            {
                ViewBag.Role = roleManager.Roles.FirstOrDefault(x => x.Id == id);
                return View();

            }

            return View("Not Found");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRoleConfirmed(string roleId)
        {
            if (roleId != null)
            {
                IdentityRole identityRole = await roleManager.FindByIdAsync(roleId);
                IdentityResult result = await roleManager.DeleteAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "roles");
                }

            }

            return View("Not Found");
        }
    }
}
