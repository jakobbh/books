using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication7.Data.Authorization;
using WebApplication7.Data.Interfaces;
using WebApplication7.Repository;
using static WebApplication7.Data.Authorization.AuthorDetailsRequirements;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<WebApplication7.Data.ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddAuthentication("AuthCookie")
    .AddCookie("AuthCookie", options =>
    {
        options.Cookie.Name = "AuthCookie";
        options.ExpireTimeSpan = TimeSpan.FromSeconds(20);
        
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustBeAuthor",
    policy => policy
    .RequireClaim(ClaimTypes.Role, "Author")
    .Requirements.Add(new AuthorDetailsRequirements(2))
    );
});

builder.Services.AddSingleton<IAuthorizationHandler, AuthorDetailsRequirementsHandler>();

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
