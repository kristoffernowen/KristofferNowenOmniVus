using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewOmniVus.Data;
using NewOmniVus.Services;

// using NewOmniVus.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Sql")));
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Sql")));

builder.Services.AddDbContext<SecondDbContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("SecondSql")));

builder.Services.AddScoped<AddressManager>();
builder.Services.AddScoped<ProfileManager>();


builder.Services.AddIdentity<IdentityUser, IdentityRole>(x =>
{
    // x.SignIn.RequireConfirmedAccount = true;
    x.User.RequireUniqueEmail = true;
    x.Password.RequiredLength = 8;
    x.Password.RequireDigit = true;
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/authentication/signin";
    x.AccessDeniedPath = "/authentication/denied";
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
