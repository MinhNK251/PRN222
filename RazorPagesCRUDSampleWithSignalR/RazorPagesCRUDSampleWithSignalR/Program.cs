using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesCRUDSampleWithSignalR.Data;
namespace RazorPagesCRUDSampleWithSignalR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<RazorPagesCRUDSampleWithSignalRContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("RazorPagesCRUDSampleWithSignalRContext") ?? throw new InvalidOperationException("Connection string 'RazorPagesCRUDSampleWithSignalRContext' not found.")));

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddSignalR();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();
            app.MapHub<SignalRServer>("/signalrServer");

            app.Run();
        }
    }
}
