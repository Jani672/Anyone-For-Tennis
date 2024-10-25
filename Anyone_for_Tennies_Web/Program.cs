using Anyone_for_Tennies.Data;
using Anyone_for_Tennies.Models;
using Anyone_for_Tennies.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services for Razor Pages (frontend)
builder.Services.AddRazorPages();

// Register API Controllers (backend)
builder.Services.AddControllers();

// Register DbContext with SQL Server (or any other database you're using)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICoachService, CoachService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>(); // Register Enrollment service

// Register PasswordHasher for Member, Admin, and Coach
builder.Services.AddScoped<IPasswordHasher<Member>, PasswordHasher<Member>>();
builder.Services.AddScoped<IPasswordHasher<Admin>, PasswordHasher<Admin>>();
builder.Services.AddScoped<IPasswordHasher<Coach>, PasswordHasher<Coach>>();

// Add Session support with a timeout and essential cookies
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add Authentication and Authorization (add your own authentication scheme here if needed)
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });

builder.Services.AddAuthorization();

// Add Swagger for API documentation (for development and debugging)
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Anyone for Tennies API",
        Version = "v1"
    });
});

// Add CORS policy (in case your Razor Pages frontend needs to call the API)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure middleware for development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty;  // Serve Swagger at root URL
    });
}

// Ensure HTTPS redirection
app.UseHttpsRedirection();
app.UseStaticFiles();  // Serve static files (CSS, JavaScript, images, etc.)

app.UseRouting();

// Enable CORS (for API access from different origins)
app.UseCors("AllowAllOrigins");

// Use Session middleware
app.UseSession();

// Use Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map Razor Pages (frontend)
app.MapRazorPages();

// Map API controllers (backend)
app.MapControllers();

app.Run();  // Run the application
