using BreadOne.Zero.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BreadOne.Zero.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

using var scope = app.Services.CreateScope();
CreateBuiltInData(scope.ServiceProvider).Wait();

app.Run();

async Task CreateBuiltInData(IServiceProvider services)
{
    try
    {
        var _context = services.GetRequiredService<ApplicationDbContext>();
        _context.Database.EnsureCreated();

        if (_context.RoleType != null && !_context.RoleType.Any())
        {
            _context.RoleType.Add(new RoleType { Name = "Director", Active = true });
            _context.RoleType.Add(new RoleType { Name = "Manager", Active = true });
            _context.RoleType.Add(new RoleType { Name = "Supervisor", Active = true });
            _context.RoleType.Add(new RoleType { Name = "Agent", Active = true });

            await _context.SaveChangesAsync();
        }
    }
    catch (Exception)   
    {

    }
}
