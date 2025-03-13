using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using TestRira.Application;
using TestRira.Core.interfaces;
using TestRira.Data;

var builder = WebApplication.CreateBuilder(args);
ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<dbContexts>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUsersService, UsersService>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
public class Startup
{
    public IConfiguration Configuration { get; set; }
    public Startup(IConfiguration configurations)
    {
        Configuration = configurations;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<dbContexts>(op => op.UseSqlServer(Configuration.GetConnectionString("TestRira")));
        services.AddScoped<IUsersService, UsersService>();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // پیکربندی میانه‌افزارها
    }
}



