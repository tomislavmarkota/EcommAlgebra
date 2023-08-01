using Microsoft.AspNetCore.Identity;

namespace EcommAlgebra.Data
{
    public class ApplicationUserDbInitializer
    {

        public static void SeedUsers(UserManager<ApplicationUser> userManager) {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                FirstName = "Admin",
                LastName = "Admin"
            };

            var result = userManager.CreateAsync(user, "password").Result;

            if(result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
}
