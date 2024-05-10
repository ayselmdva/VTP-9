using Microsoft.AspNetCore.Identity;
using VTP_9.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.User.RequireUniqueEmail = true;

    opt.Password.RequiredLength = 8;
    opt.Password.RequireNonAlphanumeric = false;
});


var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.Run();
