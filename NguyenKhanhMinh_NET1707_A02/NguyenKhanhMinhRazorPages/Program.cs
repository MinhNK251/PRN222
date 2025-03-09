using BusinessObjectsLayer.Models;
using NguyenKhanhMinhRazorPages;
using RepositoriesLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
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

app.MapHub<SignalrServer>("/signalrServer");

app.Run();
