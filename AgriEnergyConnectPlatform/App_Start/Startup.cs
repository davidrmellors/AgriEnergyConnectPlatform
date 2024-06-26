﻿using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.Owin;
using Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartup(typeof(AgriEnergyConnectPlatform.App_Start.Startup))]

namespace AgriEnergyConnectPlatform.App_Start
{
    /// <summary>
    /// Startup class for the application.
    /// </summary>
    public class Startup
    {
//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public void Configuration(IAppBuilder app)
        {
            // Configure the db context, user manager, and sign-in manager to use a single instance per request
            app.CreatePerOwinContext(() => new Data.AgriConnectContext());
            app.CreatePerOwinContext<UserManager<User>>((options, context) =>
                UserManagerFactory(context.Get<Data.AgriConnectContext>()));

            app.CreatePerOwinContext<SignInManager<User, string>>(
                (options, context) => new SignInManager<User, string>(
                    context.Get<UserManager<User>>(),
                    context.Authentication));

            app.CreatePerOwinContext<RoleManager<IdentityRole>>((options, context) =>
            new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context.Get<Data.AgriConnectContext>())));

            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });

            ConfigureRoles(app);
        }

        /// <summary>
        /// Factory method for creating a UserManager.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <returns>A UserManager for users.</returns>
        private UserManager<User> UserManagerFactory(Data.AgriConnectContext context)
        {
            var manager = new UserManager<User>(new UserStore<User>(context));
            // Optionally configure your user manager here
            return manager;
        }

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Configures the roles for the application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        private void ConfigureRoles(IAppBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var roleManager = context.Get<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    var role = new IdentityRole("Admin");
                    await roleManager.CreateAsync(role);
                }
                if (!await roleManager.RoleExistsAsync("Employee"))
                {
                    var role = new IdentityRole("Employee");
                    await roleManager.CreateAsync(role);
                }
                if (!await roleManager.RoleExistsAsync("Farmer"))
                {
                    var role = new IdentityRole("Farmer");
                    await roleManager.CreateAsync(role);
                }
                // Add other roles as necessary

                await next();
            });
        }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//