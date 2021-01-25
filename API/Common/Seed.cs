using API.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Common
{
    public class Seed
    {
        public UserManager<ApplicationUser> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }

        public Seed(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await CreateRolesAsync();
        }

        public async Task CreateRolesAsync()
        {
            var roles = Enum.GetValues(typeof(PersonType)).Cast<PersonType>();

            foreach (var role in roles)
            {
                bool exists = await RoleManager.RoleExistsAsync(role.GetPersonTypeName());
                if (!exists)
                {
                    await RoleManager.CreateAsync(new IdentityRole(role.GetPersonTypeRole()));
                }
            }
        }
    }
}
