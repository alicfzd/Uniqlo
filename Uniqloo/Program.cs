using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Uniqloo.DataAccess;
using Uniqloo.Extentions;
using Uniqloo.Models;

namespace Uniqloo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<UniqloDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSql"));
            });
            

            builder.Services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Lockout.MaxFailedAccessAttempts = 3;
                //opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(int.MaxValue);
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<UniqloDbContext>();
            builder.Services.ConfigureApplicationCookie(x =>
            {
                x.LoginPath = "/login";
                x.AccessDeniedPath = "/Home/AccessDenied";
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            app.UseUserSeed();
            app.MapControllerRoute(
                name: "login",
                pattern: "login", new
                {
                    Controller = "Account",
                    Action = "Login"
                });
            app.MapControllerRoute(
                name: "register",
                pattern: "register", new
                {
                    Controller = "Account",
                    Action = "Register"
                });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "area",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

            app.Run();

        }
    }
}

