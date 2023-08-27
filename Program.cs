using sellhandproduct.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;    
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using sellhandproduct.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MVCDemoDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MVCDemoConnectionString")));

builder.Services.AddDbContext<MVCDataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MVCDemoConnectionString")));

builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders()
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<MVCDemoDbContext>();




//builder.Services.AddDbContext<MVCDemoDbContext>(options => 
//options.UseSqlServer(builder.Configuration.GetConnectionString()));
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
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
