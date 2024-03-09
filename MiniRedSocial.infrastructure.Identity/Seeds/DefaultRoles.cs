using Microsoft.AspNetCore.Identity;
using MiniRedSocial.Core.Application.Enums;
using MiniRedSocial.infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.BasicUser.ToString()));
        }
    }
}
