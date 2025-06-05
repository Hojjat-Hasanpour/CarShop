using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyCarShop.Models.Configurations;
using MyCarShop.Models.Context;
using MyCarShop.Models.DomainModels;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();
builder.Services.AddSession(); // Active HttpContent
// Add services to the container.
builder.Services.AddControllersWithViews();
// Add Database Context
builder.Services.AddDbContext<ShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarShopCsDB")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//New google stuff
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
    });

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//    .AddEntityFrameworkStores<ShopContext>()
//    .AddDefaultTokenProviders();
builder.Services.AddDefaultIdentity<ApplicationUser>(
        options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
            options.Lockout.DefaultLockoutTimeSpan =
                TimeSpan.FromMinutes(7); // After 5 enter wrong password lockout account
        })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ShopContext>();
//.AddErrorDescriber<PersianIdentityErrorDescriber>();

builder.Services.AddRazorPages();
// Optional
//builder.Services.Configure<IdentityOptions>(options =>
//{
//    options.Password.RequireDigit = true;
//    options.Password.RequiredLength = 6;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = true;
//    options.Password.RequireLowercase = true;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    await IdentityConfig.CreateAdminUserAsync(scope.ServiceProvider);
}

app.MapStaticAssets();

//app.MapAreaControllerRoute(
//        name: "Admin",
//        areaName: "Admin",
//        pattern: "Admin/{controller=Home}/{action=Index}/{id?}")
//    .WithStaticAssets();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    ).WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}")
    .WithStaticAssets();

app.MapRazorPages();
app.Run();
