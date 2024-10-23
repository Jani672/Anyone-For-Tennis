using DbFirstEF.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure the online database for default usage (e.g., coach data retrieval)
builder.Services.AddDbContext<Hitdb1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure local database for coach login using FirstName and LastName
builder.Services.AddDbContext<NewLocalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnectionCoachLogin")));

// Configure local database for email login (dbo.Users table)
builder.Services.AddDbContext<NewLocalDbContext1>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnectionEmailLogin")));

// Configure session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set a timeout as needed
    options.Cookie.HttpOnly = true; // Ensure the cookie is HTTP only
    options.Cookie.IsEssential = true; // Needed for session state
});

// Add IHttpContextAccessor for accessing HttpContext in views
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // Ensure session is used
app.UseAuthorization();

// Configure route for Coaches
app.MapControllerRoute(
    name: "coaches",
    pattern: "Coaches/{action=Index}/{id?}",
    defaults: new { controller = "Coaches", action = "Index" });

// Configure default route for Account
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login1}/{id?}");

app.Run();
