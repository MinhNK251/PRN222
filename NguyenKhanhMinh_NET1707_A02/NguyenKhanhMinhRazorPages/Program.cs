using BusinessObjectsLayer.Models;
using DAOsLayer;
using Microsoft.EntityFrameworkCore;
using NguyenKhanhMinhRazorPages;
using RepositoriesLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<FunewsManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionStringDB")));
builder.Services.Configure<AdminAccountSettings>(builder.Configuration.GetSection("AdminAccount"));
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<INewsArticleRepo, NewsArticleRepo>();
builder.Services.AddScoped<ISystemAccountRepo, SystemAccountRepo>();
builder.Services.AddScoped<ITagRepo, TagRepo>();
builder.Services.AddSignalR();
builder.Services.AddSession();//
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();//

app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/", async (context) => context.Response.Redirect("/Login"));

app.MapHub<SignalrServer>("/signalrServer");

app.Run();
