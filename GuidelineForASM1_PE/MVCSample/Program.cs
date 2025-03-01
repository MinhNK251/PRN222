using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using ServiceLayer;

namespace MVCSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var conString = builder.Configuration.GetConnectionString("StudentDbContext") ?? throw new InvalidOperationException("Connection string 'StudentDbContext' not found.");
            builder.Services.AddDbContext<StudentDbContext>(options => options.UseSqlServer(conString));
            builder.Services.AddScoped<StudentRepository>();
            builder.Services.AddScoped<StudentService>();
            builder.Services.AddControllersWithViews();

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
        }
    }
}
