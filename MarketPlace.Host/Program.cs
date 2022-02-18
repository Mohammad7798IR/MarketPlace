using Infa.Data.Context;
using Infa.Domain.Models.Identity;
using Infa.IoC;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

IConfiguration _configuration = builder.Configuration;

#region Services

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();


#region RegisterService

RegisterService(builder.Services);

#endregion

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
           .AddEntityFrameworkStores<MarketPlaceDBContext>();

builder.Services.AddDbContext<MarketPlaceDBContext>(options =>
{
    options.UseSqlServer(_configuration.GetConnectionString("DefaultConnectionStrings"));
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.Cookie.Name = "marketplaceCookie";
    options.ExpireTimeSpan = TimeSpan.FromHours(5);
    options.LogoutPath = "/logout";
    options.LoginPath = "/login";
});





#endregion

var app = builder.Build();

#region MiddelWares

app.UseStatusCodePagesWithReExecute("/ErrorHandler/Error", "?StatusCode={0}");

app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "User",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
     name: "Admin",
     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
   );


    endpoints.MapControllerRoute(
      name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}"
      );
});



app.Run();


#endregion


#region IoC


void RegisterService(IServiceCollection services)
{
    ServiceContainer.RegisterServices(services);
}


#endregion