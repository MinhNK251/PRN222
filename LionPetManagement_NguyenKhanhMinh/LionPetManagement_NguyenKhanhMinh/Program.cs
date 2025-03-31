using Repositories;

namespace LionPetManagement_NguyenKhanhMinh
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<ILionAccountRepository, LionAccountRepository>();
            builder.Services.AddScoped<ILionTypeRepository, LionTypeRepository>();
            builder.Services.AddScoped<ILionProfileRepository, LionProfileRepository>();
            builder.Services.AddSignalR();
            builder.Services.AddSession();
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

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapGet("/", async (context) => context.Response.Redirect("/Login"));

            app.MapHub<SignalrServer>("/signalrServer");

            app.Run();
        }
    }
}
