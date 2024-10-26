using Anyone_for_Tennies.Data;
using Anyone_for_Tennies.Models;
using Anyone_for_Tennies.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

public class AccountService : IAccountService
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<Member> _passwordHasher;

    public AccountService(AppDbContext context, IPasswordHasher<Member> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    // Method to register a new member
    public async Task<Member> RegisterAsync(RegisterViewModel model)
    {
        // Check if the passwords match
        if (model.Password != model.ConfirmPassword)
            throw new Exception("Passwords do not match");

        // Check if email is already registered
        var existingMember = await _context.Members.FirstOrDefaultAsync(m => m.Email == model.Email);
        if (existingMember != null)
            throw new Exception("Email already registered");

        // Create new member object
        var member = new Member
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PasswordHash = _passwordHasher.HashPassword(null, model.Password),  // Hash the password
            RoleID = model.RoleID == "Member" ? 1 : model.RoleID == "Coach" ? 2 : 3,  // Assign RoleID based on role
            PhoneNumber = model.PhoneNumber,
            Address = model.Address,
            DateOfBirth = model.DateOfBirth,
            Active = true // Assuming a new member is active by default
        };

        // Add member to the database
        _context.Members.Add(member);
        await _context.SaveChangesAsync();
        return member;
    }

    // Method to log in a member
    public async Task<Member> LoginAsync(LoginViewModel model)
    {
        // Look for the member by email
        var member = await _context.Members.FirstOrDefaultAsync(m => m.Email == model.Email);

        // If member not found, throw an exception
        if (member == null)
            throw new Exception("User not found");

        // Verify the hashed password
        var result = _passwordHasher.VerifyHashedPassword(member, member.PasswordHash, model.Password);
        if (result == PasswordVerificationResult.Failed)
            throw new Exception("Invalid password");

        // Optionally check if the account is active
        if (!member.Active)
            throw new Exception("Account is inactive");

        // Return the member if login is successful
        return member;
    }

    // Method to update a member's information
    public async Task<Member> EditAsync(int id, EditViewModel model)
    {
        var member = await _context.Members.FindAsync(id);
        if (member == null)
            return null;

        member.FirstName = model.FirstName;
        member.LastName = model.LastName;
        member.Email = model.Email;
        member.PhoneNumber = model.PhoneNumber;
        member.Address = model.Address;

        _context.Members.Update(member);
        await _context.SaveChangesAsync();
        return member;
    }

    // Method to delete a member
    public async Task<bool> DeleteAsync(int id)
    {
        var member = await _context.Members.FindAsync(id);
        if (member == null)
            return false;

        _context.Members.Remove(member);
        await _context.SaveChangesAsync();
        return true;
    }
}
