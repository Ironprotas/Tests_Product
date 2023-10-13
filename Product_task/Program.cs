using Microsoft.EntityFrameworkCore;
using Product_task.Pages;
using Products.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var config = builder.Configuration;
var connectionString = config.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapGet("/Index", (HttpContext context) =>
{
    context.Response.Redirect("/AllGet"); // ������� �� �������� � ����������.
    return Task.CompletedTask;
});

app.UseAuthorization();

app.MapRazorPages();

app.MapRazorPages();

app.Run();