using Microsoft.EntityFrameworkCore;
using DbFirstEF.Models;

namespace DbFirstEF.Data
{
    public class NewLocalDbContext : DbContext
    {
        public NewLocalDbContext(DbContextOptions<NewLocalDbContext> options)
            : base(options)
        {
        }

        // DbSet for CoachProfile
        public DbSet<CoachProfile> CoachProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoachProfile>()
                .HasKey(cp => cp.CoachId); // Specify primary key

            // Seed initial data for local database
            modelBuilder.Entity<CoachProfile>().HasData(
                new CoachProfile { CoachId = 1, Biography = "Biography for Coach 1" },
                new CoachProfile { CoachId = 2, Biography = "Biography for Coach 2" },
                new CoachProfile { CoachId = 3, Biography = "Biography for Coach 3" },
                new CoachProfile { CoachId = 4, Biography = "Biography for Coach 4" },
                new CoachProfile { CoachId = 5, Biography = "Biography for Coach 5" },
                new CoachProfile { CoachId = 6, Biography = "Biography for Coach 6" },
                new CoachProfile { CoachId = 7, Biography = "Biography for Coach 7" },
                new CoachProfile { CoachId = 8, Biography = "Biography for Coach 8" },
                new CoachProfile { CoachId = 9, Biography = "Biography for Coach 9" },
                new CoachProfile { CoachId = 10, Biography = "Biography for Coach 10" },
                new CoachProfile { CoachId = 11, Biography = "Biography for Coach 11" },
                new CoachProfile { CoachId = 12, Biography = "Biography for Coach 12" },
                new CoachProfile { CoachId = 13, Biography = "Biography for Coach 13" },
                new CoachProfile { CoachId = 14, Biography = "Biography for Coach 14" },
                new CoachProfile { CoachId = 15, Biography = "Biography for Coach 15" },
                new CoachProfile { CoachId = 16, Biography = "Biography for Coach 16" },
                new CoachProfile { CoachId = 17, Biography = "Biography for Coach 17" },
                new CoachProfile { CoachId = 18, Biography = "Biography for Coach 18" },
                new CoachProfile { CoachId = 19, Biography = "Biography for Coach 19" },
                new CoachProfile { CoachId = 20, Biography = "Biography for Coach 20" }
            );
        }
    }
}
