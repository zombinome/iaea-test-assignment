using Microsoft.EntityFrameworkCore;
using SkiRental.Domain.Entities;

namespace SkiRental.DataAccess
{
    public class SkiRentalDbContext: DbContext
    {
        public SkiRentalDbContext(DbContextOptions<SkiRentalDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Ski> Skis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ski>()
                .ToTable("Skis")
                .HasKey(x => x.Id);

            modelBuilder.Entity<Ski>().Property(x => x.Id).UseSqlServerIdentityColumn();
        }
    }
}
