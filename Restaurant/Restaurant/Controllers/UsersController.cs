using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var users = userManager.Users; 
            return View(users);

        }
        //public IActionResult Details(string id)
        //{
        //    var user = userManager.Users.FirstOrDefault(x => x.Id == id);
        //    return View(user);

        //}

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,BirthDate,Gender,Email,PhoneNumber,StreetNumber,PostalCode,Address,City,Province,Notes,DateCreated")] ApplicationUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user2 = await userManager.FindByIdAsync(id);
                    user2.FirstName = user.FirstName;
                    user2.LastName = user.LastName;
                    user2.BirthDate = user.BirthDate;
                    user2.Gender = user.Gender;
                    user2.Email = user.Email;
                    user2.PhoneNumber = user.PhoneNumber;
                    user2.StreetNumber = user.StreetNumber;
                    user2.PostalCode = user.PostalCode;
                    user2.Address = user.Address;
                    user2.City = user.City;
                    user2.Province = user.Province;
                    user2.Notes = user.Notes;
                    await userManager.UpdateAsync(user2);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            await userManager.DeleteAsync(user);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //get
        public async Task<IActionResult> SetNewPassword(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SetNewPassword(string id, string password)
        {

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            if (id == null || password == null)
            {
                ViewBag.ErrorMessage = "The Password must be at least 8 and at max 100 characters long. \nPasswords must have at least one digit ('0'-'9'). \nPasswords must have at least one lowercase('a' - 'z'). \nPasswords must have at least one uppercase('A' - 'Z').";
                return View(user);
            }

            var addPasswordResult1 = await userManager.RemovePasswordAsync(user);
            var addPasswordResult = await userManager.AddPasswordAsync(user, password);
            if (addPasswordResult.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ErrorMessage = "The Password must be at least 8 and at max 100 characters long. \nPasswords must have at least one digit ('0'-'9'). \nPasswords must have at least one lowercase('a' - 'z'). \nPasswords must have at least one uppercase('A' - 'Z').";

            return View(user);
        }


        private bool UserExists(string id)
        {
            return userManager.Users.Any(e => e.Id == id);
        }

    }
}
