﻿using Microsoft.AspNetCore.Identity;
using MiniRedSocial.Core.Application.Enums;
using MiniRedSocial.infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.infrastructure.Identity.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new();
            defaultUser.UserName = "basicUser";
            defaultUser.Email = "basicuser@email.com";
            defaultUser.Name = "Miguel";
            defaultUser.LastName = "Basic";
            defaultUser.ImageUrl = "----";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.BasicUser.ToString());
                }
            }
        }
    }
}
