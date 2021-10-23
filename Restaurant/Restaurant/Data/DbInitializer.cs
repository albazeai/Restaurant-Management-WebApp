using Restaurant.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Data
{
    public static class DbInitializer
    {
        public static async Task<int> SeedUsersAndRoles(IServiceProvider serviceProvider)
        {
            // create the database if it doesn't exist
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Check if roles already exist and exit if there are
            if (roleManager.Roles.Count() > 0)
                return 1;  

            // Seed roles
            int result = await SeedRoles(roleManager);
            if (result != 0)
                return 2;  

            // Check if users already exist and exit if there are
            if (userManager.Users.Count() > 0)
                return 3;  

            // Seed users
            result = await SeedUsers(userManager);
            if (result != 0)
                return 4;  

            return 0;
        }
        /// <summary>
        /// seed the initial migration with adding the main application roles.
        /// </summary>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        private static async Task<int> SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // Create Admin Role
            var result = await roleManager.CreateAsync(new IdentityRole("Admin"));
            if (!result.Succeeded)
                return 1;  

            // Create Cashier Role
            result = await roleManager.CreateAsync(new IdentityRole("Cashier"));
            if (!result.Succeeded)
                return 2;  
            
            // Create Kitchen Role
            result = await roleManager.CreateAsync(new IdentityRole("Kitchen"));
            if (!result.Succeeded)
                return 3;  

            return 0;
        }

        /// <summary>
        /// seed the initial migration with one admin account.
        /// </summary>
        /// <param name="userManager"></param>
        /// <returns></returns>
        private static async Task<int> SeedUsers(UserManager<ApplicationUser> userManager)
        {
            // Create Admin User
            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(adminUser, "Password!1");
            if (!result.Succeeded)
                return 1;  

            // Assign user to Admin role
            result = await userManager.AddToRoleAsync(adminUser, "Admin");
            if (!result.Succeeded)
                return 2;  

            return 0;
        }
    }
}
