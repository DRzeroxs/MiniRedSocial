using MiniRedSocial.Core.Application;
using MiniRedSocial.Infrastructure.Persistence;
using MiniRedSocial.Middlewares;
using MiniRedSocial.Infrastructure.Shared;
using MiniRedSocial.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using MiniRedSocial.infrastructure.Identity.Entities;
using MiniRedSocial.infrastructure.Identity.Seeds;

namespace MiniRedSocial
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();
            builder.Services.AddPersistenceInfrastructure(builder.Configuration);
            builder.Services.AddIdentityInfrastructure(builder.Configuration);
            builder.Services.AddSharedInfrastructure(builder.Configuration);
            builder.Services.AddScoped<LoginAuthorize>();
            builder.Services.AddApplicationLayer();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddTransient<ValidateUserSession, ValidateUserSession>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {

                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    await DefaultRoles.SeedAsync(userManager, roleManager);
                    await DefaultBasicUser.SeedAsync(userManager, roleManager);

                }catch (Exception)
                {

                }
            }


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
