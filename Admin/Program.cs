using Microsoft.EntityFrameworkCore;
using assignment3.Service;
using assignment3.Models;
using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString)); // Use SQLite instead of SQL Server
//Or you can also Configure Entity Framework Core with SQL Server

// Configure Identity services
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// Register the AdminService for dependency injection
builder.Services.AddScoped<AdminService>();

builder.Services.AddControllersWithViews();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await SeedDatabaseAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        // Log errors or handle accordingly
    }
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

async Task SeedDatabaseAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
{
    // Create the admin role if it doesn't exist
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    var adminEmail = "admin@example.com";
    var adminPassword = "Admin@123"; // Set your desired password

    // Check if the admin user exists
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail };
        var result = await userManager.CreateAsync(adminUser, adminPassword);

        if (result.Succeeded)
        {
            // Assign the admin role to the user
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}