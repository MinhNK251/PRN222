using DataAccess.Data;
using BusinessObject.Entities;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using eStore.Components;
using Service.Services.Interfaces;
using Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using eStore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using DataAccess.DTO;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var conString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<eStoreDuckContext>(options => options.UseSqlServer(conString));
builder.Services.Configure<AdminAccountSettings>(builder.Configuration.GetSection("AdminAccount"));
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(options =>
    {
        options.Cookie.Name = "BlazorAuthCookie";
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/";
        options.SlidingExpiration = true;
    });
// Configure Redis
var redisSection = builder.Configuration.GetSection("Redis");
var redisHost = redisSection["Host"];
var redisPort = redisSection.GetValue<int>("Port");
var redisPassword = redisSection["Password"];
var redisSsl = redisSection.GetValue<bool>("Ssl");

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var options = new ConfigurationOptions
    {
        EndPoints = { "redis-16450.c100.us-east-1-4.ec2.redns.redis-cloud.com:16450" },
        Password = "qvxs5CMKOSt41Fp4J3tBoVDl7TLHpwgT",
        Ssl = false,
        AbortOnConnectFail = false
    };
    return ConnectionMultiplexer.Connect(options);
});

// Configure Distributed Cache (for session persistence across SignalR connections)
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
// Add SignalR
builder.Services.AddSignalR();
// Add Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// Add background Service
builder.Services.AddHostedService<LowStockBackgroundService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.UseSession();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/login");
        return;
    }
    await next();
});

app.MapHub<ProductCategoryHub>("/productcategoryhub");

app.UseDeveloperExceptionPage();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
