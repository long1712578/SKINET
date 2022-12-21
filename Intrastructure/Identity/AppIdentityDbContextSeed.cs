using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Long",
                    Email = "long@gmail.com",
                    UserName = "longpham",
                    Address = new Address()
                    {
                        FirstName = "pham",
                        LastName = "long",
                        Street = "19/5A",
                        City = "HCM",
                        ZipCode = "7205",
                        State = "HL"
                    }
                };
                await userManager.CreateAsync(user, "Abc123!@#");
            }
        }
    }
}
