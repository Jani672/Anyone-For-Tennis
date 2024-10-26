using Anyone_for_Tennies.Data;
using Anyone_for_Tennies.Models;
using Anyone_for_Tennies.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

public class AdminService : IAdminService
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<Admin> _passwordHasher;

    public AdminService(AppDbContext context, IPasswordHasher<Admin> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    // Method to register a new admin
    public async Task<Admin> RegisterAsync(AdminRegisterViewModel model)
    {
        if (model.Password != model.ConfirmPassword)
            throw new Exception("Passwords do not match");

        var existingAdmin = await _context.Admin.FirstOrDefaultAsync(a => a.Email == model.Email);
        if (existingAdmin != null)
            throw new Exception("Email already registered");

        var admin = new Admin
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Password = _passwordHasher.HashPassword(null, model.Password),
            RoleID = 3, // Admin role
            PhoneNumber = model.PhoneNumber,
            Address = model.Address,
            DateOfBirth = model.DateOfBirth,
            Active = true
        };

        _context.Admin.Add(admin);
        await _context.SaveChangesAsync();
        return admin;
    }

    // Method to log in an admin
    public async Task<Admin> LoginAsync(AdminLoginViewModel model)
    {
        var admin = await _context.Admin.FirstOrDefaultAsync(a => a.Email == model.Email);
        if (admin == null)
            throw new Exception("User not found");

        var result = _passwordHasher.VerifyHashedPassword(admin, admin.Password, model.Password);
        if (result == PasswordVerificationResult.Failed)
            throw new Exception("Invalid password");

        return admin;
    }

    // Method to update admin information
    public async Task<Admin> EditAsync(int id, AdminEditViewModel model)
    {
        var admin = await _context.Admin.FindAsync(id);
        if (admin == null)
            return null;

        admin.FirstName = model.FirstName;
        admin.LastName = model.LastName;
        admin.Email = model.Email;
        admin.PhoneNumber = model.PhoneNumber;
        admin.Address = model.Address;

        _context.Admin.Update(admin);
        await _context.SaveChangesAsync();
        return admin;
    }

    // Method to delete an admin
    public async Task<bool> DeleteAsync(int id)
    {
        var admin = await _context.Admin.FindAsync(id);
        if (admin == null)
            return false;

        _context.Admin.Remove(admin);
        await _context.SaveChangesAsync();
        return true;
    }
}
