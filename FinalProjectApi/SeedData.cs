using FinalProjectApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FinalProjectApi
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // ≈‰‘«¡ Role Admin ≈–« ·„ Ìﬂ‰ „ÊÃÊœ
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // ≈‰‘«¡ √Ê· Admin ≈–« ·„ Ìﬂ‰ „ÊÃÊœ
            string adminUserName = "superadmin";
            string adminEmail = "admin@example.com";
            string adminPassword = "P@ssword123"; // «” »œ· »ﬂ·„… ”— ﬁÊÌ…

            var adminUser = await userManager.FindByNameAsync(adminUserName);
            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = adminUserName,
                    Email = adminEmail
                };

                var result = await userManager.CreateAsync(user, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
