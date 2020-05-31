using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPITuto.Models
{
    public partial class CoreDbContext : DbContext
    {
        public CoreDbContext()
        {
        }

        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookingSet> BookingSet { get; set; }
        public virtual DbSet<FlightSet> FlightSet { get; set; }
        public virtual DbSet<PassengerSet> PassengerSet { get; set; }
        public virtual DbSet<PilotSet> PilotSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\dalme\\WWWings_2020Step1.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingSet>(entity =>
            {
                entity.HasKey(e => new { e.BookingId });

                entity.HasIndex(e => e.PassengerId);
                
                entity.HasOne(d => d.FlightNoNavigation)
                    .WithMany(p => p.BookingSet)
                    .HasForeignKey(d => d.FlightNo)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Passenger)
                    .WithMany(p => p.BookingSet)
                    .HasForeignKey(d => d.PassengerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<FlightSet>(entity =>
            {
                entity.HasIndex(e => e.PilotId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
