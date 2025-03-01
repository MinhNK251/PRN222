using Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//DI - Dependency Injection
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IArtRepo, ArtRepo>();
builder.Services.AddScoped<ISupplierRepo, SupplierRepo>();

//Add Session
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

//Use Session
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
