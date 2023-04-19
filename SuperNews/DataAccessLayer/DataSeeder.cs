using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using SuperNews.Abstract;
using SuperNews.UsersRoles;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SuperNews.Domains;

namespace SuperNews.DataAccessLayer
{
    public static class DataSeeder
    {
        public static void SeedNews(IServiceProvider provider)
        {
            var productRepository = provider
                .GetRequiredService<IRepository<News>>();

            if (productRepository.GetList().Count() > 0)
                return;

        

         
            
        }

        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.Users.Count() > 0) return;

            var user1 = new IdentityUser
            {
                UserName = "admin@ya.ru",
                Email = "admin@ya.ru",
                EmailConfirmed = true
            };
            IdentityResult userResult = userManager.CreateAsync(user1, "1Qwerty!").Result;
            if (userResult.Succeeded)
            {
                userResult = userManager.AddToRoleAsync(user1, AppRoles.Administrator).Result;
            }

            var user2 = new IdentityUser
            {
                UserName = "Moderat@ya.ru",
                Email = "Moderat@ya.ru",
                EmailConfirmed = true
            };
            userResult = userManager.CreateAsync(user2, "1Qwerty!").Result;
            if (userResult.Succeeded)
            {
                userResult = userManager.AddToRoleAsync(user2, AppRoles.Moderator).Result;
            }

            var user3 = new IdentityUser
            {
                UserName = "Guest@ya.ru",
                Email = "Guest@ya.ru",
                EmailConfirmed = true
            };
            userResult = userManager.CreateAsync(user3, "1Qwerty!").Result;
            if (userResult.Succeeded)
            {
                userResult = userManager.AddToRoleAsync(user3, AppRoles.Guest).Result;
            }

            var user4 = new IdentityUser
            {
                UserName = "Redactor@ya.ru",
                Email = "Redactor@ya.ru",
                EmailConfirmed = true
            };
            userResult = userManager.CreateAsync(user4, "1Qwerty!").Result;
            if (userResult.Succeeded)
            {
                userResult = userManager.AddToRoleAsync(user4, AppRoles.Redactor).Result;
            }


        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = new string[]
            {
                AppRoles.Administrator,
                AppRoles.Redactor,
                AppRoles.Moderator,
                AppRoles.Subscriber,
                AppRoles.Guest
            };
            if (roleManager.Roles.Count() > 0) return;

            foreach (var roleName in roleNames)
            {
                var role = new IdentityRole { Name = roleName };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        public static async Task AddPermissionClaim(
             this RoleManager<IdentityRole> roleManager,
             IdentityRole role,
             string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }
    }
}
