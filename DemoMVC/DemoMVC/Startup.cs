using DemoMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        // Constructor to inject IConfiguration
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyStockContext>(options =>
                            options.UseSqlServer(_configuration.GetConnectionString("MyStockDB")));
            services.AddScoped(typeof(MyStockContext));
            services.AddControllersWithViews();
        }
    }
}
