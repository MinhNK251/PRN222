using Microsoft.AspNetCore.SignalR;
using PharmaceuticalManagement_SonNN;
using Sp25PharmaceuticalDB_Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IStoreAccountRepository, StoreAccountRepository>();
builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
builder.Services.AddScoped<IMedicineInformationRepository, MedicineInformationRepository>();
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
app.MapGet("/test-signalr", async (IHubContext<SignalrServer> hubContext) =>
{
    await hubContext.Clients.All.SendAsync("TestEvent", "Hello from the server!");
    return Results.Ok("SignalR message sent.");
});
app.Run();
