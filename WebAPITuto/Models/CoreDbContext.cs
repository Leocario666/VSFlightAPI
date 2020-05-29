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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\VsFlightDB.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingSet>(entity =>
            {
                entity.HasKey(e => new { e.FlightNo, e.PassengerId });

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
