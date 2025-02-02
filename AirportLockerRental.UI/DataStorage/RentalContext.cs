using Microsoft.EntityFrameworkCore;

namespace AirportLockerRental.UI.DataStorage;

public class RentalContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<RentalHistory> RentalHistory { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Rentals.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rental>(entity =>
        {
            entity.Property(e => e.Contents)
            .IsRequired().HasMaxLength(75);
            entity.Property(e => e.StartDate)
            .IsRequired().HasMaxLength(75);

            entity.HasOne(e => e.User)
            .WithMany(e => e.Rentals)
            .HasForeignKey(e => e.UserID);
        });
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserName)
            .IsRequired().HasMaxLength(25);

            entity.Property(e => e.PasswordHash)
            .IsRequired().HasMaxLength(200);

            entity.Property(e => e.Salt)
            .IsRequired().HasMaxLength(100);

        });
        modelBuilder.Entity<RentalHistory>(entity =>
            {
                entity.Property(e => e.LockerNumber)
                .IsRequired().HasMaxLength(25);

                entity.Property(e => e.Contents)
                .IsRequired().HasMaxLength(200);

                entity.Property(e => e.StartDate)
                .IsRequired().HasMaxLength(100);

                entity.Property(e => e.EndDate)
                .IsRequired().HasMaxLength(100);

                entity.HasOne(e => e.User)
                    .WithMany(e => e.RentalHistories)
                    .HasForeignKey(e => e.UserID);
            });
    }
}
