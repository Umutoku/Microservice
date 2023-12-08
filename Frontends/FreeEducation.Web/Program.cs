using FluentValidation.AspNetCore;
using FreeEducation.Shared.Services;
using FreeEducation.Web.Handler;
using FreeEducation.Web.Models;
using FreeEducation.Web.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using FreeEducation.Web.Helpers;
using FreeEducation.Web.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddFluentValidation(x=>x
        .RegisterValidatorsFromAssemblyContaining<EducationCreateInputValidation>());
builder.Services.AddSingleton<PhotoHelper>();
builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddScoped<ClientCredentialTokenHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAccessTokenManagement();
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));
builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opts =>
{
    opts.LoginPath = "/Auth/SignIn";
    opts.ExpireTimeSpan = TimeSpan.FromDays(60);
    opts.SlidingExpiration = true;
    opts.Cookie.Name = "webcookie";
});
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddHttpClientServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
