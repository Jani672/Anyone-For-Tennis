// Data/AppDbContext.cs
using Anyone_for_Tennies.Models;
using Microsoft.EntityFrameworkCore;

namespace Anyone_for_Tennies.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for Roles table
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleID = 1, RoleName = "Member" },
                new Role { RoleID = 2, RoleName = "Coach" },
                new Role { RoleID = 3, RoleName = "Admin" }
            );

            modelBuilder.Entity<Member>()
                .HasOne(m => m.Role)
                .WithMany()
                .HasForeignKey(m => m.RoleID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Coach>()
                .HasOne(c => c.Role)
                .WithMany()
                .HasForeignKey(c => c.RoleID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Admin>()
                .HasOne(a => a.Role)
                .WithMany()
                .HasForeignKey(a => a.RoleID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Schedule>()
                .HasKey(s => s.ScheduleId);

            modelBuilder.Entity<Schedule>()
                .HasOne<Coach>()
                .WithMany()
                .HasForeignKey(s => s.CoachID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Enrollment>()
                .HasKey(e => e.EnrollmentID);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Member)
                .WithMany()
                .HasForeignKey(e => e.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Schedule)
                .WithMany()
                .HasForeignKey(e => e.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
