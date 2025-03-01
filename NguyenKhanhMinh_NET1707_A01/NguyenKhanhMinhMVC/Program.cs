using BusinessObjectsLayer.Models;
using DAOsLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;

var builder = WebApplication.CreateBuilder(args);

// Configure Admin Account from appsettings.json
builder.Services.Configure<AdminAccountSettings>(builder.Configuration.GetSection("AdminAccount"));

// Manually Register FunewsManagementContext for Dependency Injection
builder.Services.AddDbContext<FunewsManagementContext>();

// Register DAO and Repository Services
builder.Services.AddScoped<SystemAccountDAO>();
builder.Services.AddScoped<TagDAO>();
builder.Services.AddScoped<NewsArticleDAO>();
builder.Services.AddScoped<CategoryDAO>();

builder.Services.AddScoped<ISystemAccountRepo, SystemAccountRepo>();
builder.Services.AddScoped<ITagRepo, TagRepo>();
builder.Services.AddScoped<INewsArticleRepo, NewsArticleRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();

builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SystemAccount}/{action=Login}/{id?}");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=NewsArticle}/{action=Index}/{id?}");
app.Run();
