namespace Anyone_for_Tennies.Services
{
    using Anyone_for_Tennies.Data;
    using Anyone_for_Tennies.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;

    public class CoachService : ICoachService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<Coach> _passwordHasher;

        public CoachService(AppDbContext context, IPasswordHasher<Coach> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<Coach> RegisterAsync(CoachRegisterViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
                throw new Exception("Passwords do not match");

            var existingCoach = await _context.Coaches.FirstOrDefaultAsync(c => c.Email == model.Email);
            if (existingCoach != null)
                throw new Exception("Email already registered");

            var coach = new Coach
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = _passwordHasher.HashPassword(null, model.Password),
                RoleID = 2,  // Coach role
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                DateOfBirth = model.DateOfBirth,
                Biography = model.Biography,
                Experience = model.Experience,
                Photo = model.Photo,
                Active = true
            };

            _context.Coaches.Add(coach);
            await _context.SaveChangesAsync();
            return coach;
        }

        public async Task<Coach> LoginAsync(CoachLoginViewModel model)
        {
            var coach = await _context.Coaches.FirstOrDefaultAsync(c => c.Email == model.Email);
            if (coach == null)
                throw new Exception("User not found");

            var result = _passwordHasher.VerifyHashedPassword(coach, coach.PasswordHash, model.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Invalid password");

            return coach;
        }

        public async Task<Coach> EditAsync(int id, CoachEditViewModel model)
        {
            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null)
                return null;

            coach.FirstName = model.FirstName;
            coach.LastName = model.LastName;
            coach.Email = model.Email;
            coach.PhoneNumber = model.PhoneNumber;
            coach.Address = model.Address;
            coach.Biography = model.Biography;
            coach.Experience = model.Experience;
            coach.Photo = model.Photo;

            _context.Coaches.Update(coach);
            await _context.SaveChangesAsync();
            return coach;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null)
                return false;

            _context.Coaches.Remove(coach);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
