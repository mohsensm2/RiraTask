using Microsoft.EntityFrameworkCore;
using TestRira.Application;
using TestRira.Core.interfaces;
using TestRira.Data;
namespace TestRira
{
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
}
