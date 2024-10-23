using Microsoft.EntityFrameworkCore;

namespace DbFirstEF.Data
{
    public class NewLocalDbContext1 : DbContext
    {
        public NewLocalDbContext1(DbContextOptions<NewLocalDbContext1> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seed data for Users table
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, FirstName = "Donald", LastName = "Trump", Email = "trump@gmail.com", Address = "43/21 New York Ave", PhoneNumber = "123456789", DateOfBirth = new DateTime(1990, 5, 12), Role = "Coach", Password = "trump" },
                new User { UserId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane@example.com", Address = "456 Elm St", PhoneNumber = "555-5678", DateOfBirth = new DateTime(1985, 8, 23), Role = "Coach", Password = "password2" },
                new User { UserId = 3, FirstName = "Sam", LastName = "Wilson", Email = "sam@example.com", Address = "789 Oak St", PhoneNumber = "555-9101", DateOfBirth = new DateTime(1978, 3, 14), Role = "Member", Password = "password3" }
            );
        }
    }
}
