using Euro2024DB_Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();//
builder.Services.AddScoped<IGroupTeamRepository, GroupTeamRepository>();//
builder.Services.AddScoped<IAccountRepository, AccountRepository>();//
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

app.Run();
